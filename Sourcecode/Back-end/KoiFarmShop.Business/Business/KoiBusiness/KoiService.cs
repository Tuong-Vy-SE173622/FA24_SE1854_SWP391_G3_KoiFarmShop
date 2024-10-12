using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Business.Business.KoiBusiness
{
    public class KoiService : IKoiService
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
            //TODO: filtering and pagination

            var kois = await _unitOfWork.KoiRepository.GetAllAsync();
       
            return _mapper.Map<IEnumerable<KoiDto>>(kois);
        }

        public async Task<KoiDto> GetKoiByIdAsync(int id)
        {
            var koi = await _unitOfWork.KoiRepository.GetByIdAsync(id);
            return _mapper.Map<KoiDto>(koi);
        }

        public async Task<int> CreateKoiAsync(KoiCreateDto koiCreateDto)
        {
            var koi = _mapper.Map<Koi>(koiCreateDto);
            await _unitOfWork.KoiRepository.CreateAsync(koi);
            return koi.KoiId;
        }

        public async Task<int> UpdateKoiAsync(KoiUpdateDto koiUpdateDto)
        {
            var koi = _mapper.Map<Koi>(koiUpdateDto);
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

        public IQueryable<Koi> ApplyFilters(IQueryable<Koi> query, KoiFilterDto filterDto)
        {
            if (!string.IsNullOrEmpty(filterDto.Origin))
            {
                query = query.Where(k => k.Origin.Contains(filterDto.Origin));
            }

            if (filterDto.KoiTypeId.HasValue)
            {
                query = query.Where(k => k.KoiTypeId == filterDto.KoiTypeId);
            }

            if (filterDto.IsImported.HasValue)
            {
                query = query.Where(k => k.IsImported == filterDto.IsImported);
            }
            // need to add more filters based on the need, but might consider dynamic filter :D

            return query;
        }
    }


}
