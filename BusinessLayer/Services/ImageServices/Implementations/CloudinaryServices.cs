﻿using BusinessLayer.Services.ImageServices.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;

namespace BusinessLayer.Services.ImageServices.Implementations
{
    public class CloudinaryServices : ICloudinaryServices
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryServices()
        {
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            cloudinary = new Cloudinary(Configration.config["Cloudinary:Url"]);
            cloudinary.Api.Secure = true;
        }
        public async Task<Tuple<string, string>> UploadImageToCloudinary(string imagePath)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imagePath),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true
            };
            var results = await cloudinary.UploadAsync(uploadParams);
            string imageUrl = results.SecureUri.ToString();
            string cloudinaryIdentifier = results.PublicId.ToString();

            return Tuple.Create(imageUrl, cloudinaryIdentifier);
        }

        public async Task DeleteImageFromCloudinary(string CloudinaryIdentifier)
        {
            var deletionParams = new DeletionParams(CloudinaryIdentifier)
            {
                ResourceType = ResourceType.Image
            };
            await cloudinary.DestroyAsync(deletionParams);
        }
    }
}
