using BE.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Interfaces
{
    public interface IProduct
    {
        Task<IActionResult> CreateProductAsync(CreateProductRequest request);
        Task<IActionResult> GetProducts();
    }
}
