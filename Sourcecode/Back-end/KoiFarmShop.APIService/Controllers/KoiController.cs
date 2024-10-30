using KoiFarmShop.Business.Business.KoiBusiness;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Kois;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoiController : ControllerBase
    {
        private readonly IKoiService _koiService;

        public KoiController(IKoiService koiService)
        {
            _koiService = koiService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllKois([FromQuery] KoiFilterDto filterDto)
        {
            var kois = await _koiService.GetAllKoisAsync(filterDto);
            return Ok(kois);
        }

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
        public async Task<IActionResult> CreateKoi([FromBody] KoiCreateDto koiCreateDto)
        {
            var createdId = await _koiService.CreateKoiAsync(koiCreateDto);
            return CreatedAtAction(nameof(GetKoiById), new { id = createdId }, koiCreateDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKoi(int id, [FromBody] KoiUpdateDto koiUpdateDto)
        {
            if (koiUpdateDto == null)
                return BadRequest("Invalid data.");

            var result = await _koiService.UpdateKoiAsync(id, koiUpdateDto);
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

        [HttpPost("{koiTypeId}")]
        public async Task<IActionResult> CreateKoiAsync(int koiTypeId, [FromForm] KoiCreateWithImageDto koiCreateDto)
        {
            ClaimsPrincipal user = HttpContext.User;
            try
            {
                var koiViewModels = new List<KoiCreateWithImageDto>()
            {
                new KoiCreateWithImageDto
                {
                    Origin = koiCreateDto.Origin,
                    Gender = koiCreateDto.Gender,
                    Age = koiCreateDto.Age,
                    Size = koiCreateDto.Size,
                    Price = koiCreateDto.Price,
                    Characteristics = koiCreateDto.Characteristics,
                    FeedingAmountPerDay = koiCreateDto.FeedingAmountPerDay,
                    ScreeningRate = koiCreateDto.ScreeningRate,
                    IsOwnedByFarm = koiCreateDto.IsOwnedByFarm,
                    IsImported = koiCreateDto.IsImported,
                    Generation = koiCreateDto.Generation,
                    IsLocal = koiCreateDto.IsLocal,
                    Note = koiCreateDto.Note,
                    Image = koiCreateDto.Image
            }
            };
                // Call the addFood method from the service layer
                var result = await _koiService.CreateKoiWithImageAsync(koiTypeId, koiViewModels, user);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
