using SPSA.API.Domain.Dtos;
using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API.Manager.Implementaion
{
    public class AuthManager : IAuthManager
    {
        public Task<ResponseModel> SignIn(SignInDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> SignOut(SignOutDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
