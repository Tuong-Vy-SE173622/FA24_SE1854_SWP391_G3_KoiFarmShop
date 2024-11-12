using KoiFarmShop.Business.Business.KoiTypeBusiness;
using KoiFarmShop.Business.Dto.KoiTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace KoiFarmShop.APIService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KoiTypeController : ControllerBase
    {
        private readonly IKoiTypeService _koiTypeService;

        public KoiTypeController(IKoiTypeService koiTypeService)
        {
            _koiTypeService = koiTypeService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KoiTypeDto>>> GetAllKoiTypes()
        {
            var koiTypes = await _koiTypeService.GetAllKoiTypesAsync();
            return Ok(koiTypes);
        }

        [AllowAnonymous]
        [HttpGet("koitypes")]
        public async Task<IActionResult> GetFilteredKoiTypes([FromQuery] KoiTypeFilterDto filterDto)
        {
            var result = await _koiTypeService.GetAllKoiTypesAsync(filterDto);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<KoiTypeDto>> GetKoiTypeById(int id)
        {
            var koiType = await _koiTypeService.GetKoiTypeByIdAsync(id);
            if (koiType == null)
                return NotFound();
            return Ok(koiType);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateKoiType([FromForm] KoiTypeCreateDto koiTypeCreateDto)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var id = await _koiTypeService.CreateKoiTypeAsync(koiTypeCreateDto, currentUser);
            return CreatedAtAction(nameof(GetKoiTypeById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKoiType(int id, [FromForm] KoiTypeUpdateDto koiTypeUpdateDto)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var result = await _koiTypeService.UpdateKoiTypeAsync(id, koiTypeUpdateDto, currentUser);
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
