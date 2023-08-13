using SPSA.API.DataAccess.UnitOfWorks;
using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Helper.CommonMethods;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API.Manager.Implementaion
{
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> GetUserByEmail(string email)
        {
            var result = await _unitOfWork.Users.GetWhere(x=> x.Email == email);
            
            return CommonResponse.SuccessResponseForGet(result);
        }
    }
}
