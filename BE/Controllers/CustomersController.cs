using BE.Models.DTO.Request;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/cust")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomer _customerService;
        public CustomersController(ICustomer customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            return await _customerService.CreateCustomer(request);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllCustomers()
        {
            return await _customerService.GetCustomer();
        }
        
    }
}
