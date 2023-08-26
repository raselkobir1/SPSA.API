using FluentValidation;
using SPSA.API.DataAccess.UnitOfWorks;
using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Domain.Dtos.Roles;
using SPSA.API.Helper.CommonMethods;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API.Manager.Implementaion
{
    public class RoleManager : IRoleManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RoleFilterDto> _roleFilterValidator; 
        public RoleManager(IUnitOfWork unitOfWork, IValidator<RoleFilterDto> roleFilterValidator)
        {
            _unitOfWork = unitOfWork;
            _roleFilterValidator = roleFilterValidator; 
        }
        public async Task<ResponseModel> GetPasignatedUserResult(RoleFilterDto dto)
        {
            #region Validation
            var validationResult = _roleFilterValidator.Validate(dto);  
            if(!validationResult.IsValid)
                return CommonResponse.ValidationErrorResponse(CommonMethods.ConvertFluentErrorMessages(validationResult.Errors));
            #endregion

            var result = await _unitOfWork.Roles.GetPasignatedUserResult(dto); 
            return CommonResponse.SuccessResponseForGet(result);    
        }

        public Task<ResponseModel> GetRoleById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> RoleAdd(RoleAddDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> RoleUpdate(RoleUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
