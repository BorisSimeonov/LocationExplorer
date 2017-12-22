namespace LocationExplorer.Web.Areas.Writer.Controllers.Country
{
    using Infrastructure.Extensions;
    using System.Threading.Tasks;
    using BaseControllers;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Service.Interfaces.Country;
    using ViewModels.Country;
    using static Infrastructure.WebConstants;

    public class CountryController : BaseWriterController
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        public IActionResult Add()
            => View();

        [HttpPost]
        [ValidateModelState]
        [AddDefaultSuccessMessage]
        public async Task<IActionResult> Add(AddCountryViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Name) && await countryService.ExistsAsync(model.Name))
            {
                ModelState.AddModelError(nameof(model.Name), CountryWithNameExistsMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await countryService.AddAsync(model.Name);

            return RedirectToAction(nameof(Add));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (await countryService.ExistsAsync(id))
            {
                await countryService.DeleteAsync(id);
                TempData.AddSuccessMessage();
            }
            else
            {
                TempData.AddErrorMessage("Country does not exist.");
            }

            return RedirectToAction(nameof(Add));
        }
    }
}
