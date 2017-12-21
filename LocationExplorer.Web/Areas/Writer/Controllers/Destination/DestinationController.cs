namespace LocationExplorer.Web.Areas.Writer.Controllers.Destination
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BaseControllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Service.Interfaces.Destination;
    using Service.Interfaces.Region;
    using Service.Interfaces.Tag;
    using ViewModels.Destination;

    public class DestinationController : BaseWriterController
    {
        private readonly IDestinationService destinationService;

        private readonly IRegionService regionService;

        private readonly ITagService tagService;

        public DestinationController(
            IDestinationService destinationService,
            IRegionService regionService,
            ITagService tagService)
        {
            this.destinationService = destinationService;
            this.regionService = regionService;
            this.tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
            => View(new AddDestinationViewModel
            {
                Tags = await GetTags(),
                Regions = await GetRegions()
            });

        [HttpPost]
        public async Task<IActionResult> Add(AddDestinationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Tags = await GetTags();
                model.Regions = await GetRegions();
                View(model);
            }

            return RedirectToAction(nameof(Add));
        }

        private async Task<IEnumerable<SelectListItem>> GetTags()
            => await tagService.AllAsync();

        private async Task<IEnumerable<SelectListItem>> GetRegions()
            => await regionService.AllAsync();
    }
}
