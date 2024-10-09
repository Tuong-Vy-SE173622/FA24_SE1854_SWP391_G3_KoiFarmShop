using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using KoiFarmShop.Data.Models;
using KoiFarmShop.Data;
using KoiFarmShop.Business.Dto;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Azure.Core;

namespace KoiFarmShop.Business.Business
{
    public class OrderBusiness
    {
        private UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderBusiness(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAllOrder()
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllAsync();
                var o = _mapper.Map<List<OrderDto>>(orders);
                if(orders == null)
                {
                    return new BusinessResult(404, "No order found.");
                }

                return new BusinessResult(200, "Successfully retreived all orders.", o);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Failed to retrieve orders: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> GetAllOrderItem(int orderId)
        {
            try
            {
                // Retrieve all order items that belong to the specific orderId
                var orderItems = await _unitOfWork.OrderItemRepository.GetAllAsync(item => item.OrderId == orderId);

                if (orderItems == null || !orderItems.Any())
                {
                    return new BusinessResult(404, "No order items found for the specified order.");
                }

                // Map the order items to DTOs
                var orderItemDtos = _mapper.Map<List<OrderItemDto>>(orderItems);

                return new BusinessResult(200, "Successfully retrieved all order items for the specified order.", orderItemDtos);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Failed to retrieve order items: {ex.Message}");
            }
        }


        public async Task<IBusinessResult> GetOrderById(int id)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetByIdAsync(id);
                var a = _mapper.Map<OrderDto>(orders);
                if (orders == null)
                {
                    return new BusinessResult(404, "No order found.");
                }

                return new BusinessResult(200, "Successfully retrieved all order with specified ID.", a);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, $"Failed to retrieve orders: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> CreateOrder(Order order)
        {
            await _unitOfWork.BeginOrderAsync();
            try
            {
                _unitOfWork.OrderRepository.Create(order);
                await _unitOfWork.CommitOrderAsync();

                return new BusinessResult(200, "Order created successfully.", order);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackOrderAsync();
                return new BusinessResult(500, $"Failed to create order request: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> CreateOrderItem(OrderItem orderItem)
        {

            await _unitOfWork.BeginOrderAsync();
            try
            {
                _unitOfWork.OrderItemRepository.Create(orderItem);
                await _unitOfWork.CommitOrderAsync();

                return new BusinessResult(200, "Order item created successfully.", orderItem);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackOrderAsync();
                return new BusinessResult(500, $"Failed to create order item request: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> UpdateOrder(OrderDto orderDto)
        {
            await _unitOfWork.BeginOrderAsync();
            try
            {
                var existingOrder = await _unitOfWork.OrderRepository.GetByIdAsync(orderDto.OrderId);

                if (existingOrder == null)
                {
                    return new BusinessResult(404, $"Order with ID {orderDto.OrderId} not found.");
                }

                existingOrder.SubAmount = existingOrder.OrderItems.Sum(item => item.Price * item.Amount);
                existingOrder.Vat = orderDto.Vat;

                orderDto.VatAmount = existingOrder.SubAmount * orderDto.Vat;
                existingOrder.VatAmount = orderDto.VatAmount;

                existingOrder.PromotionAmount = orderDto.PromotionAmount;

                orderDto.TotalAmount = existingOrder.SubAmount + existingOrder.VatAmount - existingOrder.PromotionAmount;
                existingOrder.TotalAmount = orderDto.TotalAmount;
                existingOrder.PaymentMethod = orderDto.PaymentMethod;
                existingOrder.PaymentStatus = orderDto.PaymentStatus;
                existingOrder.IsActive = orderDto.IsActive;
                existingOrder.Note = orderDto.Note;
                existingOrder.Status = orderDto.Status;
                existingOrder.UpdatedAt = DateTime.Now;
                existingOrder.UpdatedBy = orderDto.UpdatedBy;


                _unitOfWork.OrderRepository.Update(existingOrder);
                await _unitOfWork.CommitOrderAsync();

                return new BusinessResult(200, $"Order with ID {orderDto.OrderId} updated successfully.");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackOrderAsync();
                return new BusinessResult(500, $"Failed to update order with ID {orderDto.OrderId}: {ex.Message}");
            }

        }

        public async Task<IBusinessResult> UpdateOrderItem(OrderItemDto orderItemDto)
        {
            await _unitOfWork.BeginOrderAsync();
            try
            {
                var existingOrderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(orderItemDto.OrderItemId);

                if (existingOrderItem == null)
                {
                    return new BusinessResult(404, $"Order item with ID {orderItemDto.OrderItemId} not found.");
                }

                existingOrderItem.Amount = orderItemDto.Amount;
                existingOrderItem.Price = orderItemDto.Price;
                existingOrderItem.IsActive = orderItemDto.IsActive;
                existingOrderItem.Note = orderItemDto.Note;
                existingOrderItem.UpdatedAt = DateTime.Now;
                existingOrderItem.UpdatedBy = orderItemDto.UpdatedBy;


                _unitOfWork.OrderItemRepository.Update(existingOrderItem);
                await _unitOfWork.CommitOrderAsync();

                return new BusinessResult(200, $"Order item with ID {orderItemDto.OrderItemId} updated successfully.");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackOrderAsync();
                return new BusinessResult(500, $"Failed to update order item with ID {orderItemDto.OrderItemId}: {ex.Message}");
            }

        }

        public async Task<IBusinessResult> RemoveOrder(int orderId)
        {
            await _unitOfWork.BeginOrderAsync();
            try
            {
                var existingOrder = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
                if (existingOrder == null)
                {
                    return new BusinessResult(404, $"Order item with ID {orderId} not found.");
                }

                _unitOfWork.OrderRepository.Remove(existingOrder);
                await _unitOfWork.CommitOrderAsync();

                return new BusinessResult(200, $"Order item with ID {orderId} deleted successfully.");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackOrderAsync();
                return new BusinessResult(500, $"Failed to delete order item with ID {orderId}: {ex.Message}");
            }
        }

        public async Task<IBusinessResult> RemoveOrderItem(int orderItemId)
        {
            await _unitOfWork.BeginOrderAsync();
            try
            {
                var existingOrderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(orderItemId);
                if (existingOrderItem == null)
                {
                    return new BusinessResult(404, $"Order item with ID {orderItemId} not found.");
                }

                _unitOfWork.OrderItemRepository.Remove(existingOrderItem);
                await _unitOfWork.CommitOrderAsync();

                return new BusinessResult(200, $"Order item with ID {orderItemId} deleted successfully.");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackOrderAsync();
                return new BusinessResult(500, $"Failed to delete order item with ID {orderItemId}: {ex.Message}");
            }
        }
    }
}
