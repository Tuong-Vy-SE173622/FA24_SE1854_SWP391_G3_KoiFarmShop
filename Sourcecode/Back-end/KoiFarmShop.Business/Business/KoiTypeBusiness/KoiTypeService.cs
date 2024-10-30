using AutoMapper;
using KoiFarmShop.Business.Business.Cloudinary;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.KoiTypes;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KoiFarmShop.Business.Business.KoiTypeBusiness
{
    public class KoiTypeService : IKoiTypeService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public KoiTypeService(UnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
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


        public async Task<ResultDto> CreateKoiTypeWithImageAsync(KoiTypeCreateWithImageDto koiTypeCreateDto, ClaimsPrincipal userCreate)
        {
            ResultDto result = new ResultDto();
            try
            {
                if (koiTypeCreateDto == null)
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.Message = "koi type request model is null.";
                    return result;
                }
                var newKoi = _mapper.Map<KoiType>(koiTypeCreateDto);
                newKoi.Name = koiTypeCreateDto.Name;
                newKoi.ShortDescription = koiTypeCreateDto.ShortDescription;
                newKoi.OriginHistory = koiTypeCreateDto.OriginHistory;
                newKoi.CategoryDescription = koiTypeCreateDto.CategoryDescription;
                newKoi.FengShui = koiTypeCreateDto.FengShui;
                newKoi.RaisingCondition = koiTypeCreateDto.RaisingCondition;
                newKoi.Note = koiTypeCreateDto.Note;
                newKoi.CreatedBy = userCreate.FindFirst("UserName")?.Value;
                newKoi.CreatedAt = DateTime.Now;
                if (koiTypeCreateDto.Image != null)
                {
                    var imageUrl = await _cloudinaryService.UploadImageAsync(koiTypeCreateDto.Image);
                    newKoi.Image = imageUrl; // Assuming Koi has an ImageUrl property
                }
                await _unitOfWork.KoiTypeRepository.CreateAsync(newKoi);
                //koiIdCounter++; // Tăng biến đếm sau mỗi lần thêm món ăn
                _unitOfWork.KoiTypeRepository.Save();
                result.IsSuccess = true;
                result.Code = 200;
                result.Message = "Add Koi Type Success";
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

        public async Task<ResultDto> UpdateKoiTypeWithImageAsync(int koiTypeId, KoiTypeUpdateWithImageDto koiTypeUpdateDto, ClaimsPrincipal userUpdate)
        {
            ResultDto result = new ResultDto();
            try
            {
                if (koiTypeUpdateDto == null)
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.Message = "Koi type request model is null.";
                    return result;
                }

                var existingKoiType = _unitOfWork.KoiTypeRepository.Get(x => x.KoiTypeId == koiTypeId);
                if (existingKoiType == null)
                {
                    result.IsSuccess = false;
                    result.Code = 404;
                    result.Message = "KoiType not found.";
                    return result;
                }
                // Update food properties except image
                _mapper.Map(koiTypeUpdateDto, existingKoiType);
                existingKoiType.Name = koiTypeUpdateDto.Name;
                existingKoiType.ShortDescription = koiTypeUpdateDto.ShortDescription;
                existingKoiType.CategoryDescription = koiTypeUpdateDto.CategoryDescription;
                existingKoiType.FengShui = koiTypeUpdateDto?.FengShui;
                existingKoiType.RaisingCondition = koiTypeUpdateDto?.RaisingCondition;
                existingKoiType.Note = koiTypeUpdateDto?.Note;

                if (koiTypeUpdateDto.Image != null)
                {
                    // Upload new image and update the existing image URL
                    existingKoiType.Image = await _cloudinaryService.UploadImageAsync(koiTypeUpdateDto.Image);
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
                existingKoiType.UpdatedBy = modifiedBy;
                existingKoiType.UpdatedAt = DateTime.Now;
                // Update and save changes
                _unitOfWork.KoiTypeRepository.Update(existingKoiType);
                await _unitOfWork.KoiTypeRepository.SaveAsync();
                result.IsSuccess = true;
                result.Code = 200;
                result.Message = "Update Koi Type Success";
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
