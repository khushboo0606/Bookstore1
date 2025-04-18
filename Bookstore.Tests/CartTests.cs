using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Bookstore.Models;

namespace Bookstore.Tests
{
    public class CartItemTests
    {
        [Fact]
        public void CartItem_WithInvalidQuantity_ShouldFailValidation()
        {
            // Arrange
            var item = new CartItem
            {
                Quantity = 0,  // Invalid
                BookId = 1,
                UserId = "user1"
            };

            // Act
            var context = new ValidationContext(item);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(item, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("Quantity must be at least 1"));

}
    }}