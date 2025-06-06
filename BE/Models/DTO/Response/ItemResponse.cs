namespace BE.Models.DTO.Response
{
    public class ItemResponse
    {
        public int Id { get; set; }
        public long quantity { get; set; }
        public DateTime createdAt { get; set; }
        public double unitPrice { get; set; }
        public DateTime updatedAt { get; set; }
        public string productName { get; set; }
        public string productImage { get; set; }
        public long totalAmount { get; set; }
    }
}