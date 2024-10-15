using AutoMapper;
using KoiFarmShop.Business.Business.ConsignmentBusiness;
using KoiFarmShop.Business.Dto.Consigments;
using KoiFarmShop.Data.Models;
using KoiFarmShop.Data;

public class ConsignmentDetailService : IConsignmentDetailService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ConsignmentDetailService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ConsignmentDetailResponseDto> CreateConsignmentDetailAsync(ConsignmentDetailCreateDto createDto)
    {
        var consignmentDetail = _mapper.Map<ConsignmentDetail>(createDto);
        await _unitOfWork.ConsignmentDetailRepository.CreateAsync(consignmentDetail);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ConsignmentDetailResponseDto>(consignmentDetail);
    }

    public async Task<ConsignmentDetailResponseDto> UpdateConsignmentDetailAsync(int id, ConsignmentDetailUpdateDto updateDto)
    {
        var consignmentDetail = await _unitOfWork.ConsignmentDetailRepository.GetByIdAsync(id);
        if (consignmentDetail == null) return null;

        _mapper.Map(updateDto, consignmentDetail);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ConsignmentDetailResponseDto>(consignmentDetail);
    }

    public async Task<bool> DeleteConsignmentDetailAsync(int id)
    {
        var consignmentDetail = await _unitOfWork.ConsignmentDetailRepository.GetByIdAsync(id);
        if (consignmentDetail == null) return false;

        await _unitOfWork.ConsignmentDetailRepository.RemoveAsync(consignmentDetail);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<ConsignmentDetailResponseDto> GetConsignmentDetailByIdAsync(int id)
    {
        var consignmentDetail = await _unitOfWork.ConsignmentDetailRepository.GetByIdAsync(id);
        return consignmentDetail != null ? _mapper.Map<ConsignmentDetailResponseDto>(consignmentDetail) : null;
    }

    public async Task<IEnumerable<ConsignmentDetailResponseDto>> GetAllConsignmentDetailsAsync()
    {
        var consignmentDetails = await _unitOfWork.ConsignmentDetailRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ConsignmentDetailResponseDto>>(consignmentDetails);
    }
}
