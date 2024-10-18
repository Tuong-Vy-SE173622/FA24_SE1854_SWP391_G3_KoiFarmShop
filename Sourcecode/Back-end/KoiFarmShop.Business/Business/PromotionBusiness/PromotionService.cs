using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.PromotionBusiness
{
    public class PromotionService : IPromotionService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PromotionService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PromotionDto>> GetAllPromotionsAsync()
        {
            //TODO: filtering and pagination

            var p = await _unitOfWork.PromotionRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<PromotionDto>>(p);
        }
        public async Task<PromotionDto> GetPromotionByIdAsync(int id)
        {
            var p = await _unitOfWork.PromotionRepository.GetByIdAsync(id);
            return _mapper.Map<PromotionDto>(p);
        }
        public async Task<ResultDto> GetPromotionList(int? promotionId)
        {
            var result = new ResultDto();
            try
            {
                var p = await _unitOfWork.PromotionRepository.GetPromotions();

                if (promotionId.HasValue)
                {
                    p = p.Where(u => u.PromotionId == promotionId.Value).ToList();
                }

                if (!p.Any())
                {
                    result.Message = "Data not found";
                    result.IsSuccess = false;
                    result.Code = 404;
                }
                else
                {
                    p = p.OrderByDescending(u => u.PromotionId).ToList();

                    var promotionViewModels = p.Select(u => new PromotionDto
                    {
                        PromotionId = u.PromotionId,
                        Description = u.Description,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        IsActive = u.IsActive,
                        Note = u.Note,
                        CreatedAt = u.CreatedAt,
                        CreatedBy = u.CreatedBy,
                        UpdatedAt = u.UpdatedAt,
                        UpdatedBy = u.UpdatedBy
                    }).ToList();

                    result.Data = promotionViewModels;
                    result.Message = "Success";
                    result.IsSuccess = true;
                    result.Code = 200;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsSuccess = false;
                result.Code = 500; // Add the status code for error
            }
            return result;
        }
        //public async Task<int> CreatePromotionAsync(PromotionCreateDto promotionCreateDto)
        //{
        //    var p = _mapper.Map<Promotion>(promotionCreateDto);
        //    await _unitOfWork.PromotionRepository.CreateAsync(p);
        //    return p.PromotionId;
        //}
        public async Task<ResultDto> AddNewPromotion(PromotionCreateDto model, ClaimsPrincipal userCreate)
        {
            ResultDto result = new ResultDto();
            try
            {
                // Map the request model to the user entity
                var p = _mapper.Map<Promotion>(model);

                // Generate the next user ID
                p.PromotionId = await _unitOfWork.PromotionRepository.GenerateNewPromotionId();

                // Set other properties (e.g., CreatedDate, Status, etc.)
                p.CreatedBy = userCreate.FindFirst("UserName")?.Value;
                p.CreatedAt = DateTime.UtcNow;

                // Add the user to the repository and save changes
                _unitOfWork.PromotionRepository.Create(p);
                _unitOfWork.PromotionRepository.Save();
                result.IsSuccess = true;
                result.Code = 200;
                result.Message = "Add New Voucher Success";
                return result;
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Code = 400;
                result.Message = e.Message;
                return result;
            }
        }
        public async Task<ResultDto> UpdatePromotion(int promotionId, PromotionCreateDto promotionDto, ClaimsPrincipal userUpdate)

        {
            var result = new ResultDto();
            try
            {
                var existingPromotion = _unitOfWork.PromotionRepository.Get(x => x.PromotionId == promotionId);
                if (existingPromotion == null)
                {
                    result.IsSuccess = false;
                    result.Code = 404;
                    result.Message = "Can not find voucher";
                    return result;
                }
                // Map the Dto to the existing userid entity
                _mapper.Map(promotionDto, existingPromotion);

                // Update the additional fields
                existingPromotion.Description = promotionDto.Description;
                existingPromotion.DiscountPercentage = promotionDto.DiscountPercentage;
                existingPromotion.Note = promotionDto.Note;
                existingPromotion.UpdatedBy = userUpdate.FindFirst("UserName").Value;
                existingPromotion.UpdatedAt = DateTime.Now;
                _unitOfWork.PromotionRepository.Update(existingPromotion);
                _unitOfWork.PromotionRepository.Save();
                result.IsSuccess = true;
                result.Code = 200;
                result.Message = "Update Voucher Success";
                return result;

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Code = 400;
                result.Message = ex.Message;
                return result;
            }
            return result;
        }
    }
}
