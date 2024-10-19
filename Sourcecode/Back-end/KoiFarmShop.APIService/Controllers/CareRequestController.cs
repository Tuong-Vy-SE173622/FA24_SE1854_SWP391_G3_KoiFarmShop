using Microsoft.AspNetCore.Mvc;
using KoiFarmShop.Business.Business.CareRequestBusiness;
using KoiFarmShop.Business.Dto.CareRequests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareRequestController : ControllerBase
    {
        private readonly ICareRequestService _careRequestService;

        public CareRequestController(ICareRequestService careRequestService)
        {
            _careRequestService = careRequestService;
        }
        // GET: api/<CareRequestController>
        [HttpGet("{id}")]
        public async Task<ActionResult<CareRequestResponseDto>> GetCareRequestById(int id)
        {
            var result = await _careRequestService.GetCareRequestByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CareRequestResponseDto>>> GetAllCareRequests()
        {
            var results = await _careRequestService.GetAllCareRequestAsync();
            return Ok(results);
        }

        // POST api/<CareRequestController>
        [HttpPost]
        public async Task<ActionResult<CareRequestResponseDto>> CreateCareRequest(CareRequestCreateDto createDto)
        {
            var result = await _careRequestService.CreateCareRequestAsync(createDto);
            return CreatedAtAction(nameof(GetCareRequestById), new { id = result.RequestId }, result);
        }

        // PUT api/<CareRequestController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CareRequestResponseDto>> UpdateCareRequest(int id, CareRequestUpdateDto updateDto)
        {
            var result = await _careRequestService.UpdateCareRequestAsync(id, updateDto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // DELETE api/<CareRequestController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCareRequest(int id)
        {
            var deleted = await _careRequestService.DeleteCareRequestAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
