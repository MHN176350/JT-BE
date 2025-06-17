using BE.Models.DTO.Request;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
     
        [HttpPost("lock/{id}")]
        [Authorize(Roles = "Admin")]
        public Task<IActionResult> LockSwitch(int id)
        {
            return sysUserService.LockUser(id);
        }
        [HttpPost("changePassword")]
        
        [Authorize]
        public Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
          return sysUserService.ChangePass(changePasswordRequest);
        }
        [Authorize]
        [HttpPost("updateProfile")]
        public Task<IActionResult> updateProfile([FromBody] UpdateProfileRequest request)
        {
            return sysUserService.UpdateProfile(request);
        }
        [Authorize (Roles = "Admin")]
        [HttpGet("getalluser")]
        public Task<IActionResult> GetAllUser()
        {
            return sysUserService.GetAllUser();
        }
        [Authorize]
        [HttpPost("changeProfilePicture")]
        public Task<IActionResult> ChangeProfilePicture([FromBody] ChangePictureRequest profilePicture)
        {
            return sysUserService.ChangeProfilePicture(profilePicture.PictureContent);
        }
    }
}
