namespace LocationExplorer.Web.Areas.Writer.Controllers.BaseControllers
{
    using Data.Infrastructure;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(WebConstants.WriterArea)]
    [Authorize(Roles = DataConstants.WriterRole)]
    public abstract class BaseWriterController : Controller
    {
    }
}
