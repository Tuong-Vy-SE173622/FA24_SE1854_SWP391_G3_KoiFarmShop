using KoiFarmShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Business.Business.TokenBusiness
{
    public interface ITokenService
    {
        public void GenerateRefreshToken(Token token);
        public Token GetRefreshToken(string refreshToken);
        public Token GetRefreshTokenByUserID(int userID);
        public void RemoveAllRefreshToken();
        public void ResetRefreshToken();
        public void UpdateRefreshToken(Token refreshToken);
    }
}
