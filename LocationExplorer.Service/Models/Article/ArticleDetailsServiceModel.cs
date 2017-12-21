namespace LocationExplorer.Service.Models.Article
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Common.Mapping;
    using Domain.Models;
    using Gallery;

    public class ArticleDetailsServiceModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [DisplayName("Publish Date")]
        public DateTime PublishDate { get; set; }

        public IEnumerable<GalleryListingServiceModel> Galleries { get; set; }
    }
}
