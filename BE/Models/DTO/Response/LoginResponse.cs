namespace BE.Models.DTO.Response
{
    public class LoginResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public DateTime LastLogin { get; set; }
        public string Token { get; set; }
    }
}
