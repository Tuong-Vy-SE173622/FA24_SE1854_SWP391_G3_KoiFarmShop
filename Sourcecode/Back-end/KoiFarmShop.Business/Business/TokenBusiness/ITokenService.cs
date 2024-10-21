using KoiFarmShop.Data.Models;

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
