using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Security;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using System.Security.Claims;

namespace KoiFarmShop.Business.Business.AccountBusiness
{
    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResultDto> AddNewUser(RegisterDto model, ClaimsPrincipal userCreate)
        {
            ResultDto result = new ResultDto();
            try
            {
                // Check if passwords match
                if (model.Password != model.ConfirmPassword)
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.Message = "Passwords do not match";
                    return result;
                }

                // Check if user already exists
                var existingUser = _unitOfWork.AccountRepository.Get(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.Message = "User already exists";
                    return result;
                }
                // Check if email already exists
                var existingEmail = _unitOfWork.AccountRepository.Get(u => u.Email == model.Email);
                if (existingEmail != null)
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.Message = "Email already exists";
                    return result;
                }
                // Check if phone already exists
                var existingPhone = _unitOfWork.AccountRepository.Get(u => u.Username == model.Phone);
                if (existingPhone != null)
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.Message = "Phone already exists";
                    return result;
                }

                // Map the request model to the user entity
                var user = _mapper.Map<User>(model);

                // Generate the next user ID
                user.UserId = await _unitOfWork.AccountRepository.GenerateNewUserId();

                // Hash the password using PasswordHasher
                user.Password = PasswordHasher.HashPassword(model.Password);

                // Set other properties (e.g., CreatedDate, Status, etc.)
                user.CreatedBy = userCreate.FindFirst("UserName")?.Value;
                user.CreatedAt = DateTime.UtcNow;
                user.IsActive = true; // Assuming true is the default status for an active user

                // Add the user to the repository and save changes
                _unitOfWork.AccountRepository.Create(user);
                _unitOfWork.AccountRepository.Save();
                result.IsSuccess = true;
                result.Code = 200;
                result.Message = "Add New User Success";
                return result;
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Code = 400;
                result.Message = e.InnerException.Message;
                return result;
            }
        }

        public async Task<ResultDto> AddNewCustomer(CustomerDto model, ClaimsPrincipal userCreate)
        {
            ResultDto result = new ResultDto();
            try
            {
                // Check if the user has the required role (role ID = 2)
                var userRoleClaim = userCreate.FindFirst("Role"); // Assuming role is stored in a claim
                if (userRoleClaim == null || userRoleClaim.Value != "2")
                {
                    result.IsSuccess = false;
                    result.Code = 403; // Forbidden
                    result.Message = "You do not have permission to create a customer";
                    return result;
                }

                // Check if user exists
                var user = _unitOfWork.AccountRepository.Get(u => u.UserId == model.UserId);
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.Message = "User does not exist";
                    return result;
                }

                // Check if the customer already exists for the user
                var existingCustomer = _unitOfWork.CustomerRepository.Get(c => c.UserId == model.UserId);
                if (existingCustomer != null)
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.Message = "Customer already exists for this user";
                    return result;
                }
                // Map the DTO to the Customer entity
                var customer = _mapper.Map<Customer>(model);

                // Generate the next customer ID if needed
                customer.CustomerId = await _unitOfWork.AccountRepository.GenerateNewCustomerId();

                // Set other properties (e.g., CreatedDate, CreatedBy, etc.)
                customer.CreatedBy = userCreate.FindFirst("UserName")?.Value;
                customer.CreatedAt = DateTime.UtcNow;

                // Add the customer to the repository and save changes
                _unitOfWork.CustomerRepository.Create(customer);
                _unitOfWork.CustomerRepository.Save(); // Ensure to await the save method if it's async

                result.IsSuccess = true;
                result.Code = 200;
                result.Message = "Add New Customer Success";
                return result;
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Code = 400;
                result.Message = e.InnerException?.Message ?? e.Message;
                return result;
            }
        }
    }
}
