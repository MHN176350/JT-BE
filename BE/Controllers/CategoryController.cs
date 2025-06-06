using BE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Authorize]
    [Route("api/cate")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory category;
        public CategoryController(ICategory category)
        {
            this.category = category ?? throw new ArgumentNullException(nameof(category));
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] Models.DTO.Request.CreateCategoryRequest request)
        {
            return await category.CreateCategoryAsync(request);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetCategoryByIdAsync()
        {
            return await category.GetCategories();
        }
    }
}
