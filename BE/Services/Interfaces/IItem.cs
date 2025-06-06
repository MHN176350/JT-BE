using BE.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Interfaces
{
    public interface IItem
    {
        Task<IActionResult> CreateItemAsync(CreateItemRequest request);
        Task<IActionResult> UpdateItemAsync(int ItemId,long Quantity);
        Task<IActionResult> GetItemsByStorageIdAsync(int storageId);
    }
}
