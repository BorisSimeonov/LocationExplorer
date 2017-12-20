namespace LocationExplorer.Web.Controllers.BaseControllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public abstract class BaseAuthorizedController : Controller
    {
    }
}
