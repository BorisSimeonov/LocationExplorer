namespace LocationExplorer.Web.Test.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using Service.Interfaces.Country;
    using Service.Models.Country;
    using Web.Controllers;
    using Xunit;

    public class CountryControllerTests
    {
        [Fact]
        public void Implements_Has_Authorized()
        {
            // arrange
            var serviceMock = new Mock<ICountryService>();
            var controller = new CountryController(serviceMock.Object);

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
            var serviceMock = new Mock<ICountryService>(MockBehavior.Strict);
            var controller = new CountryController(serviceMock.Object);

            var pageId = 5;
            var serviceResult = new PagedCountryListingServiceModel();
            serviceMock.Setup(s => s.AllCountriesAsync(pageId, null)).ReturnsAsync(serviceResult);

            // act
            var result = controller.All(pageId).Result;

            // assert
            Assert.IsAssignableFrom<ViewResult>(result);
            var model = (result as ViewResult)?.Model;
            Assert.True(model != null);
            Assert.IsAssignableFrom<PagedCountryListingServiceModel>(model);
            Assert.True(model as PagedCountryListingServiceModel == serviceResult);
            serviceMock.VerifyAll();
        }

        [Fact]
        public void All_When_No_Page_Id()
        {
            // arrange
            var serviceMock = new Mock<ICountryService>(MockBehavior.Strict);
            var controller = new CountryController(serviceMock.Object);

            var serviceResult = new PagedCountryListingServiceModel();
            serviceMock.Setup(s => s.AllCountriesAsync(1, null)).ReturnsAsync(serviceResult);

            // act
            var result = controller.All().Result;

            // assert
            Assert.IsAssignableFrom<ViewResult>(result);
            var model = (result as ViewResult)?.Model;
            Assert.True(model != null);
            Assert.IsAssignableFrom<PagedCountryListingServiceModel>(model);
            Assert.True(model as PagedCountryListingServiceModel == serviceResult);
            serviceMock.VerifyAll();
        }

        [Fact]
        public void Details_When_No_Country_Found()
        {
            // arrange
            var serviceMock = new Mock<ICountryService>(MockBehavior.Strict);

            var tempDataMock = new Mock<ITempDataDictionary>();
            var controller = new CountryController(serviceMock.Object)
            {
                TempData = tempDataMock.Object
            };

            int id = 1;
            serviceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync((CountryDetailsServiceModel)null);

            // act
            var result = controller.Details(id);

            // assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result.Result);
            var actionName = (result.Result as RedirectToActionResult)?.ActionName;
            Assert.True(actionName != null && actionName.Equals(nameof(CountryController.All), StringComparison.InvariantCultureIgnoreCase));
            serviceMock.VerifyAll();
        }

        [Fact]
        public void Details_When_Country_Found()
        {
            // arrange
            var serviceMock = new Mock<ICountryService>(MockBehavior.Strict);
            var controller = new CountryController(serviceMock.Object);

            int id = 1;
            var serviceResult = new CountryDetailsServiceModel();
            serviceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(serviceResult);

            // act
            var result = controller.Details(id);

            // assert
            Assert.IsAssignableFrom<ViewResult>(result.Result);
            var model = (result.Result as ViewResult)?.Model;
            Assert.True(model != null);
            Assert.IsAssignableFrom<CountryDetailsServiceModel>(model);
            Assert.True((CountryDetailsServiceModel) model == serviceResult);
            serviceMock.VerifyAll();
        }
    }
}
