namespace LocationExplorer.Web.Controllers
{
    using System.Threading.Tasks;
    using BaseControllers;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Service.Interfaces.Country;
    using static Infrastructure.WebConstants;

    public class CountryController : BaseAuthorizedController
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int page = 1)
            => View(await countryService.AllCountriesAsync(page));

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var country = await countryService.GetByIdAsync(id);
            if (country == null)
            {
                TempData.AddErrorMessage(CountryNotFoundErrorMessage);
                return RedirectToAction(nameof(All));
            }

            return View(country);
        }
    }
}