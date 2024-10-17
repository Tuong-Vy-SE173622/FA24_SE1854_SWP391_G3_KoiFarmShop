﻿using AutoMapper;
using KoiFarmShop.Business.Dto.Kois;
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

        public async Task<int> UpdateKoiAsync(int id, KoiUpdateDto koiUpdateDto)
        {
            var existingKoi = await _unitOfWork.KoiRepository.GetByIdAsync(id);
            if (existingKoi == null)
                return -1; // Koi not found

            _mapper.Map(koiUpdateDto, existingKoi); // This will only update non-null fields
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

            // Pagination logic
            var totalRecords = await query.CountAsync();
            var pagedKois = await query
                .Skip((filterDto.PageNumber - 1) * filterDto.PageSize)
                .Take(filterDto.PageSize)
                .ToListAsync();

            // Mapping the result
            var koiDtos = _mapper.Map<List<KoiDto>>(pagedKois);

            return new PaginatedResult<KoiDto>
            {
                Data = koiDtos,
                TotalRecords = totalRecords,
                PageNumber = filterDto.PageNumber,
                PageSize = filterDto.PageSize
            };
        }

    }


}
