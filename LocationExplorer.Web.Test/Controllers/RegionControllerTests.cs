namespace LocationExplorer.Web.Test.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using Service.Interfaces.Region;
    using Service.Models.Region;
    using Web.Controllers;
    using Xunit;

    public class RegionControllerTests
    {
        [Fact]
        public void Implements_Has_Authorized()
        {
            // arrange
            var serviceMock = new Mock<IRegionService>();
            var controller = new RegionController(serviceMock.Object);

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
            var serviceMock = new Mock<IRegionService>(MockBehavior.Strict);
            var controller = new RegionController(serviceMock.Object);

            var pageId = 5;
            var serviceResult = new PagedRegionListingServiceModel();
            serviceMock.Setup(s => s.AllRegionsAsync(pageId, null)).ReturnsAsync(serviceResult);

            // act
            var result = controller.All(pageId).Result;

            // assert
            Assert.IsAssignableFrom<ViewResult>(result);
            var model = (result as ViewResult)?.Model;
            Assert.True(model != null);
            Assert.IsAssignableFrom<PagedRegionListingServiceModel>(model);
            Assert.True(model as PagedRegionListingServiceModel == serviceResult);
            serviceMock.VerifyAll();
        }

        [Fact]
        public void All_When_No_Page_Id()
        {
            // arrange
            var serviceMock = new Mock<IRegionService>(MockBehavior.Strict);
            var controller = new RegionController(serviceMock.Object);

            var serviceResult = new PagedRegionListingServiceModel();
            serviceMock.Setup(s => s.AllRegionsAsync(1, null)).ReturnsAsync(serviceResult);

            // act
            var result = controller.All().Result;

            // assert
            Assert.IsAssignableFrom<ViewResult>(result);
            var model = (result as ViewResult)?.Model;
            Assert.True(model != null);
            Assert.IsAssignableFrom<PagedRegionListingServiceModel>(model);
            Assert.True(model as PagedRegionListingServiceModel == serviceResult);
            serviceMock.VerifyAll();
        }

        [Fact]
        public void Details_When_No_Region_Found()
        {
            // arrange
            var serviceMock = new Mock<IRegionService>(MockBehavior.Strict);

            var tempDataMock = new Mock<ITempDataDictionary>();
            var controller = new RegionController(serviceMock.Object)
            {
                TempData = tempDataMock.Object
            };

            int id = 1;
            serviceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync((RegionDetailsServiceModel)null);

            // act
            var result = controller.Details(id);

            // assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result.Result);
            var actionName = (result.Result as RedirectToActionResult)?.ActionName;
            Assert.True(actionName != null && actionName.Equals(nameof(RegionController.All), StringComparison.InvariantCultureIgnoreCase));
            serviceMock.VerifyAll();
        }

        [Fact]
        public void Details_When_Region_Found()
        {
            // arrange
            var serviceMock = new Mock<IRegionService>(MockBehavior.Strict);
            var controller = new RegionController(serviceMock.Object);

            int id = 1;
            var serviceResult = new RegionDetailsServiceModel();
            serviceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(serviceResult);

            // act
            var result = controller.Details(id);

            // assert
            Assert.IsAssignableFrom<ViewResult>(result.Result);
            var model = (result.Result as ViewResult)?.Model;
            Assert.True(model != null);
            Assert.IsAssignableFrom<RegionDetailsServiceModel>(model);
            Assert.True((RegionDetailsServiceModel)model == serviceResult);
            serviceMock.VerifyAll();
        }
    }
}
