
namespace RestauranteAPI.API.DTOs.Response
{
    public class APIResponseDTO
    {
        public int? Status { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }
    }
}
