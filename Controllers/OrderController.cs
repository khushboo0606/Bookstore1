using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Bookstore.Data;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Checkout - Places an order from cart
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "You must be logged in to checkout.";
                return RedirectToAction("Index", "Books");
            }

            var cartItems = await _context.CartItems
                .Include(c => c.Book)
                .Where(c => c.UserId == userId && c.Book != null)
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty!";
                return RedirectToAction("Index", "Cart");
            }

            // Check if any book has a null price before proceeding
            if (cartItems.Any(item => item.Book?.Price == null))
            {
                TempData["Error"] = "One or more items in your cart have no valid price.";
                return RedirectToAction("Index", "Cart");
            }

            // Create a new order
            var order = new Order
            {
                UserId = userId,
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    Price = item.Book?.Price ?? 0 // Handle null price safely
                }).ToList(),
                TotalAmount = cartItems.Sum(item => (item.Book?.Price ?? 0) * item.Quantity),
                OrderDate = DateTime.Now
            };

            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Clear the cart
                _context.CartItems.RemoveRange(cartItems);
                await _context.SaveChangesAsync();

                return RedirectToAction("OrderConfirmation", new { id = order.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving order: {ex.Message}");
                TempData["Error"] = "There was an error processing your order.";
                return RedirectToAction("Index", "Cart");
            }
        }

        // Order Confirmation Page
        public async Task<IActionResult> OrderConfirmation(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Book) // Ensure that you are handling null correctly here
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null || order.OrderItems == null || !order.OrderItems.Any())
            {
                TempData["Error"] = "Order not found or has no items!";
                return RedirectToAction("Index", "Books");
            }

            return View(order);
        }

        // Order History - List of past orders for the logged-in user
        public async Task<IActionResult> OrderHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "You must be logged in to view your order history.";
                return RedirectToAction("Index", "Books");
            }

            // Get all orders for the logged-in user
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Book)
                .ToListAsync();

            if (orders == null || !orders.Any())
            {
                TempData["Error"] = "No orders found in your history.";
                return RedirectToAction("Index", "Books");
            }

            // Pass orders to the view
            return View(orders); 
        }
    }
}
