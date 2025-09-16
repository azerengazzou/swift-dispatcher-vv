using my_swift.Services;

namespace MySwiftApp.Tests
{
    public class SwiftMessageServiceTests
    {
        [Fact]
        public void GetAllCategories_ShouldReturnCategoriesWithMessages()
        {
            // Arrange
            var service = new SwiftMessageService();

            // Act
            var categories = service.GetAllCategories();

            // Assert
            Assert.NotNull(categories);
            Assert.NotEmpty(categories);
            Assert.All(categories, c => Assert.NotEmpty(c.Messages));
        }
    }
}
