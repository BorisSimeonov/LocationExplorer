namespace LocationExplorer.Service.Models.Country
{
    using Common.Mapping;
    using Domain.Models;

    public class CountryListingServiceModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
