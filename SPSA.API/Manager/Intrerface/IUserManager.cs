using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Domain.Dtos.Users;

namespace SPSA.API.Manager.Intrerface
{
    public interface IUserManager
    {
        Task<ResponseModel> UserAdd(UserAddDto dto);
        Task<ResponseModel> UserUpdate(UserUpdateDto dto);  
        Task<ResponseModel> GetUserByEmail(string email);
        Task<ResponseModel> GetUserById(long id); 
        Task<ResponseModel> GetAllUsers();  
        
    }
}
