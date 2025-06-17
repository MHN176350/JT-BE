namespace BE.Models.DTO.Response
{
    public class UserResponse
    {
        public int Id {  get; set; }
        public string UserName { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Avatar {  get; set; }
        public bool IsAdmin {  get; set; }
        public bool IsActive {  get; set; }
        public DateTime LastLogin {  get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
