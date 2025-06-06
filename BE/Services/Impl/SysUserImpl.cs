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
        public SysUserImpl(SysUserDAO sysUserDAO, JWTServices jwt)
        {
            this.sysUserDAO = sysUserDAO;
            JWTServices = jwt ?? throw new ArgumentNullException(nameof(jwt));
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
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Avatar = user.Avatar,
                        Token = accessToken,
                        LastLogin = user.LastLogin
                    }
                });
            }

            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 401,
                Message = "Invalid username or password."
            });
        }

        public Task<IActionResult> Register(RegisterRequest request)
        {
           if(request.firstName.IsNullOrEmpty() || request.lastName.IsNullOrEmpty() || 
              request.username.IsNullOrEmpty() || request.password.IsNullOrEmpty())
            {
                return Task.FromResult<IActionResult>(new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "All fields are required."
                }));
            }
            var result = sysUserDAO.Add(request.firstName, request.lastName, request.username, request.password);
            if (result)
            {
                return Task.FromResult<IActionResult>(new OkObjectResult(new ResponseFormat
                {
                    statusCode = 201,
                    Message = "Registration successful."
                }));
            }
            return Task.FromResult<IActionResult>(new OkObjectResult(new ResponseFormat
            {
                statusCode = 500,
                Message = "Registration failed."
            }));
        }
    }
}
