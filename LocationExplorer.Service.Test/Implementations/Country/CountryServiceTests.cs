using LocationExplorer.Service.Models.Country;

namespace LocationExplorer.Service.Test.Implementations.Country
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Service.Implementations.Country;
    using TestHelpers;
    using Xunit;

    public class CountryServiceTests
    {
        public CountryServiceTests()
        {
            TestHelper.InitializeMapper();
        }

        private CountryService InitializeCountryServiceWithData()
            => new CountryService(TestHelper.GetContextWithData());

        [Fact]
        public async Task ExistAsync_By_Id_When_Does_Not_Exist()
        {
            // Arrange
            var service = InitializeCountryServiceWithData();
            var missingId = 33333;

            // Act
            var result = await service.ExistsAsync(missingId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ExistAsync_By_Id_When_Exist()
        {
            // Arrange
            var service = InitializeCountryServiceWithData();
            var existingCountry = TestHelper.GetCountrySeedList.First();

            // Act
            var result = await service.ExistsAsync(existingCountry.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ExistAsync_By_Name_When_Exist()
        {
            // Arrange
            var service = InitializeCountryServiceWithData();
            var existingCountry = TestHelper.GetCountrySeedList.First();

            // Act
            var result = await service.ExistsAsync(existingCountry.Name);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ExistAsync_By_Name_When_Does_Not_Exist()
        {
            // Arrange
            var service = InitializeCountryServiceWithData();
            var notExistingNameValue = "Not Existing Name Value";

            // Act
            var result = await service.ExistsAsync(notExistingNameValue);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetAllAsync_Works()
        {
            // Arrange
            var service = InitializeCountryServiceWithData();
            var page = 1;
            var itemsPerPage = 4;

            // Act
            var result = await service.AllCountriesAsync(page, itemsPerPage);

            // Assert
            var testDataCount = TestHelper.GetCountrySeedList.Count;
            var countries = result.Countries;
            var pagingInfo = result.PagingInfo;

            Assert.NotNull(pagingInfo);
            Assert.True(pagingInfo.CurrentPage == page);
            Assert.True(pagingInfo.ItemsPerPage == itemsPerPage);
            Assert.True(pagingInfo.TotalItems == testDataCount);

            var expectedCount = testDataCount > itemsPerPage ? itemsPerPage : testDataCount;
            Assert.True(countries != null);
            Assert.True(countries.Count() == expectedCount);
            var orderedList = countries.OrderBy(x => x.Name).ToList();
            Assert.Equal(orderedList, countries);
        }

        [Fact]
        public async Task CreateAsync_Works()
        {
            // Arrange
            var context = TestHelper.GetContextWithData();
            var service = new CountryService(context);
            var name = "New Country";

            // Act
            var id = await service.AddAsync(name);

            // Assert
            Assert.True(id > 0);
            var expectedCount = TestHelper.GetCountrySeedList.Count + 1;
            Assert.True(context.Countries.Count() == expectedCount);
            var newItem = await context.Countries.FirstAsync(x => x.Id == id);
            Assert.True(newItem != null && newItem.Name == name);
        }

        [Fact]
        public async Task GetByIdAsync_Works()
        {
            // Arrange
            var service = InitializeCountryServiceWithData();
            var existingCountry = TestHelper.GetCountrySeedList.First();

            // Act
            var result = await service.GetByIdAsync(existingCountry.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<CountryDetailsServiceModel>(result);
            Assert.Equal(existingCountry.Name, result.Name);
        }
    }
}
