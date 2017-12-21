namespace LocationExplorer.Web.Areas.Writer.Controllers.Article
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BaseControllers;
    using Domain.Models;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Service.Interfaces.Article;
    using Service.Interfaces.Destination;
    using ViewModels.Article;

    public class ArticleController : BaseWriterController
    {
        private readonly IDestinationService destinationService;

        private readonly IArticleService articleService;

        private readonly UserManager<User> userManager;

        public ArticleController(
            UserManager<User> userManager,
            IDestinationService destinationService,
            IArticleService articleService) 
        {
            this.userManager = userManager;
            this.destinationService = destinationService;
            this.articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
            => View(new AddArticleViewModel
            {
                Destinations = await GetDestinations()
            });

        [HttpPost]
        [AddDefaultSuccessMessage]
        public async Task<IActionResult> Add(AddArticleViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Title) && await articleService.ExistsAsync(model.Title))
            {
                ModelState.AddModelError(nameof(model.Title),
                    "Article with the same title already exists.");
            }

            if (!await destinationService.ExistsAsync(model.DestinationId))
            {
                ModelState.AddModelError(nameof(model.DestinationId), "Selected destination does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Destinations = await GetDestinations();
                return View(model);
            }

            var authorId = userManager.GetUserId(User);

            await articleService.AddAsync(
                model.Title, 
                model.Content, 
                model.DestinationId,
                authorId,
                DateTime.UtcNow);

            return RedirectToAction(nameof(Add));
        }

        private async Task<IEnumerable<SelectListItem>> GetDestinations()
            => await destinationService.AllAsync();
    }
}
