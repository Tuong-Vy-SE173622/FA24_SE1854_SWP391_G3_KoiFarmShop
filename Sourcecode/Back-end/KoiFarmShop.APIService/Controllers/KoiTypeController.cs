using KoiFarmShop.Business.Business.KoiTypeBusiness;
using KoiFarmShop.Business.Dto.KoiTypes;
using Microsoft.AspNetCore.Mvc;


namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoiTypeController : ControllerBase
    {
        private readonly IKoiTypeService _koiTypeService;

        public KoiTypeController(IKoiTypeService koiTypeService)
        {
            _koiTypeService = koiTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KoiTypeDto>>> GetAllKoiTypes()
        {
            var koiTypes = await _koiTypeService.GetAllKoiTypesAsync();
            return Ok(koiTypes);
        }

        [HttpGet("koitypes")]
        public async Task<IActionResult> GetFilteredKoiTypes([FromQuery] KoiTypeFilterDto filterDto)
        {
            var result = await _koiTypeService.GetAllKoiTypesAsync(filterDto);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<KoiTypeDto>> GetKoiTypeById(int id)
        {
            var koiType = await _koiTypeService.GetKoiTypeByIdAsync(id);
            if (koiType == null)
                return NotFound();
            return Ok(koiType);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateKoiType(KoiTypeCreateDto koiTypeCreateDto)
        {
            var id = await _koiTypeService.CreateKoiTypeAsync(koiTypeCreateDto);
            return CreatedAtAction(nameof(GetKoiTypeById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKoiType(int id, [FromBody] KoiTypeUpdateDto koiTypeUpdateDto)
        {
            var result = await _koiTypeService.UpdateKoiTypeAsync(id, koiTypeUpdateDto);
            if (result < 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> RemoveKoiType(int id)
        {
            var result = await _koiTypeService.RemoveKoiTypeAsync(id);
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}
