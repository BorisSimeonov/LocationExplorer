namespace LocationExplorer.Service.Interfaces.Tag
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Tag;

    public interface ITagService
    {
        Task<IEnumerable<SelectListItem>> AllAsync();

        Task<int> AddAsync(string name, IEnumerable<int> destinations);

        Task<TagDetailsServiceModel> GetByIdAsync(int id);

        Task<PagedTagListingServiceModel> AllTagsAsync(int page, int? itemsPerPage = null);

        Task<IList<int>> CheckForInvalidTagIds(IEnumerable<int> tagIdList);

        Task<bool> ExistsAsync(string name);

        Task<bool> ExistsAsync(int id);
    }
}
