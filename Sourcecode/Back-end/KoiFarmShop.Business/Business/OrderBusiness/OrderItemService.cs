﻿using AutoMapper;
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
    public class OrderItemService : IOrderItemService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderItemService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderItemResponseDto> CreateOrderItemAsync(OrderItemCreateDto createDto)
        {
            var orderItem = _mapper.Map<OrderItem>(createDto);
            await _unitOfWork.OrderItemRepository.CreateAsync(orderItem);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderItemResponseDto>(orderItem);
        }

        public async Task<OrderItemResponseDto> UpdateOrderItemAsync(int id, OrderItemUpdateDto updateDto)
        {
            var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
            if (orderItem == null) return null;

            _mapper.Map(updateDto, orderItem);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderItemResponseDto>(orderItem);
        }

        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
            if (orderItem == null) return false;

            await _unitOfWork.OrderItemRepository.RemoveAsync(orderItem);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<OrderItemResponseDto> GetOrderItemByIdAsync(int id)
        {
            var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
            return orderItem != null ? _mapper.Map<OrderItemResponseDto>(orderItem) : null;
        }

        public async Task<IEnumerable<OrderItemResponseDto>> GetAllOrderItemAsync()
        {
            var orderItem = await _unitOfWork.OrderItemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderItemResponseDto>>(orderItem);
        }
    }
}
