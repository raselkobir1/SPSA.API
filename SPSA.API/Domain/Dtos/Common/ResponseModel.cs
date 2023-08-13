namespace SPSA.API.Domain.Dtos.Common
{
    public class ResponseModel
    {
        internal int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
