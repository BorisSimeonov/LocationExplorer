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

        public async Task<IList<int>> CheckForInvalidTagIds(IEnumerable<int> tagIdList)
        {
            var invalidIdList = new List<int>();
            foreach (var tagId in tagIdList.Distinct())
            {
                if (!await database.Tags.AnyAsync(t => t.Id == tagId))
                {
                    invalidIdList.Add(tagId);
                }
            }

            return invalidIdList;
        }
    }
}
