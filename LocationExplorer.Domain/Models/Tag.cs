﻿namespace LocationExplorer.Domain.Models
{
    using System.Collections.Generic;

    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<DestinationTag> Destinations { get; set; }
    }
}
