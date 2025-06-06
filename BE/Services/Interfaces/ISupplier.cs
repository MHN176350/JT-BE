using BE.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Interfaces
{
    public interface ISupplier
    {
        Task<IActionResult> CreateSupplier(CreateSupplierRequest createSupplierRequest);
        Task<IActionResult> GetSupplier();
    }
}
