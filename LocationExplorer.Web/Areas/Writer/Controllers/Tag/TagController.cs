namespace LocationExplorer.Web.Areas.Writer.Controllers.Tag
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BaseControllers;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Service.Interfaces.Destination;
    using Service.Interfaces.Tag;
    using ViewModels.Tag;

    public class TagController : BaseWriterController
    {
        private readonly ITagService tagService;

        private readonly IDestinationService destinationService;

        public TagController(ITagService tagService, IDestinationService destinationService)
        {
            this.tagService = tagService;
            this.destinationService = destinationService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
            => View(new AddTagViewModel
            {
                Destinations = await GetDestinations()
            });

        [HttpPost]
        [AddDefaultSuccessMessage]
        public async Task<IActionResult> Add(AddTagViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Name) && await tagService.ExistsAsync(model.Name))
            {
                ModelState.AddModelError(nameof(model.Name), "Tag with the same name already exists.");
            }

            if (!ModelState.IsValid)
            {
                model.Destinations = await GetDestinations();
                return View(model);
            }

            var id = await tagService.AddAsync(model.Name, model.SelectedDestinations);

            if (id < 1)
            {
                ModelState.AddModelError(string.Empty, "Save failed.");
                model.Destinations = await GetDestinations();
                return View(model);
            }

            return RedirectToAction(nameof(Add));
        }

        private async Task<IEnumerable<SelectListItem>> GetDestinations()
            => await destinationService.AllAsync();
    }
}
