namespace LocationExplorer.Service.Models.Article
{
    using System.Collections.Generic;
    using Infrastructure;

    public class PagedArticleListingServiceModel
    {
        public IEnumerable<ArticleListingServiceModel> Articles { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
