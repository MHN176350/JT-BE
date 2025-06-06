using BE.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Interfaces
{
    public interface ICategory
    {
        Task<IActionResult> CreateCategoryAsync(CreateCategoryRequest req);
        Task<IActionResult> GetCategories();

    }
}
