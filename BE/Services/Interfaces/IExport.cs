using BE.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Interfaces
{
    public interface IExport
    {
         Task<IActionResult> CreateExportAsync(CreateExportRequest request);
         Task<IActionResult> GetExportByWarehouseId(int id);
        Task<IActionResult> GetExportDetail(int id);
    }
}
