using KoiFarmShop.Business.Business.ConsignmentBusiness;
using KoiFarmShop.Business.Dto.Consigments;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsignmentDetailController : ControllerBase
        {
            private readonly IConsignmentDetailService _consignmentDetailService;

            public ConsignmentDetailController(IConsignmentDetailService consignmentDetailService)
            {
                _consignmentDetailService = consignmentDetailService;
            }

            [HttpPost]
            public async Task<ActionResult<ConsignmentDetailResponseDto>> CreateConsignmentDetail(ConsignmentDetailCreateDto createDto)
            {
                var result = await _consignmentDetailService.CreateConsignmentDetailAsync(createDto);
                return CreatedAtAction(nameof(GetConsignmentDetailById), new { id = result.ConsignmentDetailId }, result);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult<ConsignmentDetailResponseDto>> UpdateConsignmentDetail(int id, ConsignmentDetailUpdateDto updateDto)
            {
                var result = await _consignmentDetailService.UpdateConsignmentDetailAsync(id, updateDto);
                if (result == null) return NotFound();
                return Ok(result);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteConsignmentDetail(int id)
            {
                var deleted = await _consignmentDetailService.DeleteConsignmentDetailAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<ConsignmentDetailResponseDto>> GetConsignmentDetailById(int id)
            {
                var result = await _consignmentDetailService.GetConsignmentDetailByIdAsync(id);
                if (result == null) return NotFound();
                return Ok(result);
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<ConsignmentDetailResponseDto>>> GetAllConsignmentDetails()
            {
                var results = await _consignmentDetailService.GetAllConsignmentDetailsAsync();
                return Ok(results);
            }
        }

}
