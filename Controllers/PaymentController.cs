using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using Stripe;
using Bookstore.Data;
using Bookstore.Models;
using System.Linq;
using System.Security.Claims;

namespace Bookstore.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public IActionResult CreateCheckoutSession()
        {
            // Ensure the user is authenticated.
            if (User == null)
            {
                TempData["Error"] = "User not logged in.";
                return RedirectToAction("Index", "Cart");
            }
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var cartItems = _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Book)
                .Select(c => new {
                    // Use the null-forgiving operator assuming each CartItem has a Book.
                    Title = c.Book!.Title,
                    Price = c.Book!.Price,
                    c.Quantity
                })
                .ToList();
                
            if (cartItems == null || !cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty!";
                return RedirectToAction("Index", "Cart");
            }
            
            // Build the domain URL.
            var domain = $"{Request.Scheme}://{Request.Host}";
            
            // Create Stripe session options with shipping address collection enabled and shipping options.
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = cartItems.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = (long)(item.Price * 100), // Stripe uses cents.
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Title,
                        },
                    },
                    Quantity = item.Quantity,
                }).ToList(),
                Mode = "payment",
                // Enable shipping address collection.
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string> { "IE", "US", "CA" } // Allow Ireland (IE) plus others.
                },
                // Adding a shipping option forces the user to provide a shipping address.
                ShippingOptions = new List<SessionShippingOptionOptions>
                {
                    new SessionShippingOptionOptions
                    {
                        ShippingRateData = new SessionShippingOptionShippingRateDataOptions
                        {
                            DisplayName = "Standard Shipping",
                            Type = "fixed_amount",
                            FixedAmount = new SessionShippingOptionShippingRateDataFixedAmountOptions
                            {
                                Amount = 500, // e.g., $5.00 shipping
                                Currency = "usd"
                            },
                            DeliveryEstimate = new SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                            {
                                Minimum = new SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions
                                {
                                    Unit = "business_day",
                                    Value = 5
                                },
                                Maximum = new SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions
                                {
                                    Unit = "business_day",
                                    Value = 7
                                }
                            }
                        }
                    }
                },
                SuccessUrl = domain + "/Payment/Success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/Payment/Cancel",
            };
            
            // Create the Stripe session.
            var service = new SessionService();
            Session session = service.Create(options);
            
            // Redirect the customer to Stripe Checkout.
            return Redirect(session.Url);
        }
        
        public IActionResult Success(string session_id)
        {
            // Ensure the user is authenticated.
            if (User == null)
            {
                TempData["Error"] = "User not logged in.";
                return RedirectToAction("Index", "Cart");
            }
            
            // Retrieve the Checkout Session from Stripe.
            var sessionService = new SessionService();
            var session = sessionService.Get(session_id);

            // In your Stripe.net version, session.PaymentIntent is already a PaymentIntent object.
            PaymentIntent paymentIntent = session.PaymentIntent;
            var shipping = paymentIntent?.Shipping;
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var cartItems = _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Book)
                .ToList();
                
            if (!cartItems.Any())
            {
                TempData["Error"] = "No cart items found.";
                return RedirectToAction("Index", "Cart");
            }
            
            // Create an order record with shipping details.
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = cartItems.Sum(c => (c.Book?.Price ?? 0) * c.Quantity),
                Status = "Paid",
                ShippingName = shipping?.Name,
                ShippingAddress = shipping?.Address?.Line1,
                ShippingCity = shipping?.Address?.City,
                ShippingCounty = shipping?.Address?.State, // Map Stripe's "State" to ShippingCounty.
                ShippingPostalCode = shipping?.Address?.PostalCode,
                ShippingCountry = shipping?.Address?.Country,
                OrderItems = cartItems.Select(c => new OrderItem
                {
                    BookId = c.BookId,
                    Quantity = c.Quantity,
                    Price = c.Book?.Price ?? 0,
                    TotalPrice = (c.Book?.Price ?? 0) * c.Quantity
                }).ToList()
            };
            
            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
            
            TempData["Success"] = "Payment successful! Your order has been placed.";
            return RedirectToAction("OrderConfirmation", "Order", new { id = order.Id });
        }
        
        public IActionResult Cancel()
        {
            TempData["Error"] = "Payment was cancelled.";
            return RedirectToAction("Index", "Cart");
        }
    }
}
