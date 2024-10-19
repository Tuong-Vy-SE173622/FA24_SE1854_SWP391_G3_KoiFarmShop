using KoiFarmShop.Business.Business.TokenBusiness;
using KoiFarmShop.Business.Business.UserBusiness;
using KoiFarmShop.Business.Dto;
using KoiFarmShop.Business.Security;
using KoiFarmShop.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthorizeController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        #region GenerateToken
        /// <summary>
        /// Which will generating token accessible for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [NonAction]
        public TokenDto GenerateToken(User user, String? RT)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("UserName", user.Username),
                new Claim("Email", user.Email),
                new Claim("Role", user.Role)
            };

            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes("c2VydmVwZXJmZWN0bHljaGVlc2VxdWlja2NvYWNoY29sbGVjdHNsb3Bld2lzZWNhbWU="));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "YourIssuer",
                audience: "YourAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            if (RT != null)
            {
                return new TokenDto()
                {
                    AccessToken = accessToken,
                    RefreshToken = RT,
                    ExpiredAt = _tokenService.GetRefreshTokenByUserID(user.UserId).ExpiredTime
                };
            }
            return new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = GenerateRefreshToken(user),
                ExpiredAt = _tokenService.GetRefreshTokenByUserID(user.UserId).ExpiredTime
            };
        }
        #endregion


        #region GenerateRefreshToken
        // Hàm tạo refresh token
        [NonAction]
        public string GenerateRefreshToken(User user)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string refreshtoken = Convert.ToBase64String(randomnumber);
                var refreshTokenEntity = new Token
                {
                    UserId = user.UserId,
                    AccessToken = new Random().Next().ToString(),
                    RefreshToken = refreshtoken.ToString(),
                    ExpiredTime = DateTime.Now.AddDays(7),
                    Status = 1
                };

                _tokenService.GenerateRefreshToken(refreshTokenEntity);
                return refreshtoken;
            }
        }
        #endregion


        #region RefreshAccessToken
        [HttpPost("RefreshAccessToken")]
        public async Task<ActionResult> RefreshAccessToken(Token token)
        {
            try
            {
                var jwtTokenHander = new JwtSecurityTokenHandler();
                var TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("c2VydmVwZXJmZWN0bHljaGVlc2VxdWlja2NvYWNoY29sbGVjdHNsb3Bld2lzZWNhbWU=")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false
                };
                //ResetRefreshToken in DB if token is disable or expired will Remove RT
                _tokenService.ResetRefreshToken();
                //check validate of Parameter
                var tokenVerification = jwtTokenHander.ValidateToken(token.AccessToken, TokenValidationParameters, out var validatedToken);
                if (tokenVerification == null)
                {
                    return Ok(new ResultDto
                    {
                        IsSuccess = false,
                        Message = "Invalid Param"
                    });
                }
                //check AccessToken expire?
                var epochTime = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                DateTimeOffset dateTimeUtc = DateTimeOffset.FromUnixTimeSeconds(epochTime);
                DateTime dateTimeUtcConverted = dateTimeUtc.UtcDateTime;
                if (dateTimeUtcConverted > DateTime.UtcNow)
                {
                    return Ok(new ResultDto
                    {
                        IsSuccess = false,
                        Message = "AccessToken had not expired",
                        Data = "Expire time: " + dateTimeUtcConverted.ToString()
                    });
                }
                //check RefreshToken exist in DB
                var storedToken = _tokenService.GetRefreshToken(token.RefreshToken);
                if (storedToken == null)
                {
                    return Ok(new ResultDto
                    {
                        IsSuccess = false,
                        Message = "RefreshToken had not existed"
                    });
                }
                //check RefreshToken is revoked?
                if (storedToken.Status == 2)
                {
                    return Ok(new ResultDto
                    {
                        IsSuccess = false,
                        Message = "RefreshToken had been revoked"
                    });
                }
                var User = _userService.GetUserById(storedToken.UserId);
                var newAT = GenerateToken(User, token.RefreshToken);

                return Ok(new ResultDto
                {
                    IsSuccess = true,
                    Message = "Refresh AT success fully",
                    Data = newAT
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultDto
                {
                    IsSuccess = false,
                    Code = 500,
                    Message = "Something go wrong"
                });
            }
        }
        #endregion


        #region Login
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string userName, string password)
        {
            var user = _userService.GetUserByUserName(userName);
            if (user != null && user.IsActive == true)
            {
                // Hash the input password with SHA256
                var hashedInputPasswordString = PasswordHasher.HashPassword(password);

                if (hashedInputPasswordString == user.Password)
                {
                    // Convert userId to string using .ToString()
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
                    // Compare the hashed input password with the stored hashed password
                    _tokenService.ResetRefreshToken();
                var token = GenerateToken(user, null);
                return Ok(token);
                }
            }
            return BadRequest(new ResultDto
            {
                IsSuccess = false,
                Message = "Status Code:401 Unauthorized",
                Data = null
            });
        }
        #endregion


        #region Logout
        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"];
                token = token.Split(' ')[1];
                var tokenHandler = new JwtSecurityTokenHandler();
                var TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("c2VydmVwZXJmZWN0bHljaGVlc2VxdWlja2NvYWNoY29sbGVjdHNsb3Bld2lzZWNhbWU=")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false
                };

                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, TokenValidationParameters, out validatedToken);
                var userIdClaim = claimsPrincipal.FindFirst("UserId");

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    var _refreshToken = _tokenService.GetRefreshTokenByUserID(userId);
                    _tokenService.UpdateRefreshToken(_refreshToken);
                    _tokenService.ResetRefreshToken();
                }
                else
                {
                    // Handle the case where the UserId claim is missing or invalid
                    return BadRequest(new ResultDto
                    {
                        IsSuccess = false,
                        Message = "User ID not found or invalid."
                    });
                }

                if (HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    HttpContext.Request.Headers.Remove("Authorization");
                }

                return Ok(new ResultDto
                {
                    IsSuccess = true,
                    Message = "Logout successfully!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultDto
                {
                    IsSuccess = false,
                    Message = "Something went wrong: " + ex.Message
                });
            }
        }
        #endregion


        #region Who Am I
        /// <summary>
        /// Check infor of user
        /// </summary>
        /// <returns>Infor of user</returns>
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            // Kiểm tra xem người dùng đã được xác thực chưa
            if (User.Identity.IsAuthenticated)
            {
                // Lấy thông tin về người dùng từ claims
                var userIdClaim = User.FindFirst("UserId");
                var userNameClaim = User.FindFirst("UserName");
                var userEmailClaim = User.FindFirst("Email");
                var userRoleClaim = User.FindFirst("Role");

                // Kiểm tra xem các claim có tồn tại không
                if (userIdClaim != null && userNameClaim != null && userEmailClaim != null && userRoleClaim != null)
                {
                    var userId = userIdClaim.Value;
                    var userName = userNameClaim.Value;
                    var userEmail = userEmailClaim.Value;
                    var userRole = userRoleClaim.Value;

                    // Trả về thông tin của người dùng cùng với token
                    var user = new User
                    {
                        UserId = int.Parse(userId),
                        Username = userName,
                        Email = userEmail,
                        Role = userRole
                    };

                    // Tạo token JWT cho người dùng


                    // Trả về thông tin của người dùng cùng với token
                    return Ok(new { UserId = userId, UserName = userName, Email = userEmail, Role = userRole });
                }
                else
                {
                    // Nếu thiếu thông tin trong claims, trả về lỗi 401 Unauthorized
                    return Unauthorized(new { Message = "Missing user information in claims" });
                }
            }
            else
            {
                // Trả về lỗi 401 Unauthorized nếu người dùng chưa được xác thực
                return Unauthorized();
            }
        }
        #endregion


        [HttpGet("test")]
        public void ViewAllUsers()
        {
            var result = _userService.GetUserByUserName("minhman");
        }
    }
}
