using KoiFarmShop.Business.Business.KoiTypeBusiness;
using KoiFarmShop.Business.Dto;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<KoiTypeDto>> GetKoiTypeById(int id)
        {
            var koiType = await _koiTypeService.GetKoiTypeByIdAsync(id);
            if (koiType == null)
                return NotFound();
            return Ok(koiType);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateKoiType(KoiTypeDto koiTypeDto)
        {
            var id = await _koiTypeService.CreateKoiTypeAsync(koiTypeDto);
            return CreatedAtAction(nameof(GetKoiTypeById), new { id }, id);
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateKoiType(KoiTypeDto koiTypeDto)
        {
            var result = await _koiTypeService.UpdateKoiTypeAsync(koiTypeDto);
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
