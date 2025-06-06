using BE.Models.DTO.Request;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Authorize]
    [Route("api/warehouse")]
    [ApiController]
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
        public async Task<IActionResult> UpdateStorage([FromBody]UpdateStorageRequest request)
        {
            return await (_storageService.UpdateStorage(request));
        }

    }
}
