using Microsoft.AspNetCore.Mvc;
using KoiFarmShop.Business.Business.CareRequestBusiness;
using KoiFarmShop.Business.Dto.CareRequests;
using Microsoft.AspNetCore.Authorization;
using KoiFarmShop.Business.Dto.Kois;

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

        [HttpGet("customer/{id}")]
        public async Task<ActionResult<CareRequestResponseDto>> GetCareRequestByCustomerId(int customerId)
        {
            var result = await _careRequestService.GetCareRequestByCustomerIdAsync(customerId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CareRequestResponseDto>>> GetAllCareRequests([FromQuery] CareRequestFilterDto filterDto)
        {
            var results = await _careRequestService.GetAllCareRequestAsync(filterDto);
            return Ok(results);
        }

        // POST api/<CareRequestController>
        [HttpPost]
        public async Task<ActionResult<CareRequestResponseDto>> CreateCareRequest([FromForm] CareRequestCreateDto createDto)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var result = await _careRequestService.CreateCareRequestAsync(createDto, currentUser);
            return CreatedAtAction(nameof(GetCareRequestById), new { id = result.CareRequestId }, result);
        }

        // PUT api/<CareRequestController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CareRequestResponseDto>> UpdateCareRequest(int id, [FromForm] CareRequestUpdateDto updateDto)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var result = await _careRequestService.UpdateCareRequestAsync(id, updateDto, currentUser);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("approve")]
        [Authorize]
        public async Task<ActionResult<CareRequestResponseDto>> ApproveCareRequest(int careRequestId)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var result = await _careRequestService.ApproveCareRequestAsync(careRequestId, currentUser);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("reject")]
        [Authorize]
        public async Task<ActionResult<CareRequestResponseDto>> RejectCareRequest(int careRequestId)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var result = await _careRequestService.RejectCareRequestAsync(careRequestId, currentUser);
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
