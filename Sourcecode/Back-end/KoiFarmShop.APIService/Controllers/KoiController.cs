using KoiFarmShop.Business.Business.KoiBusiness;
using KoiFarmShop.Business.Dto;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateKoi([FromBody] KoiCreateDto koiCreateDto)
        {
            var createdId = await _koiService.CreateKoiAsync(koiCreateDto);
            return CreatedAtAction(nameof(GetKoiById), new { id = createdId }, koiCreateDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKoi(int id, [FromBody] KoiUpdateDto koiUpdateDto)
        {
            if (id != koiUpdateDto.KoiId)
            {
                return BadRequest();
            }
            await _koiService.UpdateKoiAsync(koiUpdateDto);
            return Ok(koiUpdateDto);
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
