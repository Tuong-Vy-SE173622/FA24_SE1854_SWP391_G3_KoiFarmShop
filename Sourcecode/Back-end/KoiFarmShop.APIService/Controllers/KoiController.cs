using KoiFarmShop.Business.Business.KoiBusiness;
using KoiFarmShop.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoiController : ControllerBase
    {
        private readonly KoiService _koiService;

        public KoiController(KoiService koiService)
        {
            _koiService = koiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKois()
        {
            var kois = await _koiService.GetAllKoisAsync();
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
        public async Task<IActionResult> CreateKoi([FromBody] KoiDto koiDto)
        {
            var createdId = await _koiService.CreateKoiAsync(koiDto);
            return CreatedAtAction(nameof(GetKoiById), new { id = createdId }, koiDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKoi(int id, [FromBody] KoiDto koiDto)
        {
            if (id != koiDto.KoiId)
            {
                return BadRequest();
            }
            await _koiService.UpdateKoiAsync(koiDto);
            return NoContent();
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
    }
}
