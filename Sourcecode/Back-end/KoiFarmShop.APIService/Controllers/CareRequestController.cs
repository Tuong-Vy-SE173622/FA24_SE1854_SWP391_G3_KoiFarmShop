using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Data.Models;
using KoiFarmShop.Business.Business;
using KoiFarmShop.Business.Dto;

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareRequestController : ControllerBase
    {
        // GET: api/<ConsignmentRequestController>
        private readonly CareRequestBusiness _careRequestBusiness;

        public CareRequestController(CareRequestBusiness careRequestBusiness)
        {
            _careRequestBusiness = careRequestBusiness;
        }

        // GET: api/CareRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CareRequest>>> GetRequests()
        {

            var requests = await _careRequestBusiness.GetAllCareRequest();

            if (requests == null)
            {
                return NotFound("No requests found.");
            }

            return Ok(requests);
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<IEnumerable<CareRequestDetail>>> GetDetails(int requestId)
        {

            var requestDetails = await _careRequestBusiness.GetAllCareRequestDetail(requestId);

            if (requestDetails == null)
            {
                return NotFound("No request details found in the care request.");
            }

            return Ok(requestDetails);
        }

        // GET: api/CareRequest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CareRequest>> GetRequest(int id)
        {

            var request = await _careRequestBusiness.GetCareRequestById(id);

            if (request == null)
            {
                return NotFound($"Care request with ID {id} not found.");
            }

            return Ok(request);
        }

        // PUT: api/CareRequest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(int id, CareRequestDto requestDto)
        {

            if (_careRequestBusiness.GetCareRequestById(id) == null)
            {
                return BadRequest("The ID in the URL does not match the ID in the entity.");
            }

            var result = await _careRequestBusiness.UpdateCareRequest(requestDto);
            return GenerateActionResult(result);
        }

        // POST: api/CareRequest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CareRequestDto>> PostRequest(CareRequest careRequest)
        {
            var result = await _careRequestBusiness.CreateCareRequest(careRequest);
            return Ok(result);
        }

        [HttpPost("{requestId}/details")]
        public async Task<ActionResult<Order>> PostRequestDetails(CareRequestDetail careRequestDetail)
        {

            var result = await _careRequestBusiness.CreateCareRequestDetail(careRequestDetail);
            return Ok(result);

        }

        // DELETE: api/CareRequest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var result = await _careRequestBusiness.RemoveCareRequest(id);
            return GenerateActionResult(result);
        }

        [HttpDelete("{requestId}/details/{detailId}")]
        public async Task<IActionResult> DeleteDetails(int id)
        {

            var result = await _careRequestBusiness.RemoveCareDetail(id);
            return GenerateActionResult(result);
        }

        private IActionResult GenerateActionResult(IBusinessResult result)
        {
            return result.Status switch
            {
                200 => Ok(result.Data),
                400 => BadRequest(result),
                404 => NotFound(result),
                _ => StatusCode(500, "An internal server error occurred. Please try again later.")
            };
        }
    }
}
