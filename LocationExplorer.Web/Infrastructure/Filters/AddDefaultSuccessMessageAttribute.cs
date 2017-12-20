namespace LocationExplorer.Web.Infrastructure.Filters
{
    using LearningSystem.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class AddDefaultSuccessMessageAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            var controller = context.Controller as Controller;

            if (context.ModelState.IsValid)
            {
                controller?.TempData.AddSuccessMessage();
            }
            else
            {
                controller?.TempData.AddErrorMessage();
            }
        }
    }
}
