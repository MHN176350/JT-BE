using BE.Models.DTO.Request;
using BE.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/supplier")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplier supplier;
        public SupplierController(ISupplier supplier)
        {
            this.supplier = supplier;
        }
        [HttpPost("create")]
        public Task<IActionResult> CreateSupplier([FromBody]CreateSupplierRequest createSupplier)
        {
            return supplier.CreateSupplier(createSupplier);
        }
        [HttpGet("getsup")]
        public Task<IActionResult> GetSupp()
        {
            return supplier.GetSupplier();
        } 
    }
}
