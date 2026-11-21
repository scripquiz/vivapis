using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ViviliamApi2.Controllers
{
    public abstract class BaseViviliamController : ControllerBase
    {
        protected readonly string _exceptionMessage = "Request failed.";
        private string UserAgent => HttpContext.Request.Headers["User-Agent"].ToString();
        [ApiExplorerSettings(IgnoreApi = true)]
        public void LogIncomingRequest(string api, string request)
        {
            Log.Information("API {api} request {request} received from user agent {userAgent}.", api, request, UserAgent);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void LogException(string api, Exception ex)
        {
            Log.Error("Exception occured @{api} - {@exception}", api, ex);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool IsValidFileName(string fileName)
        {
            return !string.IsNullOrEmpty(fileName) && fileName.Length <= 255;
        }
    }
}
