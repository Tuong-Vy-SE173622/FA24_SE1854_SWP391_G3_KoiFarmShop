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
    public class OrderItemService : IOrderItemService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderItemService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OrderItemResponseDto>> CreateOrderItemsAsync(List<OrderItemCreateDto> createDtos, string? currentUser)
        {
            if (createDtos == null || !createDtos.Any())
            {
                throw new ArgumentException("No items to add.");
            }

            var orderId = createDtos.First().OrderId; // Assuming all items belong to the same order
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            var existingKoiIds = _unitOfWork.OrderItemRepository.GetAll()
                              .Where(oi => oi.OrderId == orderId)
                              .Select(oi => oi.KoiId)
                              .ToHashSet();
            var addedKoiIds = new HashSet<int>();

            double? totalAddedAmount = 0;
            var orderItems = new List<OrderItem>();

            foreach (var createDto in createDtos)
            {
                // Skip if the KoiId is a duplicate in the request or already exists in the order
                if (addedKoiIds.Contains(createDto.KoiId) || existingKoiIds.Contains(createDto.KoiId))
                {
                    continue;
                }

                var koi = await _unitOfWork.KoiRepository.GetByIdAsync(createDto.KoiId);
                if (koi == null)
                {
                    continue;
                }

                var orderItem = _mapper.Map<OrderItem>(createDto);
                orderItem.OrderItemId = _unitOfWork.OrderItemRepository
                    .GetAll()
                    .OrderByDescending(ot => ot.OrderItemId)
                    .Select(ot => ot.OrderItemId)
                    .FirstOrDefault() + 1;
                orderItem.Amount = 1;
                orderItem.Price = koi.Price;

                totalAddedAmount += orderItem.Price;
                if (currentUser == null) throw new UnauthorizedAccessException();
                orderItem.CreatedBy = currentUser;
                await _unitOfWork.OrderItemRepository.CreateAsync(orderItem);
                // Add the orderItems list for returning
                orderItems.Add(orderItem);
                // Add to the addedKoiIds set to prevent re-adding within this batch
                addedKoiIds.Add(createDto.KoiId);
            }
            

            // Update the order's SubAmount
            order.SubAmount = (order.SubAmount ?? 0) + totalAddedAmount;
            order.Vat = Constants.VAT;
            order.VatAmount = order.SubAmount * order.Vat;
            order.TotalAmount = order.SubAmount - order.VatAmount - order.PromotionAmount??0;
            order.UpdatedAt = DateTime.UtcNow;
            order.UpdatedBy = "System";
            _unitOfWork.OrderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync();

            // Map each order item to its response DTO
            return orderItems.Select(item => _mapper.Map<OrderItemResponseDto>(item)).ToList();
        }


        public async Task<OrderItemResponseDto> UpdateOrderItemAsync(int id, OrderItemUpdateDto updateDto, string? currentUser)
        {
            var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
            if (orderItem == null) return null;

            _mapper.Map(updateDto, orderItem);
            var order = await _unitOfWork.OrderRepository.GetByIdAsync((int)orderItem.OrderId);
            order.UpdatedAt = DateTime.Now;

            if (currentUser == null) throw new UnauthorizedAccessException();
            orderItem.UpdatedBy = currentUser;

            await _unitOfWork.OrderRepository.UpdateAsync(order);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderItemResponseDto>(orderItem);
        }

        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
            if (orderItem == null) return false;

            var order = await _unitOfWork.OrderRepository.GetByIdAsync((int)orderItem.OrderId);

            await _unitOfWork.OrderItemRepository.RemoveAsync(orderItem);

            order.SubAmount = order.SubAmount - orderItem.Price;
            _unitOfWork.OrderRepository.Update(order);

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
