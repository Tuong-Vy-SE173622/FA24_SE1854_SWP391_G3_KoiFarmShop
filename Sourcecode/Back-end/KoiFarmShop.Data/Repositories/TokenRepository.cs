using KoiFarmShop.Data.Basis;
using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Data.Repositories
{
    public class TokenRepository : GenericRepository<Token>
    {
        private readonly FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
        public TokenRepository(FA_SE1854_SWP391_G3_KoiFarmShopContext context) : base(context)
        {
            _context = context;
        }

        public void GenerateRefreshToken(Token token)
        {
            try
            {
                var existingToken = Get(x => x.UserId == token.UserId);
                if (existingToken != null)
                {
                    // Thực hiện cập nhật thực thể đã tồn tại
                    existingToken.AccessToken = token.AccessToken;
                    existingToken.RefreshToken = token.RefreshToken;
                    existingToken.ExpiredTime = token.ExpiredTime;
                    existingToken.Status = token.Status;
                    Update(existingToken);
                }
                else
                {
                    // Thêm thực thể mới nếu không tồn tại
                    Create(token);
                }
                Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Token GetRefreshToken(string refreshToken)
        {
            try
            {
                var _refreshToken = Get(x => x.RefreshToken == refreshToken);
                return _refreshToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Token GetRefreshTokenByUserID(int userID)
        {
            try
            {
                var existingToken = Get(x => x.UserId == userID);
                return existingToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveAllRefreshToken()
        {
            try
            {
                var _refreshTokenList = GetAll();
                foreach (var item in _refreshTokenList)
                {
                    Remove(item);
                }
                Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ResetRefreshToken()
        {
            try
            {
                var _refreshToken = GetAll();
                foreach (var item in _refreshToken)
                {
                    if (item.Status == 2 || item.ExpiredTime <= DateTime.Now)
                    {
                        Remove(item);
                        Save();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateRefreshToken(Token _refreshToken)
        {
            try
            {
                _refreshToken.Status = 2;
                Update(_refreshToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
