namespace BE.Models.DTO.Response
{
    public class ImportDetailResponse
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;  
        public string ItemCode { get; set; } = string.Empty;
        public double UnitPrice { get; set; }  
        public long Quantity { get; set; }
        public double Total { get; set; }

    }
}
