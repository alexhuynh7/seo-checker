using SEOAutoWebApi.Infrastructure.Enums;

namespace SEOAutoWebApi.Models
{
    public class ResponseModel
    {
        public StatusCodeReturnType Code { get; set; }

        public string? Title { get; set; }

        /// <summary>
        /// Use for render to client on UI
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Use for developer read log exception
        /// </summary>
        public string? ErrorMessage { get; set; }

        public dynamic? Data { get; set; }

        public static ResponseModel ReturnData(dynamic? data = null, string message = "", string title = "")
        {
            var res = new ResponseModel
            {
                Code = StatusCodeReturnType.Success,
                Title = title,
                Message = message,
                Data = data ?? true
            };
            return res;
        }

        public static ResponseModel ReturnError(string message = "", string errorMessage = "", string title = "")
        {
            var res = new ResponseModel
            {
                Code = StatusCodeReturnType.Error,
                Title = title,
                Message = message,
                ErrorMessage = errorMessage,
                Data = null
            };
            return res;
        }
    }
}
