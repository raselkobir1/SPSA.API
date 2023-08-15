using SPSA.API.Domain.Dtos.Common;

namespace SPSA.API.Manager.Intrerface
{
    public interface IUserManager
    {
        Task<ResponseModel> GetUserByEmail(string email);
        Task<ResponseModel> GetUserById(long id); 
        Task<ResponseModel> GetAllUsers();  
        
    }
}
