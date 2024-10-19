using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.OrderBusiness
{
    public class OrderService : IOrderService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto createDto)
        {
            var order = _mapper.Map<Order>(createDto);
            await _unitOfWork.OrderRepository.CreateAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<OrderResponseDto> UpdateOrderAsync(int id, OrderUpdateDto updateDto)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null) return null;

            _mapper.Map(updateDto, order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null) return false;

            await _unitOfWork.OrderRepository.RemoveAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<OrderResponseDto> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            return order != null ? _mapper.Map<OrderResponseDto>(order) : null;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrderAsync()
        {
            var order = await _unitOfWork.OrderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderResponseDto>>(order);
        }
    }
}
