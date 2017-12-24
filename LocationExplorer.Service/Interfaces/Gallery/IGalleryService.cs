namespace LocationExplorer.Service.Interfaces.Gallery
{
    using System.Threading.Tasks;

    public interface IGalleryService
    {
        Task<bool> ExistsAsync(int id);

        Task<int> AddAsync(string name, string photographerName, int articleId);

        Task<bool> AddPictureInfoAsync(string fullPath, string contentType, int galleryId, string fileName);
    }
}
