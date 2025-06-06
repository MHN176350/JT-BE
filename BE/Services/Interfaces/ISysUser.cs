using BE.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Interfaces
{
    public interface ISysUser
    {
        public Task<IActionResult> LoginAsync(LoginRequest request);
        public Task<IActionResult> Register(RegisterRequest request);
    }
}
