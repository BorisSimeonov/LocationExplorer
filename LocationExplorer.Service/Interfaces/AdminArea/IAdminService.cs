namespace LocationExplorer.Service.Interfaces.AdminArea
{
    using System.Threading.Tasks;
    using Models.AdminArea;

    public interface IAdminService
    {
        Task<PagedUserListingServiceModel> AllUsersAsync(int page, int? itemsPerPage = null);
    }
}
