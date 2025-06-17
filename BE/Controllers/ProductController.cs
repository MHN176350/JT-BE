using BE.Models.DTO.Request;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Authorize]
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productService;
        public ProductController(IProduct productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        [HttpPost("create")]    
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest request)
        {
            return await _productService.CreateProductAsync(request);
        }
        [HttpGet("pd")]
        public async Task<IActionResult> GetProductByIdAsync()
        {
            return await _productService.GetProducts();
        }
    }
}
