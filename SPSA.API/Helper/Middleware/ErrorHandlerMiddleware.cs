using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Helper.CommonMethods;
using SPSA.API.Helper.Resources;
using System.Net;

namespace SPSA.API.Helper.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next; 
        private readonly IHostEnvironment _hostEnvironment;
        public ErrorHandlerMiddleware(RequestDelegate next, IHostEnvironment hostEnvironment)
        {
            _next = next;
            _hostEnvironment = hostEnvironment;
        }
        public async Task Invoke(HttpContext context)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
            };
            try
            {
                await _next(context);

                if(context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(CommonResponse.UnAuthorizedResponse(), settings));
                }
            }
            catch (Exception ex)
            {
                var res = new ResponseModel();
                var response = context.Response;
                response.ContentType = "application/json";
                var errorMessage = _hostEnvironment.IsDevelopment() ? (ex.Message + (ex.InnerException != null ? $" - InnerException - {ex.InnerException.Message}" : "")) : CommonMessage.InternalServerError;
                switch (ex)
                {
                    case UnauthorizedAccessException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        res = CommonResponse.UnAuthorizedResponse();
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        res = CommonResponse.InternalServerErrorResponse(errorMessage);
                        break;
                }
                var result = JsonConvert.SerializeObject(res, settings);
                await response.WriteAsync(result);
            }
        }
    }
}
