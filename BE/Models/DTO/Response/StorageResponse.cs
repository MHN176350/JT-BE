using BE.Models.Entities;

namespace BE.Models.DTO.Response
{
    public class StorageResponse
    {
        public  int Id {  get; set; }
        public string Code { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int ItemCount { get; set; } = 0;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string OwnerName { get; set; } = null!;
    }
}
