using BE.DAO;
using BE.Models.DTO.Request;
using BE.Models.DTO.Response;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Impl
{
    public class ProductImpl : IProduct
    {
        private readonly ProductDAO _productDAO;
        public ProductImpl(ProductDAO productDAO)
        {
            _productDAO = productDAO ?? throw new ArgumentNullException(nameof(productDAO));
        }

        public async Task<IActionResult> CreateProductAsync(CreateProductRequest request)
        {
            if (request.Code == null || request.Name == null || request.CatId <= 0)
            { 
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Code, Name, Price and Category ID cannot be empty or zero."
                });
            }
            if(_productDAO.CreateProduct(request))
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 200,
                    Message = "Product created successfully."
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 500,
                Message = "Failed to create product."
            });
        }

        public async Task<IActionResult> GetProducts()
        {
           List<ProductResponse> products = _productDAO.GetProducts();
            if (products is null || products.Count == 0)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 404,
                    Message = "No products found."
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "Products retrieved successfully.",
                Data = products
            });
        }
    }
}
