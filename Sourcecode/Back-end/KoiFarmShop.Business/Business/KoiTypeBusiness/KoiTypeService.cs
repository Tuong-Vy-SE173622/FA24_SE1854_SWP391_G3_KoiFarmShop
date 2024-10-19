using AutoMapper;
using KoiFarmShop.Business.Dto.KoiTypes;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<int> CreateKoiTypeAsync(KoiTypeCreateDto koiTypeCreateDto)
        {
            var koiType = _mapper.Map<KoiType>(koiTypeCreateDto);
            await _unitOfWork.KoiTypeRepository.CreateAsync(koiType);
            return koiType.KoiTypeId; 
        }

        public async Task<int> UpdateKoiTypeAsync(int koiTypeId, KoiTypeUpdateDto koiTypeUpdateDto)
        {
            var existingKoiType = await _unitOfWork.KoiTypeRepository.GetByIdAsync(koiTypeId);
            if (existingKoiType == null)
            {
                return -1;
            }

            // Map the non-null fields from the DTO to the existing entity
            _mapper.Map(koiTypeUpdateDto, existingKoiType);

            return await _unitOfWork.KoiTypeRepository.UpdateAsync(existingKoiType);
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

        public async Task<PaginatedResult<KoiTypeDto>> GetAllKoiTypesAsync(KoiTypeFilterDto filterDto)
        {
            var query = _unitOfWork.KoiTypeRepository.GetQueryable();

            // Filtering logic based on available fields
            if (!string.IsNullOrWhiteSpace(filterDto.Name))
            {
                query = query.Where(kt => kt.Name.Contains(filterDto.Name));
            }

            // Pagination logic
            var totalRecords = await query.CountAsync();
            var pagedKoiTypes = await query
                .Skip((filterDto.PageNumber - 1) * filterDto.PageSize)
                .Take(filterDto.PageSize)
                .ToListAsync();

            var koiTypeDtos = _mapper.Map<List<KoiTypeDto>>(pagedKoiTypes);

            return new PaginatedResult<KoiTypeDto>
            {
                Data = koiTypeDtos,
                TotalRecords = totalRecords,
                PageNumber = filterDto.PageNumber,
                PageSize = filterDto.PageSize
            };
        }

    }
}
