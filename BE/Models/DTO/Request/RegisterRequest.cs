namespace BE.Models.DTO.Request
{
    public class RegisterRequest
    {
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public string username { get; set; } = null!;
        public string password { get; set; } = null!;
    }
}
