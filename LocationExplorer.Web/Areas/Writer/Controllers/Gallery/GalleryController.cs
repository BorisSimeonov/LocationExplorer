using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace LocationExplorer.Web.Areas.Writer.Controllers.Gallery
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using BaseControllers;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Service.Interfaces.Article;
    using Service.Interfaces.Gallery;
    using ViewModels.Gallery;
    using ViewModels.Pictures;
    using static Infrastructure.WebConstants;

    public class GalleryController : BaseWriterController
    {
        private const string PicturesBaseDirectoryName = "PictureUploads";

        private readonly IArticleService articleService;

        private readonly IGalleryService galleryService;

        private readonly IHostingEnvironment hostingEnvironment;

        public GalleryController(
            IArticleService articleService, 
            IGalleryService galleryService, 
            IHostingEnvironment hostingEnvironment)
        {
            this.articleService = articleService;
            this.galleryService = galleryService;
            this.hostingEnvironment = hostingEnvironment;
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
        public async Task<IActionResult> AddPictures(GalleryPicturesViewModel model)
        {
            var noPicturesSelected = model.Pictures == null || !model.Pictures.Any();

            if (noPicturesSelected)
            {
                ModelState.AddModelError(nameof(model.Pictures), "You have to select at least one picture.");
            }

            if (!noPicturesSelected && model.Pictures.ToList().Any(f => !AllowedFileExtensions.Contains(f.ContentType)))
            {
                ModelState.AddModelError(nameof(model.Pictures), "Only jpg and jpeg image files allowed.");
            }

            if (!noPicturesSelected && model.Pictures.ToList().Any(f => f.Length == 0 || f.Length > ImageFileMaxLength))
            {
                ModelState.AddModelError(nameof(model.Pictures), $"Image cannot be bigger than {(ImageFileMaxLength / 1024) / 1000 :D} MB.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var rootPath = Path.Combine(hostingEnvironment.WebRootPath, PicturesBaseDirectoryName);
            var galleryName = $"{model.GalleryId}";
            var combinedPath = Path.Combine(rootPath, galleryName);

            if (!Directory.Exists(combinedPath))
            {
                Directory.CreateDirectory(combinedPath);
            }

            var skippedPictures = await StorePictures(model.Pictures, combinedPath, model.GalleryId);
            
            return View(model);
        }

        public async Task<Dictionary<string, string>> StorePictures(IList<IFormFile> pictures, string path, int galleryId)
        {
            var skippedPictures = new Dictionary<string, string>();
            foreach (var file in pictures)
            {
                if (file.Length > 0 && file.Length <= ImageFileMaxLength)
                {
                    var contentType = file.ContentType;
                    var fileExtension = file.FileName.Split('.').Last().ToLower();
                    var imageName = System.Guid.NewGuid().ToString();
                    var imageFullName = $"{imageName}.{fileExtension}";

                    var filePath = Path.Combine(path, imageFullName);
                    using (var fs = new FileStream(filePath,FileMode.Create))
                    {
                        await file.CopyToAsync(fs);
                    }

                    var success = await StorePictureDataInDb(path, imageName, contentType, galleryId);
                }
            }

            return skippedPictures;
        }

        public async Task<bool> StorePictureDataInDb(string fullPath, string fileName, string contentType, int galleryId)
            => await galleryService.AddPictureInfoAsync(fullPath, contentType, galleryId, fileName);
    }
}
