using BE.DAO;
using BE.Models.DTO.Request;
using BE.Models.DTO.Response;
using BE.Models.Entities;
using BE.Services.Interfaces;
using BE.Ultility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BE.Services.Impl
{
    public class StorageImpl : IStorage
    {
        private readonly ILogger<StorageImpl> _logger;
        private readonly StorageDAO _storageDAO;
        private readonly UserServices _userServices;
        public StorageImpl(ILogger<StorageImpl> logger, StorageDAO storageDAO, UserServices userServices)
        {
            _logger = logger;
            _storageDAO = storageDAO;
            _userServices = userServices;
        }

        public async Task<IActionResult> AddStorageMember(AddStorageMemberRequest req)
        {
            if(req == null)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message= "Invalid Request"
                });

            }
            if(_storageDAO.AddStorageMember(req))
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 200,
                    Message = "Member Added Successful"
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 500,
                Message = "Internal Server error"
            });
        }

        public async Task<IActionResult> CreateStorage(CreateStorageRequest request)
        {
            if (request.Code.IsNullOrEmpty() || request.Location.IsNullOrEmpty())
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Code and Location cannot be empty."
                });

            }
            if (_storageDAO.CreateStorage(request.Location, request.Code, int.Parse(_userServices.GetUserId())))
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 200,
                    Message = "Storage created successfully."
                });
            }
            else
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 500,
                    Message = "Failed to create storage."
                });
            }
        }
        public async Task<IActionResult> GetStorageById()
        {

            int uid = int.Parse(_userServices.GetUserId() ?? "0");
            if (uid <= 0)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Unauthorized."
                });
            }
            List<StorageResponse> storages = _storageDAO.GetStorageByUserId(uid);
            if (storages is null || storages.Count == 0)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 404,
                    Message = "No storage found."
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "Storages retrieved successfully.",
                Data = storages
            });


        }

        public async Task<IActionResult> GetStorageMember(int stId)
        {
            if (stId <= 0)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Warehouse ID cannot be empty"
                });

            }return new OkObjectResult(new ResponseFormat
            {
                Data = _storageDAO.GetStorageMember(stId),
                Message = "Displaying User Data",
                statusCode = 200,
            });
        }

        public async Task<IActionResult> UpdateStorage(UpdateStorageRequest upd)
        {
            int uid = int.Parse(_userServices.GetUserId() ?? "0");
            if (uid <= 0) {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 401,
                    Message = "Please Login First"

                });


            }
            if (_storageDAO.updateStorage(upd, uid))
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 200,
                    Message = "Updated"
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 400,
                Message = "Failed to update storage please chech your privilage"
            });
        }
    } 
}
