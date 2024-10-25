namespace KoiFarmShop.Business.Business.Cloudinary
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using KoiFarmShop.Business.Config;
    using KoiFarmShop.Business.Utils;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinaryConfig _cloudinaryConfig;

        public CloudinaryService(IOptions<CloudinaryConfig> config)
        {
            _cloudinaryConfig = config.Value;
            if (string.IsNullOrEmpty(_cloudinaryConfig.CloudName) ||
                string.IsNullOrEmpty(_cloudinaryConfig.ApiKey) ||
                string.IsNullOrEmpty(_cloudinaryConfig.ApiSecret))
            {
                throw new ArgumentException("Cloudinary configuration is invalid.");
            }

            var account = new Account(
                _cloudinaryConfig.CloudName,
                _cloudinaryConfig.ApiKey,
                _cloudinaryConfig.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            FileValidation.validate(file);
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = _cloudinaryConfig.Folders.Images,
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false,
                Transformation = new Transformation().Quality("auto").FetchFormat("auto")
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            if (result.Error != null)
                throw new Exception(result.Error.Message);

            return result.SecureUrl.AbsoluteUri;
        }

        public async Task<string> UploadVideoAsync(IFormFile file)
        {
            FileValidation.validate(file);
            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = _cloudinaryConfig.Folders.Videos, // Use folder from config
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false,
                Transformation = new Transformation().Quality("auto").FetchFormat("auto")
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            if (result.Error != null)
                throw new Exception(result.Error.Message);

            return result.SecureUrl.AbsoluteUri;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            FileValidation.validate(file);
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = _cloudinaryConfig.Folders.Files, // Use folder from config
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            if (result.Error != null)
                throw new Exception(result.Error.Message);

            return result.SecureUrl.AbsoluteUri;
        }

    }

}
