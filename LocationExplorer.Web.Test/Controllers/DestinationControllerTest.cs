namespace LocationExplorer.Web.Test.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Models.Destination;
    using Moq;
    using Service.Interfaces.Destination;
    using Service.Interfaces.Tag;
    using Service.Models.Destination;
    using Web.Controllers;
    using Xunit;

    public class DestinationControllerTest
    {
        [Fact]
        public void Implements_Has_Authorized()
        {
            // arrange
            var destinationServiceMock = new Mock<IDestinationService>();
            var tagServiceMock = new Mock<ITagService>(MockBehavior.Strict);
            var controller = new DestinationController(destinationServiceMock.Object, tagServiceMock.Object);

            // act
            var hasAuthorizedAttribute = controller
                .GetType()
                .GetCustomAttributes(typeof(AuthorizeAttribute), true)
                .Any();

            // assert
            Assert.True(hasAuthorizedAttribute);
        }

        [Fact]
        public void All_When_Page_Id_Provided()
        {
            // arrange
            var destinationServiceMock = new Mock<IDestinationService>(MockBehavior.Strict);
            var tagServiceMock = new Mock<ITagService>(MockBehavior.Strict);
            var controller = new DestinationController(destinationServiceMock.Object, tagServiceMock.Object);

            var pageId = 5;
            var serviceResult = new PagedDestinationListingServiceModel();
            destinationServiceMock.Setup(s => s.AllDestinationsAsync(pageId, null)).ReturnsAsync(serviceResult);

            // act
            var result = controller.All(pageId).Result;

            // assert
            Assert.IsAssignableFrom<ViewResult>(result);
            var model = (result as ViewResult)?.Model;
            Assert.True(model != null);
            Assert.IsAssignableFrom<PagedDestinationListingServiceModel>(model);
            Assert.True(model as PagedDestinationListingServiceModel == serviceResult);
            destinationServiceMock.VerifyAll();
            tagServiceMock.VerifyAll();
        }

        [Fact]
        public void All_When_No_Page_Id()
        {
            // arrange
            var destinationServiceMock = new Mock<IDestinationService>(MockBehavior.Strict);
            var tagServiceMock = new Mock<ITagService>(MockBehavior.Strict);
            var controller = new DestinationController(destinationServiceMock.Object, tagServiceMock.Object);

            var serviceResult = new PagedDestinationListingServiceModel();
            destinationServiceMock.Setup(s => s.AllDestinationsAsync(1, null)).ReturnsAsync(serviceResult);

            // act
            var result = controller.All().Result;

            // assert
            Assert.IsAssignableFrom<ViewResult>(result);
            var model = (result as ViewResult)?.Model;
            Assert.True(model != null);
            Assert.IsAssignableFrom<PagedDestinationListingServiceModel>(model);
            Assert.True(model as PagedDestinationListingServiceModel == serviceResult);
            destinationServiceMock.VerifyAll();
            tagServiceMock.VerifyAll();
        }

        [Fact]
        public void Details_When_No_Destination_Found()
        {
            // arrange
            var destinationServiceMock = new Mock<IDestinationService>(MockBehavior.Strict);
            var tagServiceMock = new Mock<ITagService>(MockBehavior.Strict);

            var tempDataMock = new Mock<ITempDataDictionary>();
            var controller = new DestinationController(destinationServiceMock.Object, tagServiceMock.Object)
            {
                TempData = tempDataMock.Object
            };

            int id = 1;
            destinationServiceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync((DestinationDetailsServiceModel)null);

            // act
            var result = controller.Details(id);

            // assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result.Result);
            var actionName = (result.Result as RedirectToActionResult)?.ActionName;
            Assert.True(actionName != null && actionName.Equals(nameof(DestinationController.All), StringComparison.CurrentCultureIgnoreCase));
            destinationServiceMock.VerifyAll();
        }

        [Fact]
        public void Details_When_Destination_Found()
        {
            // arrange
            var destinationServiceMock = new Mock<IDestinationService>(MockBehavior.Strict);
            var tagServiceMock = new Mock<ITagService>(MockBehavior.Strict);
            var controller = new DestinationController(destinationServiceMock.Object, tagServiceMock.Object);

            int id = 1;
            var serviceResult = new DestinationDetailsServiceModel();
            destinationServiceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(serviceResult);

            // act
            var result = controller.Details(id);

            // assert
            Assert.IsAssignableFrom<ViewResult>(result.Result);
            var model = (result.Result as ViewResult)?.Model;
            Assert.True(model != null);
            Assert.IsAssignableFrom<DestinationDetailsServiceModel>(model);
            Assert.True((DestinationDetailsServiceModel)model == serviceResult);
            destinationServiceMock.VerifyAll();
        }

        [Fact]
        public void RemoveTag_When_Destination_Does_Not_Exist()
        {
            // arrange
            var destinationServiceMock = new Mock<IDestinationService>(MockBehavior.Strict);
            var tagServiceMock = new Mock<ITagService>(MockBehavior.Strict);

            var tempDataMock = new Mock<ITempDataDictionary>();
            var controller = new DestinationController(destinationServiceMock.Object, tagServiceMock.Object)
            {
                TempData = tempDataMock.Object
            };

            int destinationId = 1;
            var model = new RemoveTagRequestModel { DestinationId = destinationId };
            destinationServiceMock.Setup(s => s.ExistsAsync(destinationId)).ReturnsAsync(false);

            // act
            var result = controller.RemoveTag(model);

            // assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result.Result);
            var actionName = (result.Result as RedirectToActionResult)?.ActionName;
            Assert.True(actionName != null && actionName.Equals(nameof(DestinationController.All), StringComparison.CurrentCultureIgnoreCase));
            destinationServiceMock.VerifyAll();
            tagServiceMock.VerifyAll();
        }

        [Fact]
        public void RemoveTag_When_Tag_Does_Not_Exist()
        {
            // arrange
            var destinationServiceMock = new Mock<IDestinationService>(MockBehavior.Strict);
            var tagServiceMock = new Mock<ITagService>(MockBehavior.Strict);

            var tempDataMock = new Mock<ITempDataDictionary>();
            var controller = new DestinationController(destinationServiceMock.Object, tagServiceMock.Object)
            {
                TempData = tempDataMock.Object
            };

            int destinationId = 1;
            int tagId = 2;
            var model = new RemoveTagRequestModel { DestinationId = destinationId, TagId = tagId };
            destinationServiceMock.Setup(s => s.ExistsAsync(destinationId)).ReturnsAsync(true);
            tagServiceMock.Setup(s => s.ExistsAsync(tagId)).ReturnsAsync(false);

            // act
            var result = controller.RemoveTag(model);

            // assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result.Result);
            var castedResult = result.Result as RedirectToActionResult;
            var actionName = castedResult?.ActionName;
            Assert.True(actionName != null && actionName.Equals(nameof(DestinationController.Details), StringComparison.CurrentCultureIgnoreCase));
            Assert.True(castedResult.RouteValues.Count == 1);
            Assert.True(castedResult.RouteValues.ContainsKey("id"));
            Assert.True(Convert.ToInt32(castedResult.RouteValues["id"]) == destinationId);
            destinationServiceMock.VerifyAll();
            tagServiceMock.VerifyAll();
        }

        [Fact]
        public void RemoveTag_When_ModelState_Invalid()
        {
            // arrange
            var destinationServiceMock = new Mock<IDestinationService>(MockBehavior.Strict);
            var tagServiceMock = new Mock<ITagService>(MockBehavior.Strict);

            var tempDataMock = new Mock<ITempDataDictionary>();
            var controller = new DestinationController(destinationServiceMock.Object, tagServiceMock.Object)
            {
                TempData = tempDataMock.Object
            };

            int destinationId = 1;
            int tagId = 2;
            var model = new RemoveTagRequestModel { DestinationId = destinationId, TagId = tagId };
            destinationServiceMock.Setup(s => s.ExistsAsync(destinationId)).ReturnsAsync(true);
            tagServiceMock.Setup(s => s.ExistsAsync(tagId)).ReturnsAsync(true);

            controller.ModelState.AddModelError(string.Empty, "Some error");

            // act
            var result = controller.RemoveTag(model);

            // assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result.Result);
            var actionName = (result.Result as RedirectToActionResult)?.ActionName;
            Assert.True(actionName != null && actionName.Equals(nameof(DestinationController.All), StringComparison.CurrentCultureIgnoreCase));
            destinationServiceMock.VerifyAll();
            tagServiceMock.VerifyAll();
        }

        [Fact]
        public void RemoveTag_On_Success()
        {
            // arrange
            var destinationServiceMock = new Mock<IDestinationService>(MockBehavior.Strict);
            var tagServiceMock = new Mock<ITagService>(MockBehavior.Strict);

            var tempDataMock = new Mock<ITempDataDictionary>();
            var controller = new DestinationController(destinationServiceMock.Object, tagServiceMock.Object)
            {
                TempData = tempDataMock.Object
            };

            int destinationId = 1;
            int tagId = 2;
            var model = new RemoveTagRequestModel { DestinationId = destinationId, TagId = tagId };
            destinationServiceMock.Setup(s => s.ExistsAsync(destinationId)).ReturnsAsync(true);
            destinationServiceMock.Setup(s => s.RemoveTagAsync(destinationId, tagId)).ReturnsAsync(true);
            tagServiceMock.Setup(s => s.ExistsAsync(tagId)).ReturnsAsync(true);

            // act
            var result = controller.RemoveTag(model);

            // assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result.Result);
            var castedResult = result.Result as RedirectToActionResult;
            var actionName = castedResult?.ActionName;
            Assert.True(actionName != null && actionName.Equals(nameof(DestinationController.Details), StringComparison.CurrentCultureIgnoreCase));
            Assert.True(castedResult.RouteValues.Count == 1);
            Assert.True(castedResult.RouteValues.ContainsKey("id"));
            Assert.True(Convert.ToInt32(castedResult.RouteValues["id"]) == destinationId);
            destinationServiceMock.VerifyAll();
            tagServiceMock.VerifyAll();
        }
    }
}
