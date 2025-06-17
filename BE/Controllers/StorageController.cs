using BE.Models.DTO.Request;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Authorize]
    [Route("api/warehouse")]
    [ApiController]
    [Authorize]
    public class StorageController : ControllerBase
    {
        private readonly IStorage _storageService;
        public StorageController(IStorage storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateStorage([FromBody] CreateStorageRequest request)
        {
            return await _storageService.CreateStorage(request);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetStorageByUser()
        {
            return await _storageService.GetStorageById();
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateStorage([FromBody] UpdateStorageRequest request)
        {
            return await (_storageService.UpdateStorage(request));
        }
        [HttpGet("members")]
        public async Task<IActionResult> DisplayMember([FromQuery] string Code)
        {
            return await _storageService.GetStorageMember(Code);
        }
        [HttpPost("addMember")]
        public async Task<IActionResult> AddMember([FromBody] AddStorageMemberRequest req)
        {
            return await _storageService.AddStorageMember(req);
        }
        [HttpPost("crole")]
        public async Task<IActionResult> ChangeRole(ChangeRoleRequest req)
        {
            return await _storageService.ChangeRole(req);
        }
        [HttpGet("getcode")]
        public async Task<IActionResult> GetCodes()
        {
            return await _storageService.GetStCode();
        }
    }
}