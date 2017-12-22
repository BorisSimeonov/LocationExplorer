namespace LocationExplorer.Web.Test.Areas.Writer.Controllers.Tag
{
    using LocationExplorer.Service.Interfaces.Destination;
    using LocationExplorer.Service.Interfaces.Tag;
    using LocationExplorer.Web.Areas.Writer.Controllers.Tag;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;
    using static Data.Infrastructure.DataConstants;
    using static Web.Infrastructure.WebConstants;

    public class TagControllerTests
    {
        [Fact]
        public void TagController_Has_Authorize_And_Area_Attributes()
        {
            // arrange
            var tagServiceMock = new Mock<ITagService>();
            var destinationServiceMock = new Mock<IDestinationService>();
            var controller = new TagController(tagServiceMock.Object, destinationServiceMock.Object);

            // act
            var authorizeAttributes = controller
                .GetType()
                .GetCustomAttributes(typeof(AuthorizeAttribute), true)
                .Cast<AuthorizeAttribute>()
                .ToList();

            var areaAttributes = controller
                .GetType()
                .GetCustomAttributes(typeof(AreaAttribute), true)
                .Cast<AreaAttribute>()
                .ToList();

            // assert
            Assert.True(authorizeAttributes.Count == 1);
            Assert.True(areaAttributes.Count == 1);
            Assert.True(authorizeAttributes[0].Roles == WriterRole);
            Assert.True(areaAttributes[0].RouteKey == "area");
            Assert.True(areaAttributes[0].RouteValue == WriterArea);
        }

        [Fact]
        public async Task Delete_DefaultWorkflow()
        {
            // arrange
            var idToDelete = 13;
            var tagServiceMock = new Mock<ITagService>(MockBehavior.Strict);
            var destinationServiceMock = new Mock<IDestinationService>(MockBehavior.Strict);
            var controller = new TagController(tagServiceMock.Object, destinationServiceMock.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };

            tagServiceMock.Setup(s => s.ExistsAsync(idToDelete)).ReturnsAsync(true);
            tagServiceMock.Setup(s => s.DeleteAsync(idToDelete)).ReturnsAsync(true);

            // act
            var result = await controller.Delete(idToDelete);

            // assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
            var actionName = (result as RedirectToActionResult)?.ActionName;
            Assert.True(string.Equals(actionName, nameof(TagController.Add), System.StringComparison.InvariantCultureIgnoreCase));
            destinationServiceMock.VerifyAll();
            tagServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_Id_Does_Not_Exist()
        {
            // arrange
            var idToDelete = 13;
            var tagServiceMock = new Mock<ITagService>(MockBehavior.Strict);
            var destinationServiceMock = new Mock<IDestinationService>(MockBehavior.Strict);
            var controller = new TagController(tagServiceMock.Object, destinationServiceMock.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };

            tagServiceMock.Setup(s => s.ExistsAsync(idToDelete)).ReturnsAsync(false);

            // act
            var result = await controller.Delete(idToDelete);

            // assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
            var actionName = (result as RedirectToActionResult)?.ActionName;
            Assert.True(string.Equals(actionName, nameof(TagController.Add), System.StringComparison.InvariantCultureIgnoreCase));
            destinationServiceMock.VerifyAll();
            tagServiceMock.VerifyAll();
        }
    }
}
