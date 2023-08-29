using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Components.Forms;
using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.UnitOfWorks;
using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Domain.Dtos.Menus;
using SPSA.API.Helper.CommonMethods;
using SPSA.API.Helper.Resources;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API.Manager.Implementaion
{
    public class MenuManager : IMenuManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<MenuFilterDto> _filterValidate; 
        private readonly IValidator<MenuAddDto> _menuAddValodation;    
        private readonly IValidator<MenuUpdateDto> _menuUpdateValodation;     
        public MenuManager(IUnitOfWork unitOfWork, IValidator<MenuFilterDto> filterValidate, IValidator<MenuAddDto> menuAddValodation, IValidator<MenuUpdateDto> menuUpdateValodation)
        {
            _unitOfWork = unitOfWork;
            _filterValidate = filterValidate;
            _menuAddValodation = menuAddValodation;
            _menuUpdateValodation = menuUpdateValodation;
        }
        public async Task<ResponseModel> GetDropdownForMenus()
        {
            var result = await _unitOfWork.Menus.GetDropdownForRoles();
            if (result == null)
                return CommonResponse.NotFoundResponse();

            return CommonResponse.SuccessResponseForGet(result);
        }

        public async Task<ResponseModel> GetMenuById(long id)
        {
            var result = await _unitOfWork.Menus.GetById(id);
            if (result == null)
                return CommonResponse.NotFoundResponse();

            return CommonResponse.SuccessResponseForGet(result);        
        }

        public async Task<ResponseModel> GetPasignatedMenuResult(MenuFilterDto dto)
        {
            var validationResult = _filterValidate.Validate(dto); 
            if (!validationResult.IsValid) 
                return CommonResponse.ValidationErrorResponse(CommonMethods.ConvertFluentErrorMessages(validationResult.Errors));

            var result = await _unitOfWork.Menus.GetPasignatedUserResult(dto);
            return CommonResponse.SuccessResponseForGet(result);    
        }

        public async Task<ResponseModel> MenuAdd(MenuAddDto dto)
        {
            #region validation logic
            var validationResult = _menuAddValodation.Validate(dto);
            if(!validationResult.IsValid)
                return CommonResponse.ValidationErrorResponse(CommonMethods.ConvertFluentErrorMessages(validationResult.Errors));

            if (await _unitOfWork.Menus.Any(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && !x.IsDeleted))
                return CommonResponse.ValidationErrorResponse(ValidationMessage.MenuAlreadyExists);
            #endregion

            var menu = dto.Adapt<Menu>(); 
            menu.SetCommonPropertiesForCreate(_unitOfWork.GetLoggedInUserId());

            await _unitOfWork.Menus.Add(menu); 
            await _unitOfWork.SaveChange();

            var menuDto = menu.Adapt<MenuDto>(); 

            return CommonResponse.SuccessResponseForAdd(menuDto); 
        }

        public async Task<ResponseModel> MenuUpdate(MenuUpdateDto dto)
        {
            var menu = await _unitOfWork.Menus.GetById(dto.Id);

            #region validation logic
            var validationResult = _menuUpdateValodation.Validate(dto);
            if (!validationResult.IsValid)
                return CommonResponse.ValidationErrorResponse(CommonMethods.ConvertFluentErrorMessages(validationResult.Errors));

            if (menu == null)
                return CommonResponse.NotFoundResponse();

            if (await _unitOfWork.Menus.Any(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && x.Id != dto.Id))
                return CommonResponse.ValidationErrorResponse(ValidationMessage.MenuAlreadyExists);
            #endregion


            dto.Adapt(menu);
            menu.SetCommonPropertiesForUpdate(_unitOfWork.GetLoggedInUserId());

            _unitOfWork.Menus.Update(menu);
            await _unitOfWork.SaveChange();
            var menuDto = menu.Adapt<MenuDto>();

            return CommonResponse.SuccessResponseForUpdate(menuDto);
        }
    }
}
