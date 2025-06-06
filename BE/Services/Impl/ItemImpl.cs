using BE.DAO;
using BE.Models.DTO.Request;
using BE.Models.DTO.Response;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Impl
{
    public class ItemImpl : IItem
    {
        private readonly ILogger<ItemImpl> _logger;
        private readonly ItemDAO _itemDAO;
        public ItemImpl(ILogger<ItemImpl> logger, ItemDAO dao)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _itemDAO = dao ?? throw new ArgumentNullException(nameof(dao));
        }
        public async Task<IActionResult> CreateItemAsync(CreateItemRequest request)
        {
           if(request.ProductId <= 0 || request.StorageId <= 0 || request.Quantity < 0)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Invalid product ID, storage ID, or quantity."
                });
            }
           _itemDAO.CreateItem(request.ProductId, request.StorageId, request.Quantity);
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "Item created successfully."
            });
        }

        public async Task<IActionResult> GetItemsByStorageIdAsync(int storageId)
        {
            if(storageId <= 0)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Invalid storage ID."
                });
            }
            List<ItemResponse> items = _itemDAO.GetItemsByStorageId(storageId);
            if(items is null || items.Count == 0)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 404,
                    Message = "No items found for the given storage ID."
                });
            }
            return new OkObjectResult(new ResponseFormat
            {
                statusCode = 200,
                Message = "Items retrieved successfully.",
                Data = items
            });
        }

        public async Task<IActionResult> UpdateItemAsync(int itemId, long quantity)
        {
           if(itemId <= 0 || quantity < 0)
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 400,
                    Message = "Invalid item ID or quantity."
                });
            }
            if( _itemDAO.UpdateItem(itemId,quantity))
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 200,
                    Message = "Item updated successfully."
                });
            }
            else
            {
                return new OkObjectResult(new ResponseFormat
                {
                    statusCode = 500,
                    Message = "Failed to update item."
                });
            }

        }
    }
}
