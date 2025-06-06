namespace BE.Models.DTO.Response
{
    public class ExportResponse
    {
        public string Code { get; set; } = null!;
        public string Location { get; set; } = null!;
        public List<ItemResponse> Items { get; set; } = new List<ItemResponse>();
    }
}
