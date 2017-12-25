namespace LocationExplorer.Service.Models.Gallery
{
    using System.Collections.Generic;
    using Infrastructure;

    public class PagingGalleryPicturesServiceModel
    {
        public IEnumerable<PictureInfoServiceModel> Pictures { get; set; }

        public string GalleryName { get; set; }

        public string PhotographerName { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
