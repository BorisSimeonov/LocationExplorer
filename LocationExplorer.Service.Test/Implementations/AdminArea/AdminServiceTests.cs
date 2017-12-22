namespace LocationExplorer.Service.Test.Implementations.AdminArea
{
    using Service.Implementations.AdminArea;
    using System.Linq;
    using System.Threading.Tasks;
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
        public async Task GetAllAsync_Main_Workflow()
        {
            // Arrange
            var service = InitializeAdminServiceWithData();
            var page = 1;
            var itemsPerPage = 4;

            // Act
            var result = await service.AllUsersAsync(page, itemsPerPage);

            // Assert
            var testDataCount = TestHelperDataProvider.GetUsersSeedList.Count;
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

        [Fact]
        public async Task DeleteAsync_Main_Workflow()
        {
            // arrange 
            var context = TestHelper.GetContextWithData();
            var service = new AdminService(context);
            var existingUser = TestHelperDataProvider.GetUsersSeedList.First();
            var id = existingUser.Id;

            // act
            var result = await service.DeleteUserAsync(existingUser.Id);

            // assert
            Assert.True(result);
            Assert.True(context.Users.All(u => u.Id != id));
            var initialCollectionCount = TestHelperDataProvider.GetUsersSeedList.Count;
            var contextUsersList = context.Users.ToList();
            Assert.True(contextUsersList.Count == initialCollectionCount - 1);
        }
    }
}
