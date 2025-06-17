using System.Security.Claims;
using Microsoft.AspNetCore.Http;
namespace BE.Ultility
{
    public class UserServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }


    }
}