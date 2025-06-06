namespace BE.Models.DTO.Response
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public long Points { get; set; }
        }
}
