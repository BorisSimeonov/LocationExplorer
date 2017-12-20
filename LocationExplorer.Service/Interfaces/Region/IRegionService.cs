namespace LocationExplorer.Service.Interfaces.Region
{
    using System.Threading.Tasks;
    using Models.Region;

    public interface IRegionService
    {
        Task<RegionDetailsServiceModel> GetByIdAsync(int id);

        Task<PagedRegionListingServiceModel> AllRegionsAsync(int page, int? itemsPerPage = null);

        Task<int> AddAsync(string name, string description, int countryId);

        Task<bool> ExistsAsync(string name);

        Task<bool> ExistsAsync(int id);
    }
}
