    namespace BE.Models.DTO.Request
{
    public class UpdateUserRequest
    {
        public string Address {  get; set; }
        public bool IsAdmin {  get; set; }
        public string PhoneNumber {  get; set; }
        public string Avater { get; set; }

    }
}
