namespace LocationExplorer.Service.Test.Implementations.AdminArea
{
    using System.Linq;
    using System.Threading.Tasks;
    using Service.Implementations.AdminArea;
    using TestHelpers;
    using Xunit;

    public class AdminServiceTests
    {
        public AdminServiceTests()
        {
            TestHelper.InitializeMapper();
        }

        private AdminService InitializeAdminServiceWithData()
                => new AdminService(TestHelper.GetContextWithData());

        [Fact]
        public async Task GetAllAsync_Works()
        {
            // Arrange
            var service = InitializeAdminServiceWithData();
            var page = 1;
            var itemsPerPage = 4;

            // Act
            var result = await service.AllUsersAsync(page, itemsPerPage);

            // Assert
            var testDataCount = TestHelper.GetUsersSeedList.Count;
            var usersList = result.Users;
            var pagingInfo = result.PagingInfo;

            Assert.NotNull(pagingInfo);
            Assert.True(pagingInfo.CurrentPage == page);
            Assert.True(pagingInfo.ItemsPerPage == itemsPerPage);
            Assert.True(pagingInfo.TotalItems == testDataCount);

            var expectedCount = testDataCount > itemsPerPage ? itemsPerPage : testDataCount; 
            Assert.True(usersList != null);
            Assert.True(usersList.Count() == expectedCount);
            var orderedList = usersList.OrderBy(x => x.Username).ToList();
            Assert.Equal(orderedList, usersList);
        }
    }
}
