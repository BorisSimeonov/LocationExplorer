﻿using System;

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

        public async Task<bool> AddPictureInfoAsync(string fullPath, string contentType, int galleryId, string fileName)
        {
            if (!await ExistsAsync(galleryId))
            {
                return false;
            }
            
            var pictureInfo = new Picture
            {
                Id = fileName,
                ContentType = contentType,
                GalleryId = galleryId,
                Location = fullPath
            };

            await database.Pictures.AddAsync(pictureInfo);
            await database.SaveChangesAsync();

            return true;
        }
    }
}
