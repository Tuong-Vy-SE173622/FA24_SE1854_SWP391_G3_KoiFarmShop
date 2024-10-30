using AutoMapper;
using KoiFarmShop.Business.Dto.Kois;
using KoiFarmShop.Business.ExceptionHanlder;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using KoiFarmShop.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<KoiDto> GetKoiByIdAsync(int koiId)
        {
            var koiDto = await _unitOfWork.KoiRepository.GetQueryable()
                .Where(k => k.KoiId == koiId)
                .Select(k => new KoiDto
                {
                    KoiId = k.KoiId,
                    KoiTypeName = k.KoiType.Name, // mapping directly since i got tired about automapper '-'
                    Origin = k.Origin,
                    Gender = k.Gender,
                    Age = k.Age,
                    Size = k.Size,
                    Price = k.Price,
                    Characteristics = k.Characteristics,
                    FeedingAmountPerDay = k.FeedingAmountPerDay,
                    ScreeningRate = k.ScreeningRate,
                    IsOwnedByFarm = k.IsOwnedByFarm,
                    IsImported = k.IsImported,
                    Generation = k.Generation,
                    IsLocal = k.IsLocal,
                    IsActive = k.IsActive,
                    Note = k.Note,
                    CreatedAt = k.CreatedAt,
                    CreatedBy = k.CreatedBy,
                    UpdatedAt = k.UpdatedAt,
                    UpdatedBy = k.UpdatedBy
                })
                .FirstOrDefaultAsync();


            return koiDto;
        }

        public async Task<int> CreateKoiAsync(KoiCreateDto koiCreateDto)
        {
            var koi = _mapper.Map<Koi>(koiCreateDto);
            await _unitOfWork.KoiRepository.CreateAsync(koi);
            return koi.KoiId;
        }

        public async Task<int> UpdateKoiAsync(int id, KoiUpdateDto koiUpdateDto)
        {
            var existingKoi = await _unitOfWork.KoiRepository.GetByIdAsync(id);
            if (existingKoi == null)
                throw new NotFoundException("Koi not found"); 

            _mapper.Map(koiUpdateDto, existingKoi); // only update non-null fields :3
            return await _unitOfWork.KoiRepository.UpdateAsync(existingKoi);
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

        public async Task<PaginatedResult<KoiDto>> GetAllKoisAsync(KoiFilterDto filterDto)
        {
            var query = _unitOfWork.KoiRepository.GetQueryable();

            // Filtering logic

            if (!string.IsNullOrEmpty(filterDto.TypeName))
            {
                query = query.Where(k => k.KoiType.Name == filterDto.TypeName);
            }

            if (filterDto.MinAge.HasValue)
            {
                query = query.Where(k => k.Age >= filterDto.MinAge.Value);
            }

            if (filterDto.MaxAge.HasValue)
            {
                query = query.Where(k => k.Age <= filterDto.MaxAge.Value);
            }

            if (filterDto.MinSize.HasValue)
            {
                query = query.Where(k => k.Size >= filterDto.MinSize.Value);
            }

            if (filterDto.MaxSize.HasValue)
            {
                query = query.Where(k => k.Size <= filterDto.MaxSize.Value);
            }

            if (filterDto.IsOwnedByFarm.HasValue)
            {
                query = query.Where(k => k.IsOwnedByFarm == filterDto.IsOwnedByFarm.Value);
            }

            if (filterDto.IsImport.HasValue)
            {
                query = query.Where(k => k.IsImported == filterDto.IsImport.Value);
            }

            if (!string.IsNullOrEmpty(filterDto.KoiTypeName))
            {
                query = query.Where(k => k.KoiType.Name == filterDto.KoiTypeName);
            }

            if (filterDto.Gender.HasValue)
            {
                query = query.Where(k => k.Gender == filterDto.Gender);
            }

            if (!string.IsNullOrEmpty(filterDto.Origin))
            {
                query = query.Where(k => filterDto.Origin.Contains(k.Origin));
            }

            // Sorting
            if (filterDto.IsSortedByPrice)
                query = filterDto.IsAscending
                    ? query.OrderBy(k => k.Price)
                    : query.OrderByDescending(k => k.Price);

            // Get the total record count before pagination
            var totalRecords = await query.CountAsync();

            // Perform the Select() method to project the KoiDto fields at the query level
            var pagedKois = await query
                .Skip((filterDto.PageNumber - 1) * filterDto.PageSize)
                .Take(filterDto.PageSize)
                .Select(k => new KoiDto
                {
                    KoiId = k.KoiId,
                    KoiTypeName = k.KoiType.Name, // Mapping KoiType.Name directly :D
                    Origin = k.Origin,
                    Gender = k.Gender,
                    Age = k.Age,
                    Size = k.Size,
                    Price = k.Price,
                    Characteristics = k.Characteristics,
                    FeedingAmountPerDay = k.FeedingAmountPerDay,
                    ScreeningRate = k.ScreeningRate,
                    IsOwnedByFarm = k.IsOwnedByFarm,
                    IsImported = k.IsImported,
                    Generation = k.Generation,
                    IsLocal = k.IsLocal,
                    IsActive = k.IsActive,
                    Note = k.Note,
                    CreatedAt = k.CreatedAt,
                    CreatedBy = k.CreatedBy,
                    UpdatedAt = k.UpdatedAt,
                    UpdatedBy = k.UpdatedBy
                })
                .ToListAsync();

            // Mapping the result
            var koiDtos = _mapper.Map<List<KoiDto>>(pagedKois);

            return new PaginatedResult<KoiDto>
            {
                Data = pagedKois,  
                TotalRecords = totalRecords,
                PageNumber = filterDto.PageNumber,
                PageSize = filterDto.PageSize
            };
        }


        public async Task<HashSet<string>> GetAllKoiOrigins()
        {
            var list = await _unitOfWork.KoiRepository.GetAllAsync();
            HashSet<string> koiOrigins = list
                .Select(k => k.Origin.ToLower())
                .Where(origin => !string.IsNullOrEmpty(origin))
                .ToHashSet();
            return koiOrigins;
        }

    }


}
