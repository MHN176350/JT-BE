using BE.Models.DTO.Request;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class SysUserController : ControllerBase
    {
        private readonly ISysUser sysUserService;
        public SysUserController(ISysUser sysUserService)
        {
            this.sysUserService = sysUserService;
        }
        [HttpPost("login")]
        public Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return sysUserService.LoginAsync(request);
        }
        [HttpPost("register")]
        public Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            return sysUserService.Register(request);
        }

    }
}
