using BE.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Interfaces
{
    public interface IStorage
    {
         Task<IActionResult> CreateStorage(CreateStorageRequest request);
         Task<IActionResult> GetStorageById();
        Task<IActionResult> UpdateStorage(UpdateStorageRequest upd);
        Task<IActionResult> GetStorageMember(string Code);
        Task<IActionResult> AddStorageMember(AddStorageMemberRequest req);
        Task<IActionResult> ChangeRole(ChangeRoleRequest req);
        Task<IActionResult> GetStCode();
    }
}
