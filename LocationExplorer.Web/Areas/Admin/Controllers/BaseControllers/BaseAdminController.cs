namespace LocationExplorer.Web.Areas.Admin.Controllers.BaseControllers
{
    using Data.Infrastructure;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(WebConstants.AdministrationArea)]
    [Authorize(Roles = DataConstants.AdministratorRole)]
    public abstract class BaseAdminController : Controller
    {
    }
}
