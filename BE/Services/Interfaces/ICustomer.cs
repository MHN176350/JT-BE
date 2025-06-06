using BE.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Interfaces
{
    public interface ICustomer
    {
        Task<IActionResult> CreateCustomer(CreateCustomerRequest request);
        Task<IActionResult> GetCustomer();
    }
}
