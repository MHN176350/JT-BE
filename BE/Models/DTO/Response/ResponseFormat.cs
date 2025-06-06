namespace BE.Models.DTO.Response
{
    public class ResponseFormat
    {
        public object? Data { get; set; }
        public string? Message { get; set; }
        public int statusCode { get; set; }
    }
}
