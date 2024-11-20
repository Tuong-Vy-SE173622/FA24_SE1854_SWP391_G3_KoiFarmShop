using KoiFarmShop.Business.Business.CareRequestBusiness;
using KoiFarmShop.Business.Dto.CareRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareRequestDetailController : ControllerBase
    {
        private readonly ICareRequestDetailService _careRequestDetailService;

        public CareRequestDetailController(ICareRequestDetailService careRequestDetailService)
        {
            _careRequestDetailService = careRequestDetailService;
        }
        // GET: api/<CareRequestDetailController>
        [HttpGet("{id}")]
        public async Task<ActionResult<CareRequestDetailResponseDto>> GetCareRequestDetailById(int id)
        {
            var result = await _careRequestDetailService.GetCareRequestDetailByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CareRequestDetailResponseDto>>> GetAllCareRequestDetails()
        {
            var results = await _careRequestDetailService.GetAllCareRequestDetailAsync();
            return Ok(results);
        }

        // POST api/<CareRequestDetailController>
        [HttpPost]
        public async Task<ActionResult<CareRequestDetailResponseDto>> CreateCareRequestDetail([FromForm] CareRequestDetailCreateDto createDto)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var result = await _careRequestDetailService.CreateCareRequestDetailAsync(createDto, currentUser);
            return CreatedAtAction(nameof(GetCareRequestDetailById), new { id = result.CareRequestDetailId }, result);
        }

        // PUT api/<CareRequestDetailController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CareRequestDetailResponseDto>> UpdateCareRequestDetail(int id, [FromForm] CareRequestDetailUpdateDto updateDto)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var result = await _careRequestDetailService.UpdateCareRequestDetailAsync(id, updateDto, currentUser);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // DELETE api/<CareRequestDetailController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCareRequestDetail(int id)
        {
            var deleted = await _careRequestDetailService.DeleteCareRequestDetailAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpPut("mark-complete")]
        [Authorize]
        public async Task<ActionResult<CareRequestDetailResponseDto>> MarkCompletedCareRequestDetail(int careRequestDetailId)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var result = await _careRequestDetailService.MarkCareRequestDetailAsCompletedAsync(careRequestDetailId, currentUser);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
