namespace LocationExplorer.Service.Interfaces.Tag
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ITagService
    {
        Task<IEnumerable<SelectListItem>> AllAsync();

        Task<IList<int>> CheckForInvalidTagIds(IEnumerable<int> tagIdList);
    }
}
