namespace LocationExplorer.Web.Controllers
{
    using LearningSystem.Web.Infrastructure.Extensions;
    using System.Threading.Tasks;
    using BaseControllers;
    using Microsoft.AspNetCore.Mvc;
    using Service.Interfaces.Country;

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
                TempData.AddErrorMessage("Country does not exist.");
                return RedirectToAction(nameof(All));
            }

            return View(country);
        }
    }
}