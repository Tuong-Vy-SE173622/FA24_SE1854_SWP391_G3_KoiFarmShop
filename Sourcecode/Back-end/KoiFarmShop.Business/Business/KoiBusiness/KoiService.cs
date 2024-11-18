using AutoMapper;

using KoiFarmShop.Business.Business.Cloudinary;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Kois;
using KoiFarmShop.Business.ExceptionHanlder;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PaginatedResult<KoiDto>> GetAllKoisForAdminAsync(KoiFilterDto filterDto)
        {
            var query = _unitOfWork.KoiRepository.GetQueryable();

            // Filtering logic

            if (!string.IsNullOrEmpty(filterDto.SearchByTypeName))
            {
                query = query.Where(k => k.KoiType.Name.Contains(filterDto.SearchByTypeName));
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

            if (filterDto.Status.HasValue)
            {
                query = query.Where(k => k.Status == filterDto.Status);
            }

            if (filterDto.IsActive.HasValue)
            {
                query = query.Where(k  => k.IsActive == filterDto.IsActive);
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
                    KoiTypeId = k.KoiTypeId,
                    KoiTypeName = k.KoiType.Name, // Mapping KoiType.Name directly :D
                    Origin = k.Origin,
                    Gender = k.Gender,
                    Age = k.Age,
                    Size = k.Size,
                    Image = k.Image,
                    Certificate = k.Certificate, 
                    Price = k.Price,
                    Characteristics = k.Characteristics,
                    FeedingAmountPerDay = k.FeedingAmountPerDay,
                    ScreeningRate = k.ScreeningRate,
                    IsOwnedByFarm = k.IsOwnedByFarm,
                    IsImported = k.IsImported,
                    Generation = k.Generation,
                    IsLocal = k.IsLocal,
                    IsActive = k.IsActive,  // only unsold kois (is active = true means unsold)
                    Status = k.Status,
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

        public async Task<KoiDto> GetKoiByIdAsync(int koiId)
        {
            var koiDto = await _unitOfWork.KoiRepository.GetQueryable()
                .Where(k => k.KoiId == koiId)
                .Select(k => new KoiDto
                {
                    KoiId = k.KoiId,
                    KoiTypeId = k.KoiTypeId,
                    KoiTypeName = k.KoiType.Name, // mapping directly since i got tired about automapper '-'
                    Origin = k.Origin,
                    Gender = k.Gender,
                    Age = k.Age,
                    Size = k.Size,
                    Image = k.Image,
                    Certificate = k.Certificate,
                    Price = k.Price,
                    Characteristics = k.Characteristics,
                    FeedingAmountPerDay = k.FeedingAmountPerDay,
                    ScreeningRate = k.ScreeningRate,
                    IsOwnedByFarm = k.IsOwnedByFarm,
                    IsImported = k.IsImported,
                    Generation = k.Generation,
                    IsLocal = k.IsLocal,
                    IsActive = k.IsActive,
                    Status = k.Status,
                    Note = k.Note,
                    CreatedAt = k.CreatedAt,
                    CreatedBy = k.CreatedBy,
                    UpdatedAt = k.UpdatedAt,
                    UpdatedBy = k.UpdatedBy
                })
                .FirstOrDefaultAsync();


            return koiDto;
        }

        public async Task<int> CreateKoiAsync(KoiCreateDto koiCreateDto, string? currentUser)
        {
            //should check their role instead .-.
            if (String.IsNullOrEmpty(currentUser)) throw new UnauthorizedAccessException("current user is invalid or might not login");

            //check koi type exist
            var koiType = await _unitOfWork.KoiTypeRepository.GetByIdAsync(koiCreateDto.KoiTypeId);
            if (koiType == null)
            {
                throw new NotFoundException("KoiType not found");
            }

            var koi = _mapper.Map<Koi>(koiCreateDto);

            koi.CreatedBy = currentUser;
            koi.CreatedAt = DateTime.Now;
            koi.Status = Koi.KoiStatus.APPROVED;

            var image = koiCreateDto.Image;
            if (image != null)
            {
                koi.Image = _cloudinaryService.UploadImageAsync(image).ToString();
            }

            var certificate = koiCreateDto.Certificate;
            if (certificate != null)
            {
                koi.Certificate = _cloudinaryService.UploadFileAsync(certificate).ToString();
            }

            await _unitOfWork.KoiRepository.CreateAsync(koi);
            return koi.KoiId;
        }

        public async Task<int> CreateKoiForCustomerAsync(KoiCreateForCustomerDto koiCreateDto, string? currentUser)
        {
            if (String.IsNullOrEmpty(currentUser)) throw new UnauthorizedAccessException("current user is invalid or might not login");

            //check koi type exist
            var koiType = await _unitOfWork.KoiTypeRepository.GetByIdAsync(koiCreateDto.KoiTypeId);
            if (koiType == null)
            {
                throw new NotFoundException("KoiType not found");
            }

            var koi = _mapper.Map<Koi>(koiCreateDto);

            koi.CreatedBy = currentUser;
            koi.CreatedAt = DateTime.Now;
            koi.Status = Koi.KoiStatus.PENDING;
            koi.IsOwnedByFarm = false;

            var image = koiCreateDto.Image;
            if (image != null)
            {
                koi.Image = _cloudinaryService.UploadImageAsync(image).ToString();
            }

            var certificate = koiCreateDto.Certificate;
            if (certificate != null)
            {
                koi.Certificate = _cloudinaryService.UploadFileAsync(certificate).ToString();
            }

            await _unitOfWork.KoiRepository.CreateAsync(koi);
            return koi.KoiId;

        }

        public async Task<int> UpdateKoiAsync(int id, KoiUpdateDto koiUpdateDto, string? currentUser)
        {
            if (String.IsNullOrEmpty(currentUser)) throw new UnauthorizedAccessException("current user is invalid or might not login");
            var existingKoi = await _unitOfWork.KoiRepository.GetByIdAsync(id);
            if (existingKoi == null)
                throw new NotFoundException("Koi not found");
            if(koiUpdateDto.KoiTypeId != null)
            {
                var koiType = await _unitOfWork.KoiTypeRepository.GetByIdAsync((int)koiUpdateDto.KoiTypeId);
                if (koiType == null)
                {
                    throw new NotFoundException("KoiType not found");
                }
            }

            _mapper.Map(koiUpdateDto, existingKoi); // only update non-null fields :3


            existingKoi.UpdatedBy = currentUser;
            existingKoi.UpdatedAt = DateTime.Now;
            existingKoi.Status = Koi.KoiStatus.APPROVED; // might need to change 

            var image = koiUpdateDto.Image;
            if (image != null)
            {
                existingKoi.Image = _cloudinaryService.UploadImageAsync(image).ToString();
            }

            var certificate = koiUpdateDto.Certificate;
            if (certificate != null)
            {
                existingKoi.Certificate = _cloudinaryService.UploadFileAsync(certificate).ToString();
            }
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

        public async Task<bool> UpdateForSoldKoiAsync(int id)
        {
            var koi = await _unitOfWork.KoiRepository.GetByIdAsync(id);
            if (koi != null)
            {
                koi.IsActive = false;
                await _unitOfWork.KoiRepository.UpdateAsync(koi);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateForListSoldKoisAsynce(ListSoldKois list)
        {
            var isUpdateSuccessful = true;
            foreach (var koiId in list.ListKoiId) {
                var koi = await _unitOfWork.KoiRepository.GetByIdAsync(koiId);
                if (koi != null)
                {
                    koi.IsActive = false;
                    await _unitOfWork.KoiRepository.UpdateAsync(koi);
                } 
                else return isUpdateSuccessful = false;  
            }
            return isUpdateSuccessful;
        }

        public async Task<PaginatedResult<KoiDto>> GetAllKoisAsync(KoiFilterDto filterDto)
        {
            var query = _unitOfWork.KoiRepository.GetQueryable();

            // Filtering logic

            if (!string.IsNullOrEmpty(filterDto.SearchByTypeName))
            {
                query = query.Where(k => k.KoiType.Name.Contains(filterDto.SearchByTypeName));
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
                    KoiTypeId = k.KoiTypeId,
                    KoiTypeName = k.KoiType.Name, // Mapping KoiType.Name directly :D
                    Origin = k.Origin,
                    Gender = k.Gender,
                    Age = k.Age,
                    Size = k.Size,
                    Image = k.Image,
                    Certificate = k.Certificate,
                    Price = k.Price,
                    Characteristics = k.Characteristics,
                    FeedingAmountPerDay = k.FeedingAmountPerDay,
                    ScreeningRate = k.ScreeningRate,
                    IsOwnedByFarm = k.IsOwnedByFarm,
                    IsImported = k.IsImported,
                    Generation = k.Generation,
                    IsLocal = k.IsLocal,
                    IsActive = true,  // only unsold kois (is active = true means unsold)
                    Status = Koi.KoiStatus.APPROVED,
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

        public async Task<ResultDto> ApproveOrRejectKoiForCareRequest(KoiApproveRequest request, string? currentUser)
        {
            if (String.IsNullOrEmpty(currentUser)) throw new UnauthorizedAccessException("current user is invalid or might not login");
            var existingKoi = await _unitOfWork.KoiRepository.GetByIdAsync(request.KoiId);
            if (existingKoi == null)
                throw new NotFoundException("Koi not found");
            if(request.IsApproved == true)
            {
                existingKoi.Status = Koi.KoiStatus.APPROVED;
            }
            else existingKoi.Status = Koi.KoiStatus.REJECTED;
            existingKoi.IsActive = false; // being in care request means the koi ain't for sale

            return new ResultDto
            {
                IsSuccess = true,
                Data = existingKoi
            };
        }

        public async Task<ResultDto> ApproveOrRejectKoiForConsignment(KoiApproveRequest request, string? currentUser)
        {
            if (String.IsNullOrEmpty(currentUser)) throw new UnauthorizedAccessException("current user is invalid or might not login");
            var existingKoi = await _unitOfWork.KoiRepository.GetByIdAsync(request.KoiId);
            if (existingKoi == null)
                throw new NotFoundException("Koi not found");
            if (request.IsApproved == true)
            {
                existingKoi.Status = Koi.KoiStatus.APPROVED;
            }
            else existingKoi.Status = Koi.KoiStatus.REJECTED;
            existingKoi.IsActive = true; // being in consignment means the koi is for sale

            return new ResultDto
            {
                IsSuccess = true,
                Data = existingKoi
            };
        }

        public async Task<ResultDto> GetAllKoisCreatedByUserId(int userId)
        {
            ResultDto result = new();
            var listKoi = await _unitOfWork.KoiRepository.GetAllKoisCreatedByUser(userId);
            result.success(listKoi);
            return result;
        }

    }
}
