using System;
using System.Collections.Generic;

namespace Bookstore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ?UserId { get; set; }  // Ensuring it's not nullable
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
         public string ?Status { get; set; }

         public string? ShippingName { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingCounty { get; set; }
        public string? ShippingPostalCode { get; set; }
        public string? ShippingCountry { get; set; }
        
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); 
}
}
