using BE.Models.DTO.Request;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Authorize]
    [Route("api/item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItem itemService;
        public ItemController(IItem itemService)
        {
            this.itemService = itemService;
        }
        [HttpPost("create")]
        public Task<IActionResult> CreateItemAsync([FromBody] CreateItemRequest request)
        {
            return itemService.CreateItemAsync(request);
        }
        [HttpPut("update/{itemId}/{quantity}")]
        public Task<IActionResult> UpdateItemAsync(int itemId, long quantity)
        {
            return itemService.UpdateItemAsync(itemId, quantity);
        }
        [HttpGet("storage/{storageId}")]
        public Task<IActionResult> GetItemsByStorageIdAsync(int storageId)
        {
            return itemService.GetItemsByStorageIdAsync(storageId);

        }
    }
}
