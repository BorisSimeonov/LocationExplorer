namespace LocationExplorer.Service.Interfaces.Article
{
    using System;
    using System.Threading.Tasks;
    using Models.Article;

    public interface IArticleService
    {
        Task<int> AddAsync(
            string title, 
            string content, 
            int destinationId, 
            string authorId, 
            DateTime creationDate);

        Task<ArticleDetailsServiceModel> GetByIdAsync(int id);

        Task<PagedArticleListingServiceModel> AllArticlesAsync(int page, int? itemsPerPage = null);

        Task<bool> ExistsAsync(string title);

        Task<bool> ExistsAsync(int id);
    }
}
