namespace BE.Models.DTO.Request
{
    public class CreateImportRequest
    {
         
        public  int SupplierId {  get; set; }
        public int StorageId {  get; set; }
        public List<CreateImportItemRequest> list { get; set; }
    }
}
