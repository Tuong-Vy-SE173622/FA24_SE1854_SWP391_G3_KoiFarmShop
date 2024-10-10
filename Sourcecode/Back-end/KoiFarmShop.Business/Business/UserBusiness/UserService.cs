using AutoMapper;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;
using KoiFarmShop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.UserBusiness
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public User GetUserById(int uid)
        {
            try
            {
                var user = _unitOfWork.UserRepository.Get(x => x.UserId == uid);

                if (user == null)
                {
                    // Handle the case where the user is not found, e.g., return null or throw an exception
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public User GetUserByUserName(string userName)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.Username == userName);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<ResultDto> GetUserList(int? userId, string? name, string? email, string? phone, string? firstName, string? lastName)
        {
            var result = new ResultDto();
            try
            {
                var users = await _unitOfWork.UserRepository.GetUsers();

                if (userId.HasValue)
                {
                    users = users.Where(u => u.UserId == userId.Value).ToList();
                }

                if (!string.IsNullOrEmpty(name))
                {
                    users = users.Where(u => u.Username.ToLowerInvariant() == name.ToLowerInvariant()).ToList();
                }

                if (!string.IsNullOrEmpty(email))
                {
                    users = users.Where(u => u.Email.ToLowerInvariant() == email.ToLowerInvariant()).ToList();
                }

                if (!string.IsNullOrEmpty(phone))
                {
                    users = users.Where(u => u.Phone.ToLowerInvariant() == phone.ToLowerInvariant()).ToList();
                }

                if (!string.IsNullOrEmpty(firstName))
                {
                    users = users.Where(u => u.FirstName.ToLowerInvariant() == firstName.ToLowerInvariant()).ToList();
                }

                if (!string.IsNullOrEmpty(lastName))
                {
                    users = users.Where(u => u.LastName.ToLowerInvariant() == lastName.ToLowerInvariant()).ToList();
                }

                if (!users.Any())
                {
                    result.Message = "Data not found";
                    result.IsSuccess = false;
                    result.Code = 404;
                }
                else
                {
                    users = users.OrderByDescending(u => u.UserId).ToList();

                    var userViewModels = users.Select(u => new UserDto
                    {
                        UserId = u.UserId,
                        Username = u.Username,
                        Password = u.Password,
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Phone = u.Phone,
                        Role = u.Role,
                        CreatedAt = u.CreatedAt
                    }).ToList();

                    result.Data = userViewModels;
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
        public User SearchUser(string keyword)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.Username.Contains(keyword.Trim())
                                                || u.Email.Contains(keyword.Trim())
                                                || u.Phone.Contains(keyword.Trim()));
            if (user != null)
            {
                return user;
            }
            return null;
        }
        public async Task<ResultDto> SearchUserByKeyword(string keyword)
        {
            var result = new ResultDto();
            try
            {
                var user = SearchUser(keyword);
                result.IsSuccess = true;
                result.Code = 200;
                result.Data = user;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                result.Code = 404;
                return result;
            }
        }
        public async Task<ResultDto> UpdateUser(int userId, UserDto userDto, ClaimsPrincipal userUpdate)

        {
            var result = new ResultDto();
            try
            {
                var existingUser = _unitOfWork.UserRepository.Get(x => x.UserId == userId);
                if (existingUser == null)
                {
                    result.IsSuccess = false;
                    result.Code = 404;
                    result.Message = "Can not find user";
                    return result;
                }
                // Check if email already exists
                var existingEmail = _unitOfWork.UserRepository.Get(x => x.Email == userDto.Email && x.UserId != userId);
                if (existingEmail != null)
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.Message = "Email already exists";
                    return result;
                }
                // Check if Phone already exists
                var existingPhone = _unitOfWork.UserRepository.Get(x => x.Phone == userDto.Phone && x.UserId != userId);
                if (existingPhone != null)
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.Message = "Phone already exists";
                    return result;
                }
                // Map the Dto to the existing userid entity
                _mapper.Map(userDto, existingUser);

                // Update the additional fields
                existingUser.Username = userDto.Username;
                existingUser.Email = userDto.Email;
                existingUser.Phone = userDto.Phone;
                existingUser.Role = userDto.Role;
                existingUser.UpdatedBy = userUpdate.FindFirst("UserName").Value;
                existingUser.UpdatedAt = DateTime.Now;
                _unitOfWork.UserRepository.Update(existingUser);
                _unitOfWork.UserRepository.Save();
                result.IsSuccess = true;
                result.Code = 200;
                result.Message = "Update User Success";
                return result;

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Code = 400;
                result.Message = ex.Message;
                return result;
            }
            return result;
        }
    }
}
