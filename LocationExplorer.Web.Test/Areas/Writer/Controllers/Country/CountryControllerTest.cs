namespace LocationExplorer.Web.Test.Areas.Writer.Controllers.Country
{
    using LocationExplorer.Service.Interfaces.Country;
    using LocationExplorer.Web.Areas.Writer.Controllers.Country;
    using LocationExplorer.Web.Areas.Writer.ViewModels.Country;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;
    using static Data.Infrastructure.DataConstants;
    using static Web.Infrastructure.WebConstants;

    public class CountryControllerTest
    {
        [Fact]
        public void CountryController_Has_Authorize_And_Area_Attributes()
        {
            // arrange
            var countryService = new Mock<ICountryService>();
            var controller = new CountryController(countryService.Object);

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
            var countryService = new Mock<ICountryService>(MockBehavior.Strict);
            var controller = new CountryController(countryService.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };

            countryService.Setup(s => s.ExistsAsync(idToDelete)).ReturnsAsync(true);
            countryService.Setup(s => s.DeleteAsync(idToDelete)).ReturnsAsync(true);

            // act
            var result = await controller.Delete(idToDelete);

            // assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
            var actionName = (result as RedirectToActionResult)?.ActionName;
            Assert.True(string.Equals(actionName, nameof(CountryController.Add), System.StringComparison.InvariantCultureIgnoreCase));
            countryService.VerifyAll();
        }

        [Fact]
        public async Task Delete_Id_Does_Not_Exist()
        {
            // arrange
            var idToDelete = 13;
            var countryService = new Mock<ICountryService>(MockBehavior.Strict);
            var controller = new CountryController(countryService.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };

            countryService.Setup(s => s.ExistsAsync(idToDelete)).ReturnsAsync(false);

            // act
            var result = await controller.Delete(idToDelete);

            // assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
            var actionName = (result as RedirectToActionResult)?.ActionName;
            Assert.True(string.Equals(actionName, nameof(CountryController.Add), System.StringComparison.InvariantCultureIgnoreCase));
            countryService.VerifyAll();
        }

        [Fact]
        public async Task Add_Returns_ViewResult()
        {
            // arrange
            var countryService = new Mock<ICountryService>(MockBehavior.Strict);
            var controller = new CountryController(countryService.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };

            // act
            var result = controller.Add();

            // assert
            Assert.IsAssignableFrom<ViewResult>(result);
            countryService.VerifyAll();
        }

        [Fact]
        public async Task Add_When_Name_Exists()
        {
            // arrange
            var countryService = new Mock<ICountryService>(MockBehavior.Strict);
            var controller = new CountryController(countryService.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };

            var existingName = "CountryName";
            var model = new AddCountryViewModel { Name = existingName };

            countryService.Setup(s => s.ExistsAsync(existingName)).ReturnsAsync(true);

            // act
            var result = await controller.Add(model);

            // assert
            Assert.IsAssignableFrom<ViewResult>(result);
            var resultModel = (result as ViewResult).Model;
            Assert.NotNull(resultModel);
            Assert.IsAssignableFrom<AddCountryViewModel>(resultModel);
            Assert.Equal(model, resultModel);
            Assert.False(controller.ModelState.IsValid);
            Assert.True(controller.ModelState.ContainsKey(nameof(AddCountryViewModel.Name)));
            var stateValues = controller.ModelState.Values.ToList();
            Assert.True(stateValues.Count == 1);
            var valueMessages = stateValues[0].Errors.ToList();
            Assert.True(valueMessages.Count == 1);
            Assert.True(valueMessages[0].ErrorMessage == CountryWithNameExistsMessage);
            countryService.VerifyAll();
        }

        [Fact]
        public async Task Add_When_ModelState_Invalid()
        {
            // arrange
            var countryService = new Mock<ICountryService>(MockBehavior.Strict);
            var controller = new CountryController(countryService.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };

            var countryName = "CountryName";
            var model = new AddCountryViewModel { Name = countryName };

            controller.ModelState.AddModelError(string.Empty, "Some Extreme Error");
            countryService.Setup(s => s.ExistsAsync(countryName)).ReturnsAsync(false);

            // act
            var result = await controller.Add(model);

            // assert
            Assert.IsAssignableFrom<ViewResult>(result);
            var resultModel = (result as ViewResult).Model;
            Assert.NotNull(resultModel);
            Assert.IsAssignableFrom<AddCountryViewModel>(resultModel);
            Assert.Equal(model, resultModel);
            countryService.VerifyAll();
        }

        [Fact]
        public async Task Add_Calls_SaveAsync()
        {
            // arrange
            var countryService = new Mock<ICountryService>(MockBehavior.Strict);
            var controller = new CountryController(countryService.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };

            var countryName = "CountryName";
            var model = new AddCountryViewModel { Name = countryName };

            countryService.Setup(s => s.ExistsAsync(countryName)).ReturnsAsync(false);
            countryService.Setup(s => s.AddAsync(countryName)).ReturnsAsync(2);

            // act
            var result = await controller.Add(model);

            // assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
            var actionName = (result as RedirectToActionResult).ActionName;
            Assert.Equal(nameof(CountryController.Add), actionName);
            countryService.VerifyAll();
        }
    }
}
