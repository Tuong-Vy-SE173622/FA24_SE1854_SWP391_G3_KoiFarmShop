using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;

namespace KoiFarmShop.Business.Business.KoiBusiness
{
    public class KoiService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public KoiService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KoiDto>> GetAllKoisAsync()
        {
            var kois = await _unitOfWork.KoiRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<KoiDto>>(kois);
        }

        public async Task<KoiDto> GetKoiByIdAsync(int id)
        {
            var koi = await _unitOfWork.KoiRepository.GetByIdAsync(id);
            return _mapper.Map<KoiDto>(koi);
        }

        public async Task<int> CreateKoiAsync(KoiDto koiDto)
        {
            var koi = _mapper.Map<Koi>(koiDto);
            await _unitOfWork.KoiRepository.CreateAsync(koi);
            return koi.KoiId;
        }

        public async Task<int> UpdateKoiAsync(KoiDto koiDto)
        {
            var koi = _mapper.Map<Koi>(koiDto);
            return await _unitOfWork.KoiRepository.UpdateAsync(koi);
        }

        public async Task<bool> RemoveKoiAsync(int id)
        {
            var koi = await _unitOfWork.KoiRepository.GetByIdAsync(id);
            if (koi != null)
            {
                await _unitOfWork.KoiRepository.RemoveAsync(koi);
                return true;
            }
            return false;
        }
    }


}
