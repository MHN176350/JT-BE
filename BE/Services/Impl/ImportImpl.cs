using BE.DAO;
using BE.Models.DTO.Request;
using BE.Models.DTO.Response;
using BE.Services.Interfaces;
using BE.Ultility;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Impl
{
    public class ImportImpl : IImport
    {
        private readonly ImportDAO _importDAO;
        private readonly UserServices _userServices;
        public ImportImpl(ImportDAO importDAO, UserServices userServices)
        {
            _importDAO = importDAO;
            _userServices = userServices;
        }
        public async Task<IActionResult> CreateImport(CreateImportRequest req)
        {
            int uid = int.Parse(_userServices.GetUserId() ?? "0");
            if (req == null)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Invalid Request"
                });

            }
            if (_importDAO.createImport(req, uid))
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 200,
                    Message = "Import created successfull"
                });

            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 400,
                Message = "An error has occurred",
            });


        }

        public async Task<IActionResult> GetImportbyWarehouse(int wid)
        {
            return new OkObjectResult(new ResponseFormat
            {
                Message = "Get Import Success",
                statusCode = 200,
                Data = _importDAO.GetImportByWarehouse(wid)
            });
        }

        public async Task<IActionResult> GetImportDetail(int id)
        {
           if(id<=0)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Invalid import ID."
                });
            }
            var importDetail = _importDAO.GetImportDetail(id);
            if (importDetail == null)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 404,
                    Message = "Import not found."
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "Import detail retrieved successfully.",
                Data = importDetail
            });
        }
    }
}
