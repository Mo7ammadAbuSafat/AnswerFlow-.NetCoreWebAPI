using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services.GeneralServices
{
    public class FileServices : IFileServices
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public FileServices(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> StoreImageToLocalFolder(IFormFile file)
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.ContentRootPath, "Uploads");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return filePath;
        }
    }
}
