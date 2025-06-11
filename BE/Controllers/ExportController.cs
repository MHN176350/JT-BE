using BE.Models.DTO.Request;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
 
    [Route("api/export")]
    [ApiController]
    [Authorize]
    public class ExportController : ControllerBase
    {
        private readonly IExport _export;
        public ExportController(IExport export)
        {
            _export = export ?? throw new ArgumentNullException(nameof(export));
        }
        [HttpPost("create")]
        public async Task<IActionResult> ExportAsync([FromBody] CreateExportRequest req)
        {
           return await _export.CreateExportAsync(req);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExportByIdAsync(int id)
        {
            return await _export.GetExportByWarehouseId(id);
        }
        [HttpGet("exportDetail/{id}")]
        public async Task<IActionResult> GetExportDetail(int id)
        {
            return await _export.GetExportDetail(id);
        }
    }
}
