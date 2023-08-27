using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Components.Forms;
using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.UnitOfWorks;
using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Domain.Dtos.Roles;
using SPSA.API.Helper.CommonMethods;
using SPSA.API.Helper.Resources;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API.Manager.Implementaion
{
    public class RoleManager : IRoleManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RoleFilterDto> _roleFilterValidator; 
        private readonly IValidator<RoleAddDto> _roleAddValidator;  
        private readonly IValidator<RoleUpdateDto> _roleUpdateValidator;   
        public RoleManager(IUnitOfWork unitOfWork, IValidator<RoleFilterDto> roleFilterValidator, IValidator<RoleAddDto> roleAddValidator, IValidator<RoleUpdateDto> roleUpdateValidator)
        {
            _unitOfWork = unitOfWork;
            _roleFilterValidator = roleFilterValidator;
            _roleAddValidator = roleAddValidator;
            _roleUpdateValidator = roleUpdateValidator; 
        }

        public async Task<ResponseModel> GetDropdownForRoles()
        {
            var result = await _unitOfWork.Roles.GetDropdownForRoles();
            if (result == null)
                return CommonResponse.NotFoundResponse();

            return CommonResponse.SuccessResponseForGet(result);    
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

        public async Task<ResponseModel> GetRoleById(long id)
        {
            var result = await _unitOfWork.Roles.GetById(id);
            return CommonResponse.SuccessResponseForGet(result);
        }

        public async Task<ResponseModel> RoleAdd(RoleAddDto dto)
        {
            #region validation logic
            var validationResult = _roleAddValidator.Validate(dto);
            if (!validationResult.IsValid)
                return CommonResponse.ValidationErrorResponse(CommonMethods.ConvertFluentErrorMessages(validationResult.Errors));

            if (await _unitOfWork.Roles.Any(x => x.Name.Trim() == dto.Name.Trim().ToLower()))
                return CommonResponse.ValidationErrorResponse(CommonMessage.NameAlreadyExists); 

            #endregion

            var role = dto.Adapt<Role>();

            role.SetCommonPropertiesForCreate(_unitOfWork.GetLoggedInUserId());

            await _unitOfWork.Roles.Add(role);
            await _unitOfWork.SaveChange();

            var roleDto = role.Adapt<RoleDto>();
            return CommonResponse.SuccessResponseForAdd(roleDto);
        }

        public async Task<ResponseModel> RoleUpdate(RoleUpdateDto dto)
        {
            var role = await _unitOfWork.Roles.GetById(dto.Id);

            #region validation logic
            var validationResult = _roleUpdateValidator.Validate(dto);
            if (!validationResult.IsValid)
                return CommonResponse.ValidationErrorResponse(CommonMethods.ConvertFluentErrorMessages(validationResult.Errors));

            if (role == null)
                return CommonResponse.ValidationErrorResponse(CommonMessage.NotFound);

            if (await _unitOfWork.Roles.Any(x => x.Name.Trim() == dto.Name.Trim().ToLower() && x.Id != dto.Id))
                return CommonResponse.ValidationErrorResponse(CommonMessage.NameAlreadyExists);

            #endregion

            dto.Adapt(role);
            role.SetCommonPropertiesForUpdate(_unitOfWork.GetLoggedInUserId());
            _unitOfWork.Roles.Update(role); 
            await _unitOfWork.SaveChange();

            var roleDto = role.Adapt<RoleDto>();
            return CommonResponse.SuccessResponseForUpdate(roleDto); 
        }
    }
}
