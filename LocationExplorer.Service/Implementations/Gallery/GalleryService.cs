using System.Linq;
using AutoMapper.QueryableExtensions;
using LocationExplorer.Service.Infrastructure;
using LocationExplorer.Service.Models.Gallery;

namespace LocationExplorer.Service.Implementations.Gallery
{
    using System.Threading.Tasks;
    using Data;
    using Domain.Models;
    using Interfaces.Article;
    using Interfaces.Gallery;
    using Microsoft.EntityFrameworkCore;

    public class GalleryService : IGalleryService
    {
        private const int PicturesPerPage = 5;

        private readonly IArticleService articleService;

        private readonly LocationExplorerDbContext database;

        public GalleryService(LocationExplorerDbContext database, IArticleService articleService)
        {
            this.database = database;
            this.articleService = articleService;
        }

        public Task<bool> ExistsAsync(int id)
            => database.Galleries.AnyAsync(g => g.Id == id);

        public async Task<int> AddAsync(string name, string photographerName, int articleId)
        {
            if (!await articleService.ExistsAsync(articleId))
            {
                return 0;
            }

            var gallery = new Gallery
            {
                Name = name,
                PhotographerName = photographerName,
                ArticleId = articleId
            };

            await database.Galleries.AddAsync(gallery);
            await database.SaveChangesAsync();

            return gallery.Id;
        }

        public async Task<bool> AddPictureInfoAsync(string fileGuid, string contentType, string fullPath, int galleryId)
        {
            if (!await ExistsAsync(galleryId))
            {
                return false;
            }

            var pictureInfo = new Picture
            {
                Id = fileGuid,
                ContentType = contentType,
                GalleryId = galleryId,
                Location = fullPath
            };

            await database.Pictures.AddAsync(pictureInfo);
            await database.SaveChangesAsync();

            return true;
        }

        public async Task<PagingGalleryPicturesServiceModel> GetPictureInfo(int galleryId, int page = 1)
        {
            var gallery = await database.Galleries.Where(x => x.Id == galleryId).FirstOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(gallery?.Name))
            {
                return null;
            }

            var model = new PagingGalleryPicturesServiceModel
            {
                GalleryName = gallery.Name,
                PhotographerName = gallery.PhotographerName,
                Pictures = await database.Pictures
                    .Where(p => p.GalleryId == galleryId)
                    .Skip((page - 1) * PicturesPerPage)
                    .Take(PicturesPerPage)
                    .ProjectTo<PictureInfoServiceModel>()
                    .ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PicturesPerPage,
                    CurrentPage = page,
                    TotalItems = await database.Pictures
                        .Where(p => p.GalleryId == galleryId)
                        .CountAsync()
                }
            };

            return model;
        }
    }
}
