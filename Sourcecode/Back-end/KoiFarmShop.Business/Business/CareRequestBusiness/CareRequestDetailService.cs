using AutoMapper;
using KoiFarmShop.Business.Business.Cloudinary;
using KoiFarmShop.Business.Dto.CareRequests;
using KoiFarmShop.Business.Dto.Kois;
using KoiFarmShop.Business.ExceptionHanlder;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiFarmShop.Data.Models.CareRequest;
using static KoiFarmShop.Data.Models.CareRequestDetail;

namespace KoiFarmShop.Business.Business.CareRequestBusiness
{
    public class CareRequestDetailService : ICareRequestDetailService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public CareRequestDetailService(UnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<CareRequestDetailResponseDto> GetCareRequestDetailByIdAsync(int id)
        {
            var careRequestDetail = await _unitOfWork.CareRequestDetailRepository.GetByIdAsync(id);
            return careRequestDetail != null ? _mapper.Map<CareRequestDetailResponseDto>(careRequestDetail) : null;
        }

        public async Task<IEnumerable<CareRequestDetailResponseDto>> GetAllCareRequestDetailAsync()
        {
            var careRequestDetails = await _unitOfWork.CareRequestDetailRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CareRequestDetailResponseDto>>(careRequestDetails);
        }

        public async Task<CareRequestDetailResponseDto> CreateCareRequestDetailAsync(CareRequestDetailCreateDto createDto, string? currentUser)
        {
            var careRequestDetail = _mapper.Map<CareRequestDetail>(createDto);

            careRequestDetail.CareRequestDetailId = _unitOfWork.CareRequestDetailRepository.GetAll().OrderByDescending(crd => crd.CareRequestDetailId).Select(crd => crd.CareRequestDetailId).FirstOrDefault() + 1;
            careRequestDetail.Status = CareRequestDetailStatus.InProgress.GetDisplayName().ToString();
            if (currentUser == null) throw new UnauthorizedAccessException();
            careRequestDetail.CreatedBy = currentUser;
            var image = createDto.KoiImage;
            if (image != null)
            {
                careRequestDetail.KoiImage = await _cloudinaryService.UploadImageAsync(image);
            }
            //careRequestDetail.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.CareRequestDetailRepository.CreateAsync(careRequestDetail);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CareRequestDetailResponseDto>(careRequestDetail);
        }

        public async Task<CareRequestDetailResponseDto> UpdateCareRequestDetailAsync(int id, CareRequestDetailUpdateDto updateDto, string? currentUser)
        {
            var careRequestDetail = await _unitOfWork.CareRequestDetailRepository.GetByIdAsync(id);
            if (careRequestDetail == null) return null;

            if (updateDto.Status.HasValue)
            {
                if (updateDto.Status == CareRequestDetailStatus.InProgress)
                {
                    careRequestDetail.Status = CareRequestDetailStatus.InProgress.GetDisplayName().ToString();
                }
                else
                {
                    careRequestDetail.Status = updateDto.Status.ToString();
                }
            }
            
            

            _mapper.Map(updateDto, careRequestDetail);
            var image = updateDto.KoiImage;
            if (image != null)
            {
                careRequestDetail.KoiImage = await _cloudinaryService.UploadImageAsync(image);
            }
            //careRequestDetail.UpdatedAt = DateTime.Now;

            if (currentUser == null) throw new UnauthorizedAccessException();
            careRequestDetail.UpdatedBy = currentUser;

            await _unitOfWork.CareRequestDetailRepository.UpdateAsync(careRequestDetail);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CareRequestDetailResponseDto>(careRequestDetail);
        }

        public async Task<bool> DeleteCareRequestDetailAsync(int id)
        {
            var careRequestDetail = await _unitOfWork.CareRequestDetailRepository.GetByIdAsync(id);
            if (careRequestDetail == null) return false;

            await _unitOfWork.CareRequestDetailRepository.RemoveAsync(careRequestDetail);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> MarkCareRequestDetailAsCompletedAsync(int careRequestDetailId, string? currentUser)
        {
            var careRequestDetail = await _unitOfWork.CareRequestDetailRepository.GetByIdAsync(careRequestDetailId);
            if (careRequestDetail == null) 
            {
                throw new NotFoundException($"Care request detail doesn't exist.");
            }

            if (currentUser == null) throw new UnauthorizedAccessException();
            careRequestDetail.UpdatedBy = currentUser;

            careRequestDetail.Status = CareRequestDetailStatus.Completed.ToString();

            await _unitOfWork.CareRequestDetailRepository.UpdateAsync(careRequestDetail);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
