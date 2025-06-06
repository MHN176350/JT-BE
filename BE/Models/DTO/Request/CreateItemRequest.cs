namespace BE.Models.DTO.Request
{
    public class CreateItemRequest
    {
        public int StorageId { get; set; }
        public long Quantity { get; set; }
        public int ProductId { get; set; }
        
    }
}
