using KoiFarmShop.Business.Business.Cloudinary;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;

        public StorageController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost("upload/image")]
        public async Task<IActionResult> UploadImage(IFormFile file)  
        {
            if (file == null || file.Length == 0)
                return BadRequest("Image file is required.");

            var imageUrl = await _cloudinaryService.UploadImageAsync(file);
            return Ok(new { Url = imageUrl });
        }

        [HttpPost("upload/video")]
        public async Task<IActionResult> UploadVideo( IFormFile file)  
        {
            if (file == null || file.Length == 0)
                return BadRequest("Video file is required.");

            var videoUrl = await _cloudinaryService.UploadVideoAsync(file);
            return Ok(new { Url = videoUrl });
        }

        [HttpPost("upload/file")]
        public async Task<IActionResult> UploadFile( IFormFile file)  
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required.");

            var fileUrl = await _cloudinaryService.UploadFileAsync(file);
            return Ok(new { Url = fileUrl });
        }
    }

}
