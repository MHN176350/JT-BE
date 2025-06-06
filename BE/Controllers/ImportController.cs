using BE.Models.DTO.Request;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/import")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IImport importServices;
        public ImportController(IImport importServices)
        {
            this.importServices = importServices;

        }
        [HttpPost("create")]
        public Task<IActionResult> createImport([FromBody] CreateImportRequest import)
        {
            return importServices.CreateImport(import);
        }
        [HttpGet("{id}")]
        public Task<IActionResult> GetImportbyWarehouse(int id)
        {
            return importServices.GetImportbyWarehouse(id);
        }
        [HttpGet("importDetail")]


        public Task<IActionResult> GetImportDetail(int id)
        {
            return importServices.GetImportDetail(id);
        }
    }
}