namespace BE.Models.DTO.Request
{
    public class CreateProductRequest
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; }
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }= 0.0;
        public int CatId { get; set; } = 0;

    }
}
