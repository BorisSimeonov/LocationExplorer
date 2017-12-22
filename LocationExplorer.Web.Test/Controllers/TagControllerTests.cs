namespace LocationExplorer.Web.Test.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using Service.Interfaces.Tag;
    using Service.Models.Country;
    using Service.Models.Tag;
    using Web.Controllers;
    using Xunit;

    public class TagControllerTests
    {
        [Fact]
        public void Implements_Has_Authorized()
        {
            // arrange
            var serviceMock = new Mock<ITagService>();
            var controller = new TagController(serviceMock.Object);

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
            var serviceMock = new Mock<ITagService>(MockBehavior.Strict);
            var controller = new TagController(serviceMock.Object);

            var pageId = 5;
            var serviceResult = new PagedTagListingServiceModel();
            serviceMock.Setup(s => s.AllTagsAsync(pageId, null)).ReturnsAsync(serviceResult);

            // act
            var result = controller.All(pageId).Result;

            // assert
            Assert.IsAssignableFrom<ViewResult>(result);
            var model = (result as ViewResult)?.Model;
            Assert.True(model != null);
            Assert.IsAssignableFrom<PagedTagListingServiceModel>(model);
            Assert.True(model as PagedTagListingServiceModel == serviceResult);
            serviceMock.VerifyAll();
        }

        [Fact]
        public void All_When_No_Page_Id()
        {
            // arrange
            var serviceMock = new Mock<ITagService>(MockBehavior.Strict);
            var controller = new TagController(serviceMock.Object);

            var serviceResult = new PagedTagListingServiceModel();
            serviceMock.Setup(s => s.AllTagsAsync(1, null)).ReturnsAsync(serviceResult);

            // act
            var result = controller.All().Result;

            // assert
            Assert.IsAssignableFrom<ViewResult>(result);
            var model = (result as ViewResult)?.Model;
            Assert.True(model != null);
            Assert.IsAssignableFrom<PagedTagListingServiceModel>(model);
            Assert.True(model as PagedTagListingServiceModel == serviceResult);
            serviceMock.VerifyAll();
        }

        [Fact]
        public void Details_When_No_Tag_Found()
        {
            // arrange
            var serviceMock = new Mock<ITagService>(MockBehavior.Strict);

            var tempDataMock = new Mock<ITempDataDictionary>();
            var controller = new TagController(serviceMock.Object)
            {
                TempData = tempDataMock.Object
            };

            int id = 1;
            serviceMock.Setup(s => s.ExistsAsync(id)).ReturnsAsync(false);

            // act
            var result = controller.Details(id);

            // assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result.Result);
            var actionName = (result.Result as RedirectToActionResult)?.ActionName;
            Assert.True(actionName != null && actionName.Equals(nameof(TagController.All), StringComparison.InvariantCultureIgnoreCase));
            serviceMock.VerifyAll();
        }

        [Fact]
        public void Details_When_Tag_Found()
        {
            // arrange
            var serviceMock = new Mock<ITagService>(MockBehavior.Strict);
            var controller = new TagController(serviceMock.Object);

            int id = 1;
            var serviceResult = new TagDetailsServiceModel();
            serviceMock.Setup(s => s.ExistsAsync(id)).ReturnsAsync(true);
            serviceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(serviceResult);

            // act
            var result = controller.Details(id);

            // assert
            Assert.IsAssignableFrom<ViewResult>(result.Result);
            var model = (result.Result as ViewResult)?.Model;
            Assert.True(model != null);
            Assert.IsAssignableFrom<TagDetailsServiceModel>(model);
            Assert.True((TagDetailsServiceModel)model == serviceResult);
            serviceMock.VerifyAll();
        }
    }
}
