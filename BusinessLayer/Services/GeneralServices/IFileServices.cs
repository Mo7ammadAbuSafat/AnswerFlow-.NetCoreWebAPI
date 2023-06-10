using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services.GeneralServices
{
    public interface IFileServices
    {
        Task<string> StoreImageToLocalFolder(IFormFile file);
    }
}