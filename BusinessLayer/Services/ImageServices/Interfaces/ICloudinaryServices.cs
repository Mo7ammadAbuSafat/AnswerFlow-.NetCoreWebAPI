namespace BusinessLayer.Services.ImageServices.Interfaces
{
    public interface ICloudinaryServices
    {
        Task DeleteImageFromCloudinary(string CloudinaryIdentifier);
        Task<Tuple<string, string>> UploadImageToCloudinary(string imagePath);
    }
}