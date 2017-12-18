namespace LocationExplorer.Web.Areas.Admin.Controllers
{
    using BaseControllers;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseAdminController
    {
        [HttpGet]
        public IActionResult Index()
            => View();
    }
}
