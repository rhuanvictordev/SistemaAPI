
namespace RestauranteAPI.API.DTOs.Requests
{
    public class LoginRequestDTO
    {
        public required string Email {  get; set; }
        public required string Senha { get; set; }
    }
}
