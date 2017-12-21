namespace LocationExplorer.Service.Interfaces.Region
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Region;

    public interface IRegionService
    {
        Task<RegionDetailsServiceModel> GetByIdAsync(int id);

        Task<PagedRegionListingServiceModel> AllRegionsAsync(int page, int? itemsPerPage = null);

        Task<int> AddAsync(string name, string description, int countryId);

        Task<IEnumerable<SelectListItem>> AllAsync();

        Task<bool> ExistsAsync(string name);

        Task<bool> ExistsAsync(int id);
    }
}
