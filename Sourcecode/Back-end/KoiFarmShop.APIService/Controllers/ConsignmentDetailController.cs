using KoiFarmShop.Business.Business.ConsignmentBusiness;
using KoiFarmShop.Business.Dto.Consigments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KoiFarmShop.APIService.Controllers
{
    [Authorize]
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
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var result = await _consignmentDetailService.CreateConsignmentDetailAsync(consignmentDetailCreateDto, currentUser);
            return CreatedAtAction(nameof(GetById), new { consignmentRequestId = result.ConsignmentDetailId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsignmentDetail(int id, [FromBody] ConsignmentDetailUpdateDto consignmentDetailUpdateDto)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var result = await _consignmentDetailService.UpdateConsignmentDetailAsync(id, consignmentDetailUpdateDto, currentUser);
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
        [HttpGet("test")]
        public string Test()
        {
            var usernameClaim = HttpContext.User.FindFirst("UserName");
             return usernameClaim?.Value;
        }
    }


}
