using KoiFarmShop.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.CarePlanBusiness
{
    public interface ICarePlanService
    {
        Task<CarePlanResponseDto> GetCarePlanByIdAsync(int id);
        Task<IEnumerable<CarePlanResponseDto>> GetAllCarePlansAsync();
        Task<CarePlanResponseDto> CreateCarePlanAsync(CarePlanCreateDto createDto);
        Task<CarePlanResponseDto> UpdateCarePlanAsync(int id, CarePlanUpdateDto updateDto);
        Task<bool> DeleteCarePlanAsync(int id);
    }
}
