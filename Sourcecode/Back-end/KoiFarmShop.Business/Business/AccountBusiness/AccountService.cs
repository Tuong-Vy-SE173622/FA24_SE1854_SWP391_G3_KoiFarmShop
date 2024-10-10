using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
                //user.Password = PasswordHasher.HashPassword(model.Password);

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
                result.Message = e.Message;
                return result;
            }
        }
    }
}
