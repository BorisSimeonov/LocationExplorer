namespace LocationExplorer.Service.Interfaces.Country
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Country;

    public interface ICountryService
    {
        Task<CountryDetailsServiceModel> GetByIdAsync(int id);

        Task<PagedCountryListingServiceModel> AllCountriesAsync(int page, int? itemsPerPage = null);

        Task<IEnumerable<SelectListItem>> AllAsync();
        
        Task<int> AddAsync(string name);

        Task<bool> ExistsAsync(string name);

        Task<bool> ExistsAsync(int id);

        Task<bool> DeleteAsync(int id);
    }
}
