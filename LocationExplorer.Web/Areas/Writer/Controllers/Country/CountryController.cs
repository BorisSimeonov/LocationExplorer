namespace LocationExplorer.Web.Areas.Writer.Controllers.Country
{
    using System.Threading.Tasks;
    using BaseControllers;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Service.Interfaces.Country;
    using ViewModels;

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
                ModelState.AddModelError(nameof(model.Name), "Contry with the same name exist.");
                return View(model);
            }

            await countryService.AddAsync(model.Name);

            return RedirectToAction(nameof(Add));
        }
    }
}
