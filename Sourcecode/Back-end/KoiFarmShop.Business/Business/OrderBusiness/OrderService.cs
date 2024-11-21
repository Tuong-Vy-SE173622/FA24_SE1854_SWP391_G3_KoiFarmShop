using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Consigments;
using KoiFarmShop.Business.Dto.Kois;
using KoiFarmShop.Business.ExceptionHanlder;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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

        public async Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto createDto, string? currentUser)
        {
            var user = _unitOfWork.UserRepository.GetById(createDto.customerId);
            if (user == null)
            {
                throw new NotFoundException("Customer Id does not exist");
            }

            var order = _mapper.Map<Order>(createDto);
            if (currentUser == null) throw new UnauthorizedAccessException();
            order.CreatedBy = currentUser;

            order.OrderId = _unitOfWork.OrderRepository.GetAll().OrderByDescending(o => o.OrderId).Select(o => o.OrderId).FirstOrDefault() + 1;
            //order.OrderDate = DateTime.Now;
            //order.IsActive = true;
            order.PaymentStatus = "Pending";
            order.IsActive = true;
            order.Status = null;

            await _unitOfWork.OrderRepository.CreateAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<OrderResponseDto> UpdateOrderAsync(int id, OrderUpdateDto updateDto, string? currentUser)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }
            var orderItems = await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
            if (orderItems == null)
                throw new NotFoundException("No items found for this order.");

            
            _mapper.Map(updateDto, order);
            if (currentUser == null) throw new UnauthorizedAccessException();
            order.SubAmount = await _unitOfWork.OrderRepository.SumOfOrderItem(id);
            order.UpdatedBy = currentUser;
            order.Vat = Constants.VAT;
            order.VatAmount = order.SubAmount * order.Vat;
            order.TotalAmount = order.SubAmount + order.VatAmount - order.PromotionAmount;
            order.UpdatedAt = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResponseDto>(order);
        }

        public async Task UpdateOrderStatusAfterPaymentAsync(int orderId)
        {
            using var transaction = await _unitOfWork.OrderRepository.BeginTransactionAsync();
            try
            {
                var order = await _unitOfWork.OrderRepository.GetOrderWithDetailsAsync(orderId)
                    ?? throw new NotFoundException("Order does not exist!");

                // Update order status
                order.IsActive = true;
                order.PaymentStatus = "Paid";
                order.Status = "Completed";

                // Update related koi statuses
                foreach (var orderItem in order.OrderItems)
                {
                    var koi = await _unitOfWork.KoiRepository.GetByIdAsync(orderItem.KoiId);
                    koi.IsActive = false;
                }

                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new ApplicationException("Failed to update order status.", ex);
            }
        }

        public async Task<OrderResponseDto> SoftDeleteOrderAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            order.Status = "Inactive";

            order.UpdatedAt = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<bool> HardDeleteOrderAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null) return false;

            await _unitOfWork.OrderRepository.RemoveAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<OrderResponseDto> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithDetailsAsync(id);
            return order != null ? _mapper.Map<OrderResponseDto>(order) : null;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrderAsync()
        {
            var order = await _unitOfWork.OrderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderResponseDto>>(order);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllActiveOrderByIdAsync(int customerId)
        {
            var order = await _unitOfWork.OrderRepository.GetAllAsync();
            var activeOrder = order.Where(o => o.IsActive == true && o.CustomerId == customerId);
            return _mapper.Map<IEnumerable<OrderResponseDto>>(activeOrder);
        }

        public async Task<OrderStatusResponseDto> GetOrderStatusByIdAsync(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            return order != null ? _mapper.Map<OrderStatusResponseDto>(order) : null;
        }

        public async Task<Dictionary<string, double>> GetMonthlySubAmountAsync(DateTime startDate, DateTime endDate)
        {

            var orders = await _unitOfWork.OrderRepository.GetQueryable()
                .Where(order => order.PaymentStatus == "Paid" &&
                                order.OrderDate >= startDate &&
                                order.OrderDate <= endDate)
                .GroupBy(order => new { order.OrderDate.Year, order.OrderDate.Month })
                .Select(group => new
                {
                    Month = $"{group.Key.Month:D2}-{group.Key.Year}", 
                    SubAmount = group.Sum(order => order.SubAmount ?? 0) 
                })
                .ToListAsync();

            return orders.ToDictionary(o => o.Month, o => o.SubAmount);
        }
        
        public async Task<StatsForDashBoard> GetStatsForDashBoardAsync(DateTime startDate, DateTime endDate)
        {
            var totalCustomers = await _unitOfWork.CustomerRepository.CountAllCustomersAsync();
            var totalOrders = await _unitOfWork.OrderRepository.CountAllOrdersAsync();
            var totalKois = await _unitOfWork.KoiRepository.CountAllKoisAsync();
            var revenue = await GetMonthlySubAmountAsync(startDate, endDate);

            return new StatsForDashBoard
            {
                totalCustomers = totalCustomers,
                totalOrders = totalOrders,
                totalKois = totalKois,
                Revenue = revenue
            };
        }
      
    }
}
