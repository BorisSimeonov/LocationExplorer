namespace LocationExplorer.Service.Implementations.AdminArea
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Infrastructure;
    using Interfaces.AdminArea;
    using Microsoft.EntityFrameworkCore;
    using Models.AdminArea;

    public class AdminService : IAdminService
    {
        private readonly LocationExplorerDbContext database;

        public AdminService(LocationExplorerDbContext database)
        {
            this.database = database;
        }

        public async Task<PagedUserListingServiceModel> AllUsersAsync(int page, int? itemsPerPage = null)
        {
            itemsPerPage = !itemsPerPage.HasValue || itemsPerPage < 1
                ? PagingConstants.DefaultItemsPerPage
                : itemsPerPage.Value;

            page = page < 1 ? 1 : page;

            return new PagedUserListingServiceModel
            {
                Users = await database.Users
                    .OrderBy(u => u.UserName)
                    .Skip((page - 1) * itemsPerPage.Value)
                    .Take(itemsPerPage.Value)
                    .ProjectTo<UserListingModelService>()
                    .ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = itemsPerPage.Value,
                    TotalItems = await database.Users.CountAsync()
                }
            };
        } 
    }
}
