using Microsoft.AspNetCore.Mvc;
using Moq;
using my_swift.Controllers;
using my_swift.Models;
using my_swift.Services;

namespace MySwiftApp.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewWithCategories()
        {
            // Arrange
            var categories = new List<SwiftCategory>
            {
                new() { Id = "test", Title = "Test", Messages = new() { new SwiftMessage { Code="MT001", Description="Desc"} } }
            };

            var serviceMock = new Mock<ISwiftMessageService>();
            serviceMock.Setup(s => s.GetAllCategories()).Returns(categories);

            var controller = new HomeController(serviceMock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<SwiftCategory>>(viewResult.Model);
            Assert.Single(model);
            Assert.Equal("Test", model[0].Title);
        }
    }
}
