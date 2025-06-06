using BE.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace BE.Services.Interfaces
{
    public interface IImport
    {
        Task<IActionResult> CreateImport(CreateImportRequest req);
        Task<IActionResult> GetImportbyWarehouse(int id);
        Task<IActionResult> GetImportDetail(int id);
    }
}
