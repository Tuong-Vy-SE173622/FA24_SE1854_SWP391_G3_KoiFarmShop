using AutoMapper;
using KoiFarmShop.Business.Dto.Consigments;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Business.Business.ConsignmentBusiness
{
    public class ConsignmentDetailService : IConsignmentDetailService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConsignmentDetailService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ConsignmentDetailResponseDto> CreateConsignmentDetailAsync(ConsignmentDetailCreateDto consignmentDetailCreateDto)
        {
            var consignmentDetail = _mapper.Map<ConsignmentDetail>(consignmentDetailCreateDto);
            await _unitOfWork.ConsignmentDetailRepository.CreateAsync(consignmentDetail);
            await _unitOfWork.SaveChangesAsync();


            return _mapper.Map<ConsignmentDetailResponseDto>(consignmentDetail);
        }

        public async Task<ConsignmentDetailResponseDto> UpdateConsignmentDetailAsync(int id, ConsignmentDetailUpdateDto consignmentDetailUpdateDto)
        {
            var consignmentDetail = await _unitOfWork.ConsignmentDetailRepository.GetByIdAsync(id);
            if (consignmentDetail == null) throw new KeyNotFoundException("Consignment Detail not found");

            _mapper.Map(consignmentDetailUpdateDto, consignmentDetail);
            await _unitOfWork.ConsignmentDetailRepository.UpdateAsync(consignmentDetail);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ConsignmentDetailResponseDto>(consignmentDetail);
        }

        public async Task DeleteConsignmentDetailAsync(int id)
        {
            var consignmentDetail = await _unitOfWork.ConsignmentDetailRepository.GetByIdAsync(id);
            if (consignmentDetail == null) throw new KeyNotFoundException("Consignment Detail not found");

            await _unitOfWork.ConsignmentDetailRepository.RemoveAsync(consignmentDetail);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ConsignmentDetailResponseDto>> GetDetailsByConsignmentRequestIdAsync(int consignmentRequestId)
        {
            var consignmentDetails = await _unitOfWork.ConsignmentDetailRepository.GetQueryable()
                .Where(cd => cd.ConsignmentId == consignmentRequestId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ConsignmentDetailResponseDto>>(consignmentDetails);
        }
    }

}
