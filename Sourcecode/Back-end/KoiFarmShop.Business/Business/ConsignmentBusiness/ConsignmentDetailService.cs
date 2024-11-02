using AutoMapper;
using System.Linq;
using KoiFarmShop.Business.Dto.Consigments;
using KoiFarmShop.Business.ExceptionHanlder;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        public async Task<ConsignmentDetailResponseDto> CreateConsignmentDetailAsync(ConsignmentDetailCreateDto consignmentDetailCreateDto, string? currentUser)
        {
            var koi = await _unitOfWork.KoiRepository.GetByIdAsync(consignmentDetailCreateDto.KoiId);
            if (koi == null) 
                throw new NotFoundException("Koi not found");

            var consignment = await _unitOfWork.ConsignmentRequestRepository.GetByIdAsync(consignmentDetailCreateDto.ConsignmentId);
            if (consignment == null)
                throw new NotFoundException("Consignment not found");

            var consignmentDetail = _mapper.Map<ConsignmentDetail>(consignmentDetailCreateDto);

            if (currentUser == null) throw new UnauthorizedAccessException();
            consignmentDetail.CreatedBy = currentUser;
            await _unitOfWork.ConsignmentDetailRepository.CreateAsync(consignmentDetail);
            await _unitOfWork.SaveChangesAsync();

            await RecalculateConsignmentRequestTotalsAsync(consignment);

            return _mapper.Map<ConsignmentDetailResponseDto>(consignmentDetail);
        }

        public async Task<ConsignmentDetailResponseDto> UpdateConsignmentDetailAsync(int id, ConsignmentDetailUpdateDto consignmentDetailUpdateDto, string? currentUser)
        {
            var consignmentDetail = await _unitOfWork.ConsignmentDetailRepository.GetByIdAsync(id);
            if (consignmentDetail == null) 
                throw new KeyNotFoundException("Consignment Detail not found");

            _mapper.Map(consignmentDetailUpdateDto, consignmentDetail);

            if (consignmentDetail.KoiId is not null)
            {
                var koi = await _unitOfWork.KoiRepository.GetByIdAsync((int)consignmentDetail.KoiId);
                if (koi == null)
                    throw new NotFoundException("Koi not found");
            }
            if (consignmentDetail.ConsignmentId is not null)
            {
                var consignment = await _unitOfWork.ConsignmentRequestRepository.GetByIdAsync((int)consignmentDetail.ConsignmentId);
                if (consignment == null)
                    throw new NotFoundException("Consignment not found");
                await RecalculateConsignmentRequestTotalsAsync(consignment);
            }
            
            if (currentUser == null) throw new UnauthorizedAccessException();
            consignmentDetail.UpdatedBy = currentUser;
            
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

        public async Task<bool> RecalculateConsignmentRequestTotalsAsync(ConsignmentRequest consignment)
        {
            if (consignment == null)
                return false;
            var consignmentDetails = consignment.ConsignmentDetails;

            double subAmount = 0;
            foreach (var detail in consignmentDetails)
            {
                subAmount += detail.SoldPrice ?? 0; 
            }
            consignment.SubAmount = subAmount;
     
            double vat = Constants.VAT; 
            consignment.Vat = vat;
            consignment.VatAmount = subAmount * vat;

            consignment.TotalAmount = subAmount + consignment.VatAmount;

            _unitOfWork.ConsignmentRequestRepository.Update(consignment);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }

}
