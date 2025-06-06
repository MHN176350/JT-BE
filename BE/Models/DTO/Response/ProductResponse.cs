namespace BE.Models.DTO.Response
{
    public class ProductResponse
    {
        public int id { get; set; }
        public string code { get; set; } = null!;
        public string name { get; set; } = null!;
        public string image { get; set; } = null!;
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public double price { get; set; } = 0.0;
        public string description { get; set; } = null!;
        public string categoryName { get; set; } = null!;

    }
}
