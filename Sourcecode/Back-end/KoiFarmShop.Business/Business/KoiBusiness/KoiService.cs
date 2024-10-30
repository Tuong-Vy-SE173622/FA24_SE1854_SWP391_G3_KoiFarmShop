using AutoMapper;
using KoiFarmShop.Business.Business.Cloudinary;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Kois;
using KoiFarmShop.Business.Dto.KoiTypes;
using KoiFarmShop.Business.ExceptionHanlder;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using KoiFarmShop.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace KoiFarmShop.Business.Business.KoiBusiness
{
    public class KoiService : IKoiService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public KoiService(UnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
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
                query = query.Where(k => k.KoiType.Name.Contains(filterDto.TypeName));
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
                query = query.Where(k => k.Origin.Contains(filterDto.Origin));
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


        public async Task<ResultDto> CreateKoiWithImageAsync(int koiTypeId, List<KoiCreateWithImageDto> koiCreateDto, ClaimsPrincipal userCreate)
        {
            ResultDto result = new ResultDto();
            try
            {
                if (koiCreateDto == null)
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.Message = "Koi request model is null.";
                    return result;
                }
                foreach (var koi in koiCreateDto)
                {
                    var newKoi = _mapper.Map<Koi>(koi);
                    newKoi.KoiTypeId = koiTypeId;
                    newKoi.Origin = koi.Origin;
                    newKoi.Gender = koi.Gender;
                    newKoi.Age = koi.Age;
                    newKoi.Size = koi.Size;
                    newKoi.Price = koi.Price;
                    newKoi.Characteristics = koi.Characteristics;
                    newKoi.FeedingAmountPerDay = koi.FeedingAmountPerDay;
                    newKoi.ScreeningRate = koi.ScreeningRate;
                    newKoi.IsOwnedByFarm = koi.IsOwnedByFarm;
                    newKoi.IsImported = koi.IsImported;
                    newKoi.Generation = koi.Generation;
                    newKoi.IsLocal = koi.IsLocal;
                    newKoi.Note = koi.Note;
                    newKoi.CreatedBy = userCreate.FindFirst("UserName")?.Value;
                    newKoi.CreatedAt = DateTime.Now;
                    if (koi.Image != null)
                    {
                        var imageUrl = await _cloudinaryService.UploadImageAsync(koi.Image);
                        newKoi.Image = imageUrl; // Assuming Koi has an ImageUrl property
                    }
                    _unitOfWork.KoiRepository.Create(newKoi);
                }
                _unitOfWork.KoiRepository.Save();
                result.IsSuccess = true;
                result.Code = 200;
                result.Message = "Add Koi Success";
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Code = 400;
                result.Message = ex.Message;
                return result;
            }
        }
        public async Task<ResultDto> UpdateKoiWithImageAsync(int koiId, KoiUpdateWithImageDto koiUpdateDto, ClaimsPrincipal userUpdate)
        {
            ResultDto result = new ResultDto();
            try
            {
                if (koiUpdateDto == null)
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.Message = "Koi request model is null.";
                    return result;
                }

                var existingKoi = _unitOfWork.KoiRepository.Get(x => x.KoiId == koiId);
                if (existingKoi == null)
                {
                    result.IsSuccess = false;
                    result.Code = 404;
                    result.Message = "Koi not found.";
                    return result;
                }
                // Update food properties except image
                _mapper.Map(koiUpdateDto, existingKoi);
                existingKoi.Origin = koiUpdateDto.Origin;
                existingKoi.Gender = koiUpdateDto.Gender;
                existingKoi.Age = koiUpdateDto.Age;
                existingKoi.Size = koiUpdateDto.Size;
                existingKoi.Characteristics = koiUpdateDto.Characteristics;
                existingKoi.FeedingAmountPerDay = koiUpdateDto.FeedingAmountPerDay;
                existingKoi.ScreeningRate = koiUpdateDto.ScreeningRate;
                existingKoi.IsOwnedByFarm = koiUpdateDto.IsOwnedByFarm;
                existingKoi.IsImported = koiUpdateDto.IsImported;
                existingKoi.Generation = koiUpdateDto.Generation;
                existingKoi.IsLocal = koiUpdateDto.IsLocal;
                existingKoi.Note = koiUpdateDto.Note;


                if (koiUpdateDto.Image != null)
                {
                    // Upload new image and update the existing image URL
                    existingKoi.Image = await _cloudinaryService.UploadImageAsync(koiUpdateDto.Image);
                }
                // Set modified by and date
                var modifiedBy = userUpdate.FindFirst("UserName")?.Value;
                if (string.IsNullOrEmpty(modifiedBy))
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.Message = "User name claim not found.";
                    return result;
                }
                existingKoi.UpdatedBy = modifiedBy;
                existingKoi.UpdatedAt = DateTime.Now;
                // Update and save changes
                _unitOfWork.KoiRepository.Update(existingKoi);
                await _unitOfWork.KoiRepository.SaveAsync();
                result.IsSuccess = true;
                result.Code = 200;
                result.Message = "Update Koi Success";
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Code = 500;
                result.Message = $"Exception: {ex.Message}";
                return result;
            }
        }
    }
}
