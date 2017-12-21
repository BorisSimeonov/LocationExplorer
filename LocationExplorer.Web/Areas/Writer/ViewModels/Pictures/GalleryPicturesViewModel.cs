namespace LocationExplorer.Web.Areas.Writer.ViewModels.Pictures
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    public class GalleryPicturesViewModel
    {
        public int GalleryId { get; set; }

        public IList<IFormFile> Pictures { get; set; }
    }
}
