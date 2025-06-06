using BE.DAO;
using BE.Models.DTO.Request;
using BE.Models.DTO.Response;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Impl
{
    public class ExportImpl : IExport
    {
        private readonly ExportDAO _exportDAO;
        public ExportImpl(ExportDAO exportDAO)
        {
            _exportDAO = exportDAO;
        }
        public async Task<IActionResult> CreateExportAsync(CreateExportRequest request)
        {
            if( request.CustId <=0 || !request.items.Any())
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Invalid export request data."
                });
            }
            if (_exportDAO.CreateExport(request))
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 200,
                    Message = "Export created successfully."
                });
            }
            else
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 500,
                    Message = "Failed to create export."
                });
            }
        }

        public async Task<IActionResult> GetExportByWarehouseId(int id)
        {
          if(id <= 0)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Invalid export ID."
                });
            }
            var export = _exportDAO.GetExportById(id);
            if (export == null)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 404,
                    Message = "Export not found."
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "Export retrieved successfully.",
                Data = export
            });
        }
       
        public async Task<IActionResult> GetExportDetail(int id)
        {
            if (id <= 0)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Invalid export ID."
                });
            }
            var export = _exportDAO.GetExportDetail(id);
            if (export == null)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 404,
                    Message = "Export not found."
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "Export retrieved successfully.",
                Data = export
            });
        }
    }
}
