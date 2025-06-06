using BE.Context;
using BE.DAO;
using BE.Models.DTO.Request;
using BE.Models.DTO.Response;
using BE.Models.Entities;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BE.Services.Impl
{
    public class CategoryImpl : ICategory
    {
       private readonly CategoryDAO _categoryDAO;
        public CategoryImpl(CategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }
        public async Task<IActionResult> CreateCategoryAsync(CreateCategoryRequest req)
        {
            if(req.Name.IsNullOrEmpty()||req.Description.IsNullOrEmpty())
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode=400,
                    Message= "Name and Description cannot be empty "
                });

            }
           if(!_categoryDAO.Create(req))
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 500,
                    Message = "Failed to create category."
                });
            }


            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "Category created successfully."
            });
        }

        public async Task<IActionResult> GetCategories()
        {
            List<Category> res = _categoryDAO.getCategories();
            if(res is null || res.Count == 0)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 404,
                    Message = "No categories found."
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "Categories retrieved successfully.",
                Data = res
            });
        }
    }
}
