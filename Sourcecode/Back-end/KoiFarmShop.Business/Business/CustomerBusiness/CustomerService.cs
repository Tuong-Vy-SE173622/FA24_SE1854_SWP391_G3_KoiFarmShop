using AutoMapper;
using KoiFarmShop.Business.Dto.Promotion;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.CustomerBusiness
{
    public class CustomerService : ICustomerService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ListCustomerDto>> GetAllCustomersAsync()
        {
            //TODO: filtering and pagination

            var c = await _unitOfWork.CustomerRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ListCustomerDto>>(c);
        }
        public Customer GetCustomerById(int id)
        {
            try
            {
                var customer = _unitOfWork.CustomerRepository.Get(x => x.CustomerId == id);

                if (customer == null)
                {
                    return null;
                }

                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ResultDto> GetCustomerList(int? customerId, int? userId)
        {
            var result = new ResultDto();
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetCustomers();

                if (customerId.HasValue)
                {
                    customer = customer.Where(u => u.CustomerId == customerId.Value).ToList();
                }

                if (userId.HasValue)
                {
                    customer = customer.Where(u => u.UserId == userId.Value).ToList();
                }

                if (!customer.Any())
                {
                    result.Message = "Data not found";
                    result.IsSuccess = false;
                    result.Code = 404;
                }
                else
                {
                    customer = customer.OrderByDescending(u => u.UserId).ToList();

                    var customerViewModels = customer.Select(u => new ListCustomerDto
                    {
                        CustomerId = u.CustomerId,
                        UserId = u.UserId,
                        Address = u.Address,
                        LoyaltyPoints = u.LoyaltyPoints,
                        CreatedAt = u.CreatedAt,
                        CreatedBy = u.CreatedBy,
                        UpdatedAt = u.UpdatedAt,
                        UpdatedBy = u.UpdatedBy,
                    }).ToList();

                    result.Data = customerViewModels;
                    result.Message = "Success";
                    result.IsSuccess = true;
                    result.Code = 200;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsSuccess = false;
                result.Code = 500; // Add the status code for error
            }
            return result;
        }
    }
}
