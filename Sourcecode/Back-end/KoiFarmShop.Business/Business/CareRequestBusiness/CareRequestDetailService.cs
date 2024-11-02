using AutoMapper;
using KoiFarmShop.Business.Dto.CareRequests;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.CareRequestBusiness
{
    public class CareRequestDetailService : ICareRequestDetailService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CareRequestDetailService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task<CareRequestDetailResponseDto> CreateCareRequestDetailAsync(CareRequestDetailCreateDto createDto)
        {
            var careRequestDetail = _mapper.Map<CareRequestDetail>(createDto);

            careRequestDetail.RequestDetailId = _unitOfWork.CareRequestDetailRepository.GetAll().OrderByDescending(crd => crd.RequestDetailId).Select(crd => crd.RequestDetailId).FirstOrDefault() + 1;

            await _unitOfWork.CareRequestDetailRepository.CreateAsync(careRequestDetail);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CareRequestDetailResponseDto>(careRequestDetail);
        }

        public async Task<CareRequestDetailResponseDto> UpdateCareRequestDetailAsync(int id, CareRequestDetailUpdateDto updateDto)
        {
            var careRequestDetail = await _unitOfWork.CareRequestDetailRepository.GetByIdAsync(id);
            if (careRequestDetail == null) return null;

            _mapper.Map(updateDto, careRequestDetail);
            careRequestDetail.UpdatedAt = DateTime.Now;
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
    }
}
