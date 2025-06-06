using BE.DAO;
using BE.Models.DTO.Request;
using BE.Models.DTO.Response;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Impl
{
    public class SupplierImpl : ISupplier
    {
        private readonly SupplierDAO _supplierDAO;
        public SupplierImpl(SupplierDAO supplierDAO)
        {
            _supplierDAO = supplierDAO;
        }
        public async Task<IActionResult> CreateSupplier(CreateSupplierRequest createSupplierRequest)
        {
            if (createSupplierRequest.PhoneNumber is null || createSupplierRequest.Name is null || createSupplierRequest.Address is null)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode=400,
                    Message="Invalid request"
                });
            }
            if (_supplierDAO.CreateSupplier(createSupplierRequest))
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 200,
                    Message = "Created Successful"
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 400,
                Message = "In to no se vo e ro"
            });
        }

        public async Task<IActionResult> GetSupplier()
        {
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "Get Supplier Successful",
                Data = _supplierDAO.GetSup()
            });
        }
    }
}
