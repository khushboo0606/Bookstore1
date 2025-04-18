using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Bookstore.Models;

namespace Bookstore.Tests
{
    public class BookTests
    {
        [Fact]
        public void Book_WithoutTitle_ShouldFailValidation()
        {
            var book = new Book
            {
                Price = 20,
                Quantity = 5
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(book);

            bool isValid = Validator.TryValidateObject(book, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("Title"));
        }
    }
}
