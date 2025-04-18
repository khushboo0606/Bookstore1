using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Bookstore.Models;  // This will work after you fix the project reference

namespace Bookstore.Tests
{
    public class BookTests
    {
        [Fact]
        public void Book_ShouldFailValidation_WhenTitleIsMissing()
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
