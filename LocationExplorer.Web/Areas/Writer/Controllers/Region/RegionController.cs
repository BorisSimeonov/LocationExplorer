namespace LocationExplorer.Web.Areas.Writer.Controllers.Region
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BaseControllers;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Service.Interfaces.Country;
    using Service.Interfaces.Region;
    using ViewModels.Region;

    public class RegionController : BaseWriterController
    {
        private readonly IRegionService regionService;

        private readonly ICountryService countryService;

        public RegionController(IRegionService regionService, ICountryService countryService)
        {
            this.regionService = regionService;
            this.countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
            => View(new AddRegionViewModel
            {
                Countries = await GetCountries()
            });

        [HttpPost]
        [AddDefaultSuccessMessage]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Name) && await regionService.ExistsAsync(model.Name))
            {
                ModelState.AddModelError(nameof(model.Name), "Region with the same name already exists.");
            }

            if (!await countryService.ExistsAsync(model.CountryId))
            {
                ModelState.AddModelError(nameof(model.CountryId), $"There is no country with id '{model.CountryId}'");
            }

            if (!ModelState.IsValid)
            {
                model.Countries = await GetCountries();
                return View(model);
            }

            var id = await regionService.AddAsync(model.Name, model.Description, model.CountryId);

            if (id < 1)
            {
                ModelState.AddModelError(string.Empty, "Save failed.");
                model.Countries = await GetCountries();
                return View(model);
            }

            return RedirectToAction(nameof(Add));
        }

        private async Task<IEnumerable<SelectListItem>> GetCountries()
            => await countryService.AllAsync();
    }
}
