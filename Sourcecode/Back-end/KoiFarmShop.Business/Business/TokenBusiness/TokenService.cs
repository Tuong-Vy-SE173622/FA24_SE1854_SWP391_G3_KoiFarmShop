using KoiFarmShop.Data;
using KoiFarmShop.Data.Models;

namespace KoiFarmShop.Business.Business.TokenBusiness
{
    public class TokenService : ITokenService
    {
        private readonly UnitOfWork _unitOfWork;
        public TokenService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void GenerateRefreshToken(Token token)
        {
            try
            {
                var existingToken = _unitOfWork.TokenRepository.Get(x => x.UserId == token.UserId);
                if (existingToken != null)
                {
                    // Thực hiện cập nhật thực thể đã tồn tại
                    existingToken.AccessToken = token.AccessToken;
                    existingToken.RefreshToken = token.RefreshToken;
                    existingToken.ExpiredTime = token.ExpiredTime;
                    existingToken.Status = token.Status;
                    _unitOfWork.TokenRepository.Update(existingToken);
                }
                else
                {
                    // Thêm thực thể mới nếu không tồn tại
                    _unitOfWork.TokenRepository.Create(token);
                }
                _unitOfWork.TokenRepository.Save();
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
                var _refreshToken = _unitOfWork.TokenRepository.Get(x => x.RefreshToken == refreshToken);
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
                var existingToken = _unitOfWork.TokenRepository.Get(x => x.UserId == userID);
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
                var _refreshTokenList = _unitOfWork.TokenRepository.GetAll();
                foreach (var item in _refreshTokenList)
                {
                    _unitOfWork.TokenRepository.Remove(item);
                }
                _unitOfWork.TokenRepository.Save();
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
                var _refreshToken = _unitOfWork.TokenRepository.GetAll();
                foreach (var item in _refreshToken)
                {
                    if (item.Status == 2 || item.ExpiredTime <= DateTime.Now)
                    {
                        _unitOfWork.TokenRepository.Remove(item);
                        _unitOfWork.TokenRepository.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateRefreshToken(Token refreshToken)
        {
            try
            {
                refreshToken.Status = 2;
                _unitOfWork.TokenRepository.Update(refreshToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
