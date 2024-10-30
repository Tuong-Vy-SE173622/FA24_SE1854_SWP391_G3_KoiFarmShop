using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.ExceptionHanlder;
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
            createDto.OrderDate = DateTime.Now;
            createDto.IsActive = true;
            var order = _mapper.Map<Order>(createDto);
            await _unitOfWork.OrderRepository.CreateAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<OrderResponseDto> UpdateOrderAsync(int id, OrderUpdateDto updateDto)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }
            var orderItems = await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
            if (orderItems == null)
                throw new NotFoundException("No items found for this order.");

            updateDto.SubAmount = await _unitOfWork.OrderRepository.SumOfOrderItem(id);

            updateDto.UpdateAt = DateTime.Now;
            _mapper.Map(updateDto, order);
            order.SubAmount = 0;
            order.TotalAmount = order.SubAmount + order.VatAmount - order.TotalAmount;
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
