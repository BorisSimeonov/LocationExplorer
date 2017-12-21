namespace LocationExplorer.Web.Controllers
{
    using System.Threading.Tasks;
    using BaseControllers;
    using Microsoft.AspNetCore.Mvc;
    using Service.Interfaces.Tag;

    public class TagController : BaseAuthorizedController
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int page = 1)
            => View(await tagService.AllTagsAsync(page));

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (!await tagService.ExistsAsync(id))
            {
                return RedirectToAction(nameof(All));
            }

            var tag = await tagService.GetByIdAsync(id);

            return View(tag);
        }
    }
}
