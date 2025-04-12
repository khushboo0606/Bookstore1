using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
    public int OrderId { get; set; } // Foreign key to Order
    public int BookId { get; set; } // Foreign key to Book
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; } // Total price for the item (Quantity * Price)
    
    public Book ?Book { get; set; } // Navigation property to Book
    public Order ?Order { get; set; } 
    }

}
