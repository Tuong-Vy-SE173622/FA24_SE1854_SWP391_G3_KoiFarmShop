using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Consigments;
using KoiFarmShop.Business.ExceptionHanlder;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.CarePlanBusiness
{
    public class CarePlanService : ICarePlanService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarePlanService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CarePlanResponseDto> GetCarePlanByIdAsync(int id)
        {
            var carePlan = await _unitOfWork.CarePlanRepository.GetByIdAsync(id);
            return _mapper.Map<CarePlanResponseDto>(carePlan);
        }

        public async Task<IEnumerable<CarePlanResponseDto>> GetAllCarePlansAsync()
        {
            var carePlan = await _unitOfWork.CarePlanRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CarePlanResponseDto>>(carePlan);
        }

        public async Task<CarePlanResponseDto> CreateCarePlanAsync(CarePlanCreateDto createDto)
        {
            var carePlan = _mapper.Map<CarePlan>(createDto);

            await _unitOfWork.CarePlanRepository.CreateAsync(carePlan);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CarePlanResponseDto>(carePlan);
        }

        public async Task<CarePlanResponseDto> UpdateCarePlanAsync(int id, CarePlanUpdateDto updateDto)
        {
            var carePlan = await _unitOfWork.CarePlanRepository.GetByIdAsync(id);
            if (carePlan == null) throw new KeyNotFoundException("Care Plan not found");

            //need to check customer id
            _mapper.Map(updateDto, carePlan);

            await _unitOfWork.CarePlanRepository.UpdateAsync(carePlan);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CarePlanResponseDto>(carePlan);
        }

        public async Task<bool> DeleteCarePlanAsync(int id)
        {
            var carePlan = await _unitOfWork.CarePlanRepository.GetByIdAsync(id);
            if (carePlan == null) throw new KeyNotFoundException("Care Plan not found");

            await _unitOfWork.CarePlanRepository.RemoveAsync(carePlan);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
