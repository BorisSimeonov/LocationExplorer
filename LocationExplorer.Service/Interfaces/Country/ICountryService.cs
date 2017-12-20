namespace LocationExplorer.Service.Interfaces.Country
{
    using System.Threading.Tasks;
    using Models.Country;

    public interface ICountryService
    {
        Task<CountryDetailsServiceModel> GetByIdAsync(int id);

        Task<PagedCountryListingServiceModel> AllCountriesAsync(int page, int? itemsPerPage = null);

        Task<int> AddAsync(string name);

        Task<bool> ExistsAsync(string name);

        Task<bool> ExistsAsync(int id);
    }
}
