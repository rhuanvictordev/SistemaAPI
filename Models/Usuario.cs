using RestauranteAPI.Data;

namespace RestauranteAPI.Models
{
    public class Usuario
    {
        public long? IdUsuario { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public long? IdGrupo { get; set; }
        public DateTime? Criado { get; set; }
        public DateTime? Alterado { get; set; }
    }
}
