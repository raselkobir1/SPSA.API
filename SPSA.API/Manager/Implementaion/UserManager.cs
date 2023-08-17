using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.UnitOfWorks;
using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Domain.Dtos.Users;
using SPSA.API.Helper.CommonMethods;
using SPSA.API.Helper.Resources;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API.Manager.Implementaion
{
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UserAddDto> _userAddValidator;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserManager(IUnitOfWork unitOfWork, IValidator<UserAddDto> userAddValidator, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _userAddValidator = userAddValidator;  
            _passwordHasher = passwordHasher;   
        }

        public async Task<ResponseModel> GetAllUsers()
        {
            var users = await _unitOfWork.Users.GetAll();
            return CommonResponse.SuccessResponseForGet(users);
        }

        public async Task<ResponseModel> GetUserByEmail(string email)
        {
            var result = await _unitOfWork.Users.GetWhere(x=> x.Email == email);
            
            return CommonResponse.SuccessResponseForGet(result);
        }

        public async Task<ResponseModel> GetUserById(long id)
        {
            var result = await _unitOfWork.Users.GetById(id);

            return CommonResponse.SuccessResponseForGet(result);
        }

        public async Task<ResponseModel> UserAdd(UserAddDto dto)
        {
            #region validation logic
            var validationResult = _userAddValidator.Validate(dto);
            if (!validationResult.IsValid)
                return CommonResponse.ValidationErrorResponse(CommonMethods.ConvertFluentErrorMessages(validationResult.Errors));

            if (await _unitOfWork.Users.Any(x => x.Email == dto.Email.Trim().ToLower() && !x.IsDeleted))
                return CommonResponse.ValidationErrorResponse(ValidationMessage.EmailAlreadyExists);

            //if (!await _unitOfWork.Roles.Any(x => x.Id == dto.RoleId))
            //    return CommonResponse.ValidationErrorResponse(string.Format(ValidationMessage.Role_InvalidId, dto.RoleId));
            #endregion

            var user = dto.Adapt<User>();
            var encyptedPassword = _passwordHasher.HashPassword(user, dto.Password);

            user.Password = encyptedPassword;
            user.BranchId = 1; //_unitOfWork.Branches.GetDefaultBranchId();
            user.SetCommonPropertiesForCreate(_unitOfWork.GetLoggedInUserId());

            await _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChange();

            var userDto = user.Adapt<UserDto>();

            return CommonResponse.SuccessResponseForAdd(userDto);
        }
    }
}
