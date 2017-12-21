namespace LocationExplorer.Web.Controllers
{
    using System.Threading.Tasks;
    using BaseControllers;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models.Destination;
    using Service.Interfaces.Destination;
    using Service.Interfaces.Tag;

    public class DestinationController : BaseAuthorizedController
    {
        private readonly IDestinationService destinationService;

        private readonly ITagService tagService;

        public DestinationController(IDestinationService destinationService, ITagService tagService)
        {
            this.destinationService = destinationService;
            this.tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int page = 1)
            => View(await destinationService.AllDestinationsAsync(page));

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var destination = await destinationService.GetByIdAsync(id);
            if (destination == null)
            {
                TempData.AddErrorMessage("Destination does not exist.");
                return RedirectToAction(nameof(All));
            }

            return View(await destinationService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTag(RemoveTagRequestModel model)
        {
            if (!await destinationService.ExistsAsync(model.DestinationId))
            {
                TempData.AddErrorMessage("Destination does not exist.");
                return RedirectToAction(nameof(All));
            }

            if (!await tagService.ExistsAsync(model.TagId))
            {
                TempData.AddErrorMessage("Destination does not exist.");
                return RedirectToAction(nameof(Details), new { id = model.DestinationId });
            }

            if (!ModelState.IsValid)
            {
                TempData.AddErrorMessage("Invalid data.");
                return RedirectToAction(nameof(All));
            }

            var success = await destinationService.RemoveTagAsync(model.DestinationId, model.TagId);
            TempData.AddSuccessMessage();

            return RedirectToAction(nameof(Details), new {id = model.DestinationId});
        }
    }
}

