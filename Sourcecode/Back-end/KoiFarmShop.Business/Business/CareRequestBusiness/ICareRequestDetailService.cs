﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiFarmShop.Business.Dto.CareRequests;

namespace KoiFarmShop.Business.Business.CareRequestBusiness
{
    public interface ICareRequestDetailService
    {
        Task<IEnumerable<CareRequestDetailResponseDto>> GetAllCareRequestDetailAsync();
        Task<CareRequestDetailResponseDto> GetCareRequestDetailByIdAsync(int id);
        Task<CareRequestDetailResponseDto> CreateCareRequestDetailAsync(CareRequestDetailCreateDto createDto);
        Task<CareRequestDetailResponseDto> UpdateCareRequestDetailAsync(int id, CareRequestDetailUpdateDto updateDto);
        Task<bool> DeleteCareRequestDetailAsync(int id);
    }
}
