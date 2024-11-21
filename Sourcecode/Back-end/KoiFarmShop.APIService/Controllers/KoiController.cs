using KoiFarmShop.Business.Business.ConsignmentBusiness;
using KoiFarmShop.Business.Business.KoiBusiness;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Kois;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoiFarmShop.APIService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KoiController : ControllerBase
    {
        private readonly IKoiService _koiService;

        private readonly IConsignmentRequestService _consignmentRequestService;
        public KoiController(IKoiService koiService, IConsignmentRequestService consignmentRequestService)
        {
            _koiService = koiService;
            _consignmentRequestService = consignmentRequestService;

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllKois([FromQuery] KoiFilterDto filterDto)
        {
            var kois = await _koiService.GetAllKoisAsync(filterDto);
            return Ok(kois);
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetAllKoisForAdmin([FromQuery] KoiFilterDto filterDto)
        {
            var kois = await _koiService.GetAllKoisForAdminAsync(filterDto);
            return Ok(kois);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKoiById(int id)
        {
            var koi = await _koiService.GetKoiByIdAsync(id);
            if (koi == null)
            {
                return NotFound();
            }
            return Ok(koi);
        }

        [HttpPost]
        public async Task<ResultDto> CreateKoi([FromForm] KoiCreateDto koiCreateDto)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var createdId = await _koiService.CreateKoiAsync(koiCreateDto, currentUser);
            ResultDto result = new();
            result.success(createdId,"New koi has been created with id : " + createdId);
            return result;
        }

        [HttpPost("for-customer")]
        public async Task<ResultDto> CreateKoiForCustomer([FromForm] KoiCreateForCustomerDto koiCreateDto)
        {
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            var createdId = await _koiService.CreateKoiForCustomerAsync(koiCreateDto, currentUser);
            ResultDto result = new();
            result.success(createdId, "New koi has been created with id : " + createdId);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKoi(int id, [FromForm] KoiUpdateDto koiUpdateDto)
        {
            if (koiUpdateDto == null)
                return BadRequest("Invalid data.");
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;

            var result = await _koiService.UpdateKoiAsync(id, koiUpdateDto, currentUser);
            if (result >= 0)
                return Ok("Koi updated successfully.");
            return NotFound("Koi not found.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveKoi(int id)
        {
            var success = await _koiService.RemoveKoiAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("all-origins")]
        public async Task<ResultDto> GetAllKoiOrigins()
        {
            var origins = await _koiService.GetAllKoiOrigins();

            ResultDto result = new();
            result.success(origins);
            return result;
        }

        [HttpPut("update-for-list-sold-kois")]
        public async Task<ResultDto> UpdateStatusForSoldKoi([FromForm] ListSoldKois list)
        {
            ResultDto result = new();
            if (list == null)
            {
                result.error("Empty List of Koi ids !");
                return result;
            }
            var isUpdateSuccessful = await _koiService.UpdateForListSoldKoisAsynce(list);
            if (isUpdateSuccessful)
            {
                foreach(var id in list.ListKoiId)
                {
                    var koi = await _koiService.GetKoiWithConsignment(id);
                    if (koi == null)
                    {
                        result.error("Update failed dued to koi id does not exist");
                        return result;
                    }
                    var consignment = koi.ConsignmentRequest;
                    if( consignment != null)
                    {
                        await _consignmentRequestService.CreateTransactionAfterConsignmentCompleted(consignment.ConsignmentId);
                    }
                }
                result.success();
                return result;
            }
            else
            {
                result.error("Update failed!");
                return result;
            }
        }

        [HttpPut("approve-for-care-request")]
        public async Task<ResultDto> ApproveKoiForCareRequest([FromForm] KoiApproveRequest request)
        {
            ResultDto result = new();
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            if (currentUser == null)
            {
                result.error("user not login or invalid user role to access this endpoint");
                return result;
            }
            return await _koiService.ApproveOrRejectKoiForCareRequest(request, currentUser);
        }

        [HttpPut("approve-for-consignment")]
        public async Task<ResultDto> ApproveKoiForConsignment([FromForm] KoiApproveRequest request)
        {
            ResultDto result = new();
            var currentUser = HttpContext.User?.FindFirst("UserName")?.Value;
            if (currentUser == null)
            {
                result.error("user not login or invalid user role to access this endpoint");
                return result;
            }
            return await _koiService.ApproveOrRejectKoiForConsignment(request, currentUser);
        }

        [HttpGet("koi-created-by-user")]
        public async Task<ResultDto> GetAllKoisCreatedByUserId([FromQuery] int userId, bool isInConsignment, bool isInCareRequest)
        {
            return await _koiService.GetAllKoisCreatedByUserId(userId, isInConsignment, isInCareRequest);
        }
        [AllowAnonymous]
        [HttpGet("test")]
        public async Task<string> test()
        {
            return await _koiService.Test();
        }
    }
}

