namespace BE.Models.DTO.Response
{
    public class StorageMemberResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Privilage { get; set; }
        public string Avatar { get; set; }
        public DateTime LastLogin { get; set; }

    }
}
