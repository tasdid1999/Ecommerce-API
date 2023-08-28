namespace ProductAPI.Services.FileServices
{
    public interface IFileService
    {
        Tuple<int, string> SaveImage(IFormFile image);
        bool DeleteImage (string imageUrl);
    }
}
