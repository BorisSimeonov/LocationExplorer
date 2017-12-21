namespace LocationExplorer.Service.Implementations.Tag
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Interfaces.Tag;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class TagService : ITagService
    {
        private readonly LocationExplorerDbContext database;

        public TagService(LocationExplorerDbContext database)
        {
            this.database = database;
        }

        public async Task<IEnumerable<SelectListItem>> AllAsync()
            => await database.Tags
            .OrderBy(t => t.Name)
            .Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            })
            .ToListAsync();
    }
}
