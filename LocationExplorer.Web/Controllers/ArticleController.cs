namespace LocationExplorer.Web.Controllers
{
    using System.Threading.Tasks;
    using BaseControllers;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Service.Interfaces.Article;

    public class ArticleController : BaseAuthorizedController
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int page = 1)
            => View(await articleService.AllArticlesAsync(page));

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var article = await articleService.GetByIdAsync(id);
            if (article == null)
            {
                TempData.AddErrorMessage("Article does not exist.");
                return RedirectToAction(nameof(All));
            }

            return View(article);
        }
    }
}
