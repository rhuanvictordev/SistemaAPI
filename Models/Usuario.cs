using RestauranteAPI.Data;

namespace RestauranteAPI.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}
