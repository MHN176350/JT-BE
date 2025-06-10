using BE.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Interfaces
{
    public interface ISysUser
    {
        Task<IActionResult> LoginAsync(LoginRequest request);
        Task<IActionResult> Register(RegisterRequest request);
        Task<IActionResult> ChangePass(ChangePasswordRequest changePasswordRequest);
        Task<IActionResult> LockUser(int userId);
        Task<IActionResult> UpdateProfile(UpdateProfileRequest updateProfileRequest);
        Task<IActionResult> GetAllUser();
        Task<IActionResult> ChangeProfilePicture( string profilePicture);
    }
}