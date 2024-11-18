using KoiFarmShop.Business.Business.CarePlanBusiness;
using KoiFarmShop.Business.Business.CareRequestBusiness;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.CareRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarePlanController : ControllerBase
    {
        private readonly ICarePlanService _carePlanService;

        public CarePlanController(ICarePlanService carePlanService)
        {
            _carePlanService = carePlanService;
        }
        // GET: api/<CarePlanController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CarePlanResponseDto>>> GetAllCareRequests()
        {
            var results = await _carePlanService.GetAllCarePlansAsync();
            return Ok(results);
        }

        // GET api/<CarePlanController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarePlanResponseDto>> GetCarePlanById(int id)
        {
            var result = await _carePlanService.GetCarePlanByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST api/<CarePlanController>
        [HttpPost]
        public async Task<ActionResult<CareRequestResponseDto>> CreateCarePlan([FromBody] CarePlanCreateDto createDto)
        {
            var result = await _carePlanService.CreateCarePlanAsync(createDto);
            return CreatedAtAction(nameof(GetCarePlanById), new { id = result.CarePlanId }, result);
        }

        // PUT api/<CarePlanController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CarePlanResponseDto>> UpdateCarePlan(int id, [FromBody] CarePlanUpdateDto updateDto)
        {
            var result = await _carePlanService.UpdateCarePlanAsync(id, updateDto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // DELETE api/<CarePlanController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarePlan(int id)
        {
            var deleted = await _carePlanService.DeleteCarePlanAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
