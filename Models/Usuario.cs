using RestauranteAPI.Data;

namespace RestauranteAPI.Models
{
    public class Usuario
    {
        public long? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string Grupo { get; set; }
    }
}
