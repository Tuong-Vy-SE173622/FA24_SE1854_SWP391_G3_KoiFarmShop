using KoiFarmShop.Business.Business.ConsignmentBusiness;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Consigments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> CreateConsignmentRequest([FromBody] ConsignmentRequestCreateDto consignmentRequestCreateDto)
        {
            var result = await _consignmentRequestService.CreateConsignmentRequestAsync(consignmentRequestCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = result.ConsignmentId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsignmentRequest(int id, [FromBody] ConsignmentRequestUpdateDto consignmentRequestUpdateDto)
        {
            var result = await _consignmentRequestService.UpdateConsignmentRequestAsync(id, consignmentRequestUpdateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsignmentRequest(int id)
        {
            await _consignmentRequestService.DeleteConsignmentRequestAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _consignmentRequestService.GetConsignmentRequestByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _consignmentRequestService.GetAllConsignmentRequestsAsync();
            return Ok(result);
        }

        [HttpGet("get-all-by-customer")]
        public async Task<ResultDto> GetAllConsigmentByCustomer(int customerId)
        {
            ResultDto result = new();
            var consignments = await _consignmentRequestService.GetAllConsignmentsByCustomer(customerId);
            result.success(consignments);
            return result;
        }
    }


}
