using System;

namespace LocationExplorer.Web.Areas.Writer.Controllers.Gallery
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using BaseControllers;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.DotNet.PlatformAbstractions;
    using Service.Interfaces.Article;
    using Service.Interfaces.Gallery;
    using ViewModels.Gallery;
    using ViewModels.Pictures;
    using static Infrastructure.WebConstants;

    public class GalleryController : BaseWriterController
    {
        private IArticleService articleService;

        private IGalleryService galleryService;

        public GalleryController(IArticleService articleService, IGalleryService galleryService)
        {
            this.articleService = articleService;
            this.galleryService = galleryService;
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            if (!await articleService.ExistsAsync(id))
            {
                TempData.AddErrorMessage("Invalid article.");
                return RedirectToAction("Index", "Home");
            }

            return View(new AddGalleryViewModel { ArticleId = id });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGalleryViewModel model)
        {
            if (!await articleService.ExistsAsync(model.ArticleId))
            {
                TempData.AddErrorMessage("Invalid article.");
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var galleryId = await galleryService.AddAsync(model.Name, model.PhotographerName, model.ArticleId);

            if (galleryId == 0)
            {
                TempData.AddErrorMessage("Save failed.");
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(nameof(AddPictures), galleryId);
        }

        [HttpGet]
        public IActionResult AddPictures(int id)
        {
            return View(new GalleryPicturesViewModel { GalleryId = id });
        }

        [HttpPost]
        public IActionResult AddPictures(GalleryPicturesViewModel model)
        {
            if (model.Pictures.ToList().Any(f => !AllowedFileExtensions.Contains(f.ContentType)))
            {
                ModelState.AddModelError(nameof(model.Pictures), "Only jpg and jpeg image files allowed.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var galleryFolder = $@"\{model.GalleryId}";
            foreach (var file in model.Pictures)
            {
                if (file.Length > 0 && file.Length <= ImageFileMaxLength)
                {
                    var contentType = file.ContentType;
                    var fileExtension = file.FileName.Split('.').Last();
                    var imageId = System.Guid.NewGuid().ToString();
                }
            }
            
            return View(model);
        }
    }
}
