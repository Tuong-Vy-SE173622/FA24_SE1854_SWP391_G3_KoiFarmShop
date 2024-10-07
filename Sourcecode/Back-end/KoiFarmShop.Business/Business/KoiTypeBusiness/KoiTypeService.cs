using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;

namespace KoiFarmShop.Business.Business.KoiTypeBusiness
{
    public class KoiTypeService : IKoiTypeService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public KoiTypeService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KoiTypeDto>> GetAllKoiTypesAsync()
        {
            var koiTypes = await _unitOfWork.KoiTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<KoiTypeDto>>(koiTypes);
        }

        public async Task<KoiTypeDto> GetKoiTypeByIdAsync(int id)
        {
            var koiType = await _unitOfWork.KoiTypeRepository.GetByIdAsync(id);
            return _mapper.Map<KoiTypeDto>(koiType);
        }

        public async Task<int> CreateKoiTypeAsync(KoiTypeDto koiTypeDto)
        {
            var koiType = _mapper.Map<KoiType>(koiTypeDto);
            await _unitOfWork.KoiTypeRepository.CreateAsync(koiType);
            return koiType.KoiTypeId; 
        }

        public async Task<int> UpdateKoiTypeAsync(KoiTypeDto koiTypeDto)
        {
            var koiType = _mapper.Map<KoiType>(koiTypeDto);
            return await _unitOfWork.KoiTypeRepository.UpdateAsync(koiType);
        }

        public async Task<bool> RemoveKoiTypeAsync(int id)
        {
            var koiType = await _unitOfWork.KoiTypeRepository.GetByIdAsync(id);
            if (koiType != null)
            {
                await _unitOfWork.KoiTypeRepository.RemoveAsync(koiType);
                return true;
            }
            return false;
        }
    }
}
