using KoiFarmShop.Business.Business.KoiBusiness;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Kois;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KoiController : ControllerBase
    {
        private readonly IKoiService _koiService;

        public KoiController(IKoiService koiService)
        {
            _koiService = koiService;

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllKois([FromQuery] KoiFilterDto filterDto)
        {
            var kois = await _koiService.GetAllKoisAsync(filterDto);
            return Ok(kois);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKoiById(int id)
        {
            var koi = await _koiService.GetKoiByIdAsync(id);
            if (koi == null)
            {
                return NotFound();
            }
            return Ok(koi);
        }

        [HttpPost]
        public async Task<IActionResult> CreateKoi(KoiCreateDto koiCreateDto)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var createdId = await _koiService.CreateKoiAsync(koiCreateDto, currentUser);
            return CreatedAtAction(nameof(GetKoiById), new { id = createdId }, koiCreateDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKoi(int id, KoiUpdateDto koiUpdateDto)
        {
            if (koiUpdateDto == null)
                return BadRequest("Invalid data.");
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;

            var result = await _koiService.UpdateKoiAsync(id, koiUpdateDto,currentUser);
            if (result >= 0)
                return Ok("Koi updated successfully.");
            return NotFound("Koi not found.");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveKoi(int id)
        {
            var success = await _koiService.RemoveKoiAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("all-origins")]
        public async Task<ResultDto> GetAllKoiOrigins()
        {
            var origins = await _koiService.GetAllKoiOrigins();

            ResultDto result = new ResultDto();
            result.success(origins);
            return result;
        } 
    }
}
