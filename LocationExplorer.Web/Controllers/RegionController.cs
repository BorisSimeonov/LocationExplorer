namespace LocationExplorer.Web.Controllers
{
    using System.Threading.Tasks;
    using BaseControllers;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Service.Interfaces.Region;

    public class RegionController : BaseAuthorizedController
    {
        private readonly IRegionService regionService;

        public RegionController(IRegionService regionService)
        {
            this.regionService = regionService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int page = 1)
            => View(await regionService.AllRegionsAsync(page));

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var region = await regionService.GetByIdAsync(id);
            if (region == null)
            {
                TempData.AddErrorMessage("Country does not exist.");
                return RedirectToAction(nameof(All));
            }

            return View(region);
        }
    }
}
