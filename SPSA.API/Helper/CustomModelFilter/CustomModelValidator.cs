using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SPSA.API.Helper.CommonMethods;

namespace SPSA.API.Helper.CustomModelFilter
{
    public class CustomModelValidator : ControllerBase, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorList = new List<string>();
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var x in item.Errors)
                    {
                        errorList.Add(x.ErrorMessage);
                    }
                }
                context.Result = StatusCode(StatusCodes.Status400BadRequest, CommonResponse.ValidationErrorResponse(errorList));
            }
        }
    }
}
