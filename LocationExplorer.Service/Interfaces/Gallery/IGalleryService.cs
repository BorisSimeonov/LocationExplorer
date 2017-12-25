namespace LocationExplorer.Service.Interfaces.Gallery
{
    using System.Threading.Tasks;
    using Models.Gallery;

    public interface IGalleryService
    {
        Task<bool> ExistsAsync(int id);

        Task<int> AddAsync(string name, string photographerName, int articleId);

        Task<bool> AddPictureInfoAsync(string fileGuid, string contentType, string fullPath, int galleryId);

        Task<PagingGalleryPicturesServiceModel> GetPictureInfo(int galleryId, int page = 1);
    }
}
