using BE.DAO;
using BE.Models.DTO.Request;
using BE.Models.DTO.Response;
using BE.Services.Interfaces;
using BE.Ultility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BE.Services.Impl
{
    public class SysUserImpl : ISysUser
    {
        private readonly SysUserDAO sysUserDAO;
        private readonly JWTServices JWTServices;
        private readonly UserServices userServices;
        private readonly StorageServices storageService;
        public SysUserImpl(SysUserDAO sysUserDAO, JWTServices jwt, UserServices us, StorageServices storage)
        {
            this.sysUserDAO = sysUserDAO;
            JWTServices = jwt ?? throw new ArgumentNullException(nameof(jwt));
            userServices = us ?? throw new ArgumentNullException(nameof(us));
            storageService = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public async Task<IActionResult> ChangePass(ChangePasswordRequest changePasswordRequest)
        {
            if(changePasswordRequest.OldPassword.IsNullOrEmpty() || changePasswordRequest.NewPassword.IsNullOrEmpty())
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Old password and new password cannot be empty."
                });
            }
            int usId = int.Parse(userServices.GetUserId()??"0");

            var result = await sysUserDAO.ChangePassword(usId, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);
            if (result)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 200,
                    Message = "Password changed successfully."
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 500,
                Message = "Password change failed."
            });
        }

        public async Task<IActionResult> ChangeProfilePicture(string profilePicture)
        {
            int usId = int.Parse(userServices.GetUserId()??"0");
            if (profilePicture.IsNullOrEmpty()) {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Profile picture cannot be empty."
                });
            }

            var result = await sysUserDAO.UpdateProfilePicture(usId, profilePicture);
            if (!result.IsNullOrEmpty())
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 200,
                    Message = "Profile picture updated successfully.",
                    Data = result
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 500,
                Message = "Failed to update profile picture."
            });
            
        }

        public async Task<IActionResult> GetAllUser()
        {
            Object result = sysUserDAO.GetAllUsers();
            if (result is null || !((List<object>)result).Any())
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 404,
                    Message = "No users found."
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "User list retrieved successfully.",
                Data = sysUserDAO.GetAllUsers()
            });
        }

        public async Task<IActionResult> LockUser(int userId)
        {
         if(sysUserDAO.LockUser(userId))
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "User locked successfully."
            });
         return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 500,
                    Message = "Failed to lock user."
                });
        }

        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            if (request.Username.IsNullOrEmpty() || request.Password.IsNullOrEmpty())
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Username and password cannot be empty."
                });
            }

            var user = sysUserDAO.Login(request.Username, request.Password);
            if (user is not null)
            {
                var accessToken =JWTServices.GenerateAuthToken(user.Id, user.IsAdmin);
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 200,
                    Message = "Login successful.",
                    Data = new LoginResponse
                    {
                        UserName = user.Username,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Avatar = await storageService.GetImageUrl(user.Avatar),
                        Token = accessToken,
                        LastLogin = user.LastLogin
                    }
                } );    
            }

            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 401,
                Message = "Invalid username or password ."
            });
        }

        public async Task<IActionResult> Register(RegisterRequest request)
        {
           if(request.firstName.IsNullOrEmpty() || request.lastName.IsNullOrEmpty() || 
              request.username.IsNullOrEmpty() || request.password.IsNullOrEmpty())
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "All fields are required."
                });
            }
            var result = sysUserDAO.Add(request.firstName, request.lastName, request.username, request.password);
            if (result)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 201,
                    Message = "Registration successful."
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 500,
                Message = "Registration failed."
            });
        }

        public async Task<IActionResult> UpdateProfile(UpdateProfileRequest updateProfileRequest)
        {
            if(updateProfileRequest.Id <= 0 || updateProfileRequest.Avatar.IsNullOrEmpty()) 
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Invalid profile data."
                });
            }
            var result = sysUserDAO.UpdateProfile(updateProfileRequest);
            if (result)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 200,
                    Message = "Profile updated successfully."
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 500,
                Message = "Profile update failed."
            });
        }

    }
}
