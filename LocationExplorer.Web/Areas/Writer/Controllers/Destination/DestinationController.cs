namespace LocationExplorer.Web.Areas.Writer.Controllers.Destination
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BaseControllers;
    using Infrastructure.Filters;
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
        [AddDefaultSuccessMessage]
        public async Task<IActionResult> Add(AddDestinationViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Name) && await destinationService.ExistsAsync(model.Name))
            {
                ModelState.AddModelError(nameof(model.Name), 
                    "Destination with the same name already exists.");
            }

            var invalidIds = await tagService.CheckForInvalidTagIds(model.SelectedTags);

            if (invalidIds.Any())
            {
                ModelState.AddModelError(nameof(model.SelectedTags), 
                    $"Non-existing tags were selected. (Count: {invalidIds.Count})");
            }

            if (!await regionService.ExistsAsync(model.RegionId))
            {
                ModelState.AddModelError(nameof(model.RegionId), "Selected region does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Tags = await GetTags();
                model.Regions = await GetRegions();
                return View(model);
            }

            await destinationService.AddAsync(model.Name, model.RegionId, model.SelectedTags);

            return RedirectToAction(nameof(Add));
        }

        private async Task<IEnumerable<SelectListItem>> GetTags()
            => await tagService.AllAsync();

        private async Task<IEnumerable<SelectListItem>> GetRegions()
            => await regionService.AllAsync();
    }
}
