
namespace RestauranteAPI.API.DTOs.Requests
{
    public class RegisterRequestDTO
    {
        public required string Nome {  get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required long IdGrupo { get; set; }
    }
}
