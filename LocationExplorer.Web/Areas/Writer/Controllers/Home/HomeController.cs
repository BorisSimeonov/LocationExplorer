namespace LocationExplorer.Web.Areas.Writer.Controllers.Home
{
    using BaseControllers;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseWriterController
    {
        public IActionResult Index()
            => View();
    }
}
