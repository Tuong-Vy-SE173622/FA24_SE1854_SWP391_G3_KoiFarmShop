using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Dto.Consigments;
using KoiFarmShop.Business.Dto.Kois;
using KoiFarmShop.Business.ExceptionHanlder;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
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

        public async Task<OrderResponseDto> UpdateOrderStatusAsync(int id, OrderUpdateStatusDto updateStatusDto)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithDetailsAsync(id);
            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }
            _mapper.Map(updateStatusDto, order);
            if(order.PaymentStatus == "Paid")
            {
                foreach (var orderItem in order.OrderItems)
                {
                    var koi = _unitOfWork.KoiRepository.GetById((int)orderItem.KoiId);
                    //var koiStatusUpdate = new KoiStatusUpdateDto()
                    //{
                    //    IsActive = false
                    //};
                    //_mapper.Map(koiStatusUpdate, koi);
                    koi.IsActive = false;
                    await _unitOfWork.KoiRepository.UpdateAsync(koi);

                }
            }
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResponseDto>(order);
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
      
    }
}
