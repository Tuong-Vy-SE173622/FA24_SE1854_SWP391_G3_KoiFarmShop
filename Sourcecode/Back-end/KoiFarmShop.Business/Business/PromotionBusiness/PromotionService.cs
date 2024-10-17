using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Promotion GetPromotionById(int id)
        {
            try
            {
                var p = _unitOfWork.PromotionRepository.Get(x => x.PromotionId == id);

                if (p == null)
                {
                    // Handle the case where the user is not found, e.g., return null or throw an exception
                    return null;
                }

                return p;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
    }
}
