namespace LocationExplorer.Domain.Models
{
    public class DestinationTag
    {
        public int Id { get; set; }

        public int DestinationId { get; set; }

        public Destination Destination { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
