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
        public async Task<IActionResult> CreateConsignmentDetail([FromBody] ConsignmentDetailCreateDto consignmentDetailCreateDto)
        {
            var result = await _consignmentDetailService.CreateConsignmentDetailAsync(consignmentDetailCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = result.ConsignmentDetailId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsignmentDetail(int id, [FromBody] ConsignmentDetailUpdateDto consignmentDetailUpdateDto)
        {
            var result = await _consignmentDetailService.UpdateConsignmentDetailAsync(id, consignmentDetailUpdateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsignmentDetail(int id)
        {
            await _consignmentDetailService.DeleteConsignmentDetailAsync(id);
            return NoContent();
        }

        [HttpGet("{consignmentRequestId}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _consignmentDetailService.GetDetailsByConsignmentRequestIdAsync(id);
            return Ok(result);
        }
    }


}
