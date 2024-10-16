using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Data.Models;
using KoiFarmShop.Data;
using KoiFarmShop.Business.Business;
using KoiFarmShop.Business.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsignmentRequestController : ControllerBase
    {
        // GET: api/<ConsignmentRequestController>
        private readonly ConsignmentRequestBusiness _consignmentRequestBusiness;

        public ConsignmentRequestController(ConsignmentRequestBusiness consignmentRequestBusiness)
        {
            _consignmentRequestBusiness = consignmentRequestBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsignmentRequest>>> GetRequests()
        {
            //return await _context.Orders.ToListAsync();

            var requests = await _consignmentRequestBusiness.GetAllRequest();

            if (requests == null)
            {
                return NotFound("No requests found.");
            }

            return Ok(requests);
        }

        // GET api/<ConsignmentRequestController>/5
        [HttpGet("{id}/details")]
        public async Task<ActionResult<IEnumerable<ConsignmentDetail>>> GetDetails(int requestId)
        {
            //return await _context.Orders.ToListAsync();

            var requestDetails = await _consignmentRequestBusiness.GetAllConsignmentDetail(requestId);

            if (requestDetails == null)
            {
                return NotFound("No request details found in the request.");
            }

            return Ok(requestDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsignmentRequest>> GetRequest(int id)
        {

            var request = await _consignmentRequestBusiness.GetRequestById(id);

            if (request == null)
            {
                return NotFound($"Request with ID {id} not found.");
            }

            return Ok(request);
        }

        // POST api/<ConsignmentRequestController>
        [HttpPost]
        public async Task<ActionResult<ConsignmentRequestDto>> PostRequest(ConsignmentRequest consignmentRequest)
        {
            var result = await _consignmentRequestBusiness.CreateConsignmentRequest(consignmentRequest);
            return Ok(result);
        }

        [HttpPost("{requestId}/details")]
        public async Task<ActionResult<Order>> PostRequestDetails(ConsignmentDetail consignmentDetail)
        {

            var result = await _consignmentRequestBusiness.CreateConsignmentDetail(consignmentDetail);
            return Ok(result);

        }

        // PUT api/<ConsignmentRequestController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(int id, ConsignmentRequestDto requestDto)
        {

            if (_consignmentRequestBusiness.GetRequestById(id) == null)
            {
                return BadRequest("The ID in the URL does not match the ID in the entity.");
            }

            var result = await _consignmentRequestBusiness.UpdateRequest(requestDto);
            return GenerateActionResult(result);
        }

        // DELETE api/<ConsignmentRequestController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var result = await _consignmentRequestBusiness.RemoveRequest(id);
            return GenerateActionResult(result);
        }

        [HttpDelete("{requestId}/details/{detailId}")]
        public async Task<IActionResult> DeleteDetails(int id)
        {

            var result = await _consignmentRequestBusiness.RemoveDetail(id);
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
