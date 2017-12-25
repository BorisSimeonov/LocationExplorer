using LocationExplorer.Service.Infrastructure;

namespace LocationExplorer.Web.Areas.Writer.Controllers.Gallery
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using BaseControllers;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Service.Interfaces.Article;
    using Service.Interfaces.Gallery;
    using Service.Models.Gallery;
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
                ModelState.AddModelError(nameof(model.Pictures), $"Image cannot be bigger than {(ImageFileMaxLength / 1024) / 1000:D} MB.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var skippedPictures = await StorePictures(model.Pictures, model.GalleryId);

            return View(model);
        }

        [HttpPost]
        public async Task<Dictionary<string, string>> StorePictures(IList<IFormFile> pictures, int galleryId)
        {
            var rootPath = Path.Combine(hostingEnvironment.WebRootPath, PicturesBaseDirectoryName);
            var rootGalleryPath = Path.Combine(rootPath, galleryId.ToString());

            if (!Directory.Exists(rootGalleryPath))
            {
                Directory.CreateDirectory(rootGalleryPath);
            }

            var skippedPictures = new Dictionary<string, string>();
            foreach (var file in pictures)
            {
                if (file.Length > 0 && file.Length <= ImageFileMaxLength)
                {
                    var imageGuid = Guid.NewGuid().ToString();
                    var imageNameAndExtension = $"{imageGuid}.{file.FileName.Split('.').Last().ToLower()}";

                    var currentSystemImagePath = Path.Combine(rootGalleryPath, imageNameAndExtension);
                    using (var fs = new FileStream(currentSystemImagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fs);
                    }

                    var appRootImagePath = Path.Combine(PicturesBaseDirectoryName, galleryId.ToString(), imageNameAndExtension);
                    var success = await StorePictureDataInDb(appRootImagePath, imageGuid, file.ContentType, galleryId);
                }
            }

            TempData.AddSuccessMessage();
            return skippedPictures;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details([FromRoute]int id, [FromQuery]int page = 1)
        {
            var detailsModel = await galleryService.GetPictureInfo(id, page);
            var pictures = await GetPicturesFromFs(detailsModel.Pictures);

            return View(new GalleryDetailsViewModel
            {
                Name = detailsModel.GalleryName,
                Photographer = detailsModel.PhotographerName,
                Pictures = pictures,
                PagingInfo = detailsModel.PagingInfo
            });
        }

        private async Task<IList<FileContentResult>> GetPicturesFromFs(IEnumerable<PictureInfoServiceModel> pictures)
        {
            var picturesArray = new List<FileContentResult>();

            foreach (var picture in pictures)
            {
                var x = await System.IO.File.ReadAllBytesAsync(Path.Combine(hostingEnvironment.WebRootPath, picture.Location));
                var file = File(x, picture.ContentType);
                picturesArray.Add(file);
            }

            return picturesArray;
        }

        private async Task<bool> StorePictureDataInDb(string appImagePath, string imageGuid, string contentType, int galleryId)
            => await galleryService.AddPictureInfoAsync(imageGuid, contentType, appImagePath, galleryId);
    }
}
