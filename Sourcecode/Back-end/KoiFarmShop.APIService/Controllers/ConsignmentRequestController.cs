using KoiFarmShop.Business.Business.ConsignmentBusiness;
using KoiFarmShop.Business.Dto.Consigments;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsignmentRequestController : ControllerBase
    {
        private readonly IConsignmentRequestService _consignmentRequestService;

        public ConsignmentRequestController(IConsignmentRequestService consignmentRequestService)
        {
            _consignmentRequestService = consignmentRequestService;
        }

        [HttpPost]
        public async Task<ActionResult<ConsignmentRequestResponseDto>> CreateConsignmentRequest(ConsignmentRequestCreateDto createDto)
        {
            var result = await _consignmentRequestService.CreateConsignmentRequestAsync(createDto);
            return CreatedAtAction(nameof(GetConsignmentRequestById), new { id = result.ConsignmentId }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ConsignmentRequestResponseDto>> UpdateConsignmentRequest(int id, ConsignmentUpdateDto updateDto)
        {
            var result = await _consignmentRequestService.UpdateConsignmentRequestAsync(id, updateDto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsignmentRequest(int id)
        {
            var deleted = await _consignmentRequestService.DeleteConsignmentRequestAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsignmentRequestResponseDto>> GetConsignmentRequestById(int id)
        {
            var result = await _consignmentRequestService.GetConsignmentRequestByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsignmentRequestResponseDto>>> GetAllConsignmentRequests()
        {
            var results = await _consignmentRequestService.GetAllConsignmentRequestsAsync();
            return Ok(results);
        }
    }

}
