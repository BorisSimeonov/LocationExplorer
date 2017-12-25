namespace LocationExplorer.Web.Areas.Writer.ViewModels.Gallery
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Service.Infrastructure;

    public class GalleryDetailsViewModel
    {
        public string Name { get; set; }

        public string Photographer { get; set; }

        public IEnumerable<FileContentResult> Pictures { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
