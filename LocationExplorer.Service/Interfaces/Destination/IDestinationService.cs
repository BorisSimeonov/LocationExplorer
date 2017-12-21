namespace LocationExplorer.Service.Interfaces.Destination
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Destination;

    public interface IDestinationService
    {
        Task<DestinationDetailsServiceModel> GetByIdAsync(int id);

        Task<PagedDestinationListingServiceModel> AllDestinationsAsync(int page, int? itemsPerPage = null);

        Task<int> AddAsync(string name, int regionId, IList<int> tags);

        Task<IEnumerable<SelectListItem>> AllAsync();

        Task<bool> ExistsAsync(string name);

        Task<bool> ExistsAsync(int id);

        Task<bool> RemoveTagAsync(int destinationId, int tagId);
    }
}
