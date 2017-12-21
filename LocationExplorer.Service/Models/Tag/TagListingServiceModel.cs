namespace LocationExplorer.Service.Models.Tag
{
    using Common.Mapping;
    using Domain.Models;

    public class TagListingServiceModel : IMapFrom<Tag>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
