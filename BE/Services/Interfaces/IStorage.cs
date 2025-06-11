using BE.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Interfaces
{
    public interface IStorage
    {
        public Task<IActionResult> CreateStorage(CreateStorageRequest request);
        public Task<IActionResult> GetStorageById();
        Task<IActionResult> UpdateStorage(UpdateStorageRequest upd);
        Task<IActionResult> GetStorageMember(int stId);
        Task<IActionResult> AddStorageMember(AddStorageMemberRequest req);
    }
}
