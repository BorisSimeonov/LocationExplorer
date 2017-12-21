namespace LocationExplorer.Service.Models.Article
{
    using AutoMapper;
    using Common.Mapping;
    using Domain.Models;

    public class ArticleListingServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorUsername { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper.CreateMap<Article, ArticleListingServiceModel>()
                .ForMember(a => a.AuthorUsername, cfg => cfg.MapFrom(a => a.Author.UserName));
    }
}
