namespace BE.Models.DTO.Request
{
    public class ExportItemRequest
    {
        public int ItemId { get; set; }
        public long Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
