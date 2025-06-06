namespace BE.Models.DTO.Request
{
    public class CreateImportItemRequest
    {
       public int ProductId {  get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }

    }
}
