namespace LocationExplorer.Service.Implementations.Article
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Domain.Models;
    using Infrastructure;
    using Interfaces.Article;
    using Interfaces.Destination;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models.Article;

    public class ArticleService : IArticleService
    {
        private readonly LocationExplorerDbContext database;

        private readonly IDestinationService destinationService;

        private readonly UserManager<User> userManager;

        public ArticleService(LocationExplorerDbContext database, UserManager<User> userManager, IDestinationService destinationService)
        {
            this.database = database;
            this.userManager = userManager;
            this.destinationService = destinationService;
        }

        public async Task<int> AddAsync(string title, string content, int destinationId, string authorId,
            DateTime creationDate)
        {
            if (!await destinationService.ExistsAsync(destinationId))
            {
                return 0;
            }

            var author = await userManager.FindByIdAsync(authorId);

            if (author == null)
            {
                return 0;
            }

            var article = new Article
            {
                Title = title,
                Content = content,
                CreationDate = creationDate,
                AuthorId = authorId,
                DestinationId = destinationId
            };

            await database.Articles.AddAsync(article);
            await database.SaveChangesAsync();

            return article.Id;
        }

        public async Task<ArticleDetailsServiceModel> GetByIdAsync(int id)
            => await database.Articles
                .Where(a => a.Id == id)
                .ProjectTo<ArticleDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<PagedArticleListingServiceModel> AllArticlesAsync(int page, int? itemsPerPage = null)
        {
            itemsPerPage = !itemsPerPage.HasValue || itemsPerPage < 1
                ? PagingConstants.DefaultItemsPerPage
                : itemsPerPage.Value;

            page = page < 1 ? 1 : page;

            return new PagedArticleListingServiceModel
            {
                Articles = await database.Articles
                    .OrderBy(c => c.Title)
                    .Skip((page - 1) * itemsPerPage.Value)
                    .Take(itemsPerPage.Value)
                    .ProjectTo<ArticleListingServiceModel>()
                    .ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = itemsPerPage.Value,
                    TotalItems = await database.Articles.CountAsync()
                }
            };
        }

        public async Task<bool> ExistsAsync(string title)
            => await database.Articles.AnyAsync(a => string.Equals(a.Title, title, StringComparison.InvariantCultureIgnoreCase));

        public async Task<bool> ExistsAsync(int id)
            => await database.Articles.AnyAsync(a => a.Id == id);
    }
}
