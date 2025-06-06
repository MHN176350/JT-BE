using BE.Models;

namespace BE.Models.DTO.Request
{
    public class CreateExportRequest
    {
        public int StorageId { get; set; }
        public int CustId { get; set; }
        public bool UsePoint { get; set; }= false;
        public List<ExportItemRequest> items { get; set; } = new List<ExportItemRequest>();
    }
}
