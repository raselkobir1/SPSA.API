using SPSA.API.DataAccess.UnitOfWorks;
using SPSA.API.Domain.Dtos.Common;
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

        public Task<ResponseModel> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
