using BE.DAO;
using BE.Models.DTO.Request;
using BE.Models.DTO.Response;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Impl
{
    public class CustomerImpl : ICustomer
    {
        private readonly CustomerDAO _customerDAO;
        public CustomerImpl(CustomerDAO customerDAO)
        {
            _customerDAO = customerDAO ?? throw new ArgumentNullException(nameof(customerDAO));
        }

      
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request)
        {
            if(request == null || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                return new OkObjectResult( new ResponseFormat { 
                    statusCode = 400,
                    Message = "Invalid customer request data."
                });

            }
            if (_customerDAO.CreateCustomer(request))
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 200,
                    Message = "Customer created successfully."
                });
            }
            else
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 500,
                    Message = "Failed to create customer."
                });
            }
        }

        public async Task<IActionResult> GetCustomer()
        {
          return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "Customer retrieved successfully.",
                Data = _customerDAO.GetCustomer()
            });
        }
    }
}
