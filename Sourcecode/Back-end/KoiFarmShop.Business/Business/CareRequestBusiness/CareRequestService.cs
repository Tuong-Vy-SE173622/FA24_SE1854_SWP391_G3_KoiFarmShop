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
    public class CareRequestService : ICareRequestService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CareRequestService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CareRequestResponseDto> GetCareRequestByIdAsync(int id)
        {
            var careRequest = await _unitOfWork.CareRequestRepository.GetByIdAsync(id);
            return careRequest != null ? _mapper.Map<CareRequestResponseDto>(careRequest) : null;
        }

        public async Task<IEnumerable<CareRequestResponseDto>> GetAllCareRequestAsync()
        {
            var careRequests = await _unitOfWork.CareRequestRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CareRequestResponseDto>>(careRequests);
        }

        public async Task<CareRequestResponseDto> CreateCareRequestAsync(CareRequestCreateDto createDto)
        {
            var careRequest = _mapper.Map<CareRequest>(createDto);
            await _unitOfWork.CareRequestRepository.CreateAsync(careRequest);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CareRequestResponseDto>(careRequest);
        }

        public async Task<CareRequestResponseDto> UpdateCareRequestAsync(int id, CareRequestUpdateDto updateDto)
        {
            var careRequest = await _unitOfWork.CareRequestRepository.GetByIdAsync(id);
            if (careRequest == null) return null;

            _mapper.Map(updateDto, careRequest);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CareRequestResponseDto>(careRequest);
        }

        public async Task<bool> DeleteCareRequestAsync(int id)
        {
            var careRequest = await _unitOfWork.CareRequestRepository.GetByIdAsync(id);
            if (careRequest == null) return false;

            await _unitOfWork.CareRequestRepository.RemoveAsync(careRequest);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
