using RestauranteAPI.Data;
using RestauranteAPI.Framework;

namespace RestauranteAPI.Models
{
    public class Usuario : BaseModel<Usuario>
    {
        public long Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string SenhaHash { get; set; }

        public override List<Parametro> CriarParametros()
        {
            return new List<Parametro>()
            {
                new Parametro("USUARIOS", true, "ID", this.Id, nameof(Id)),
                new Parametro("USUARIOS", false, "NOME", this.Nome, nameof(Nome)),
                new Parametro("USUARIOS", false, "EMAIL", this.Email, nameof(Email)),
                new Parametro("USUARIOS", false, "SENHAHASH", this.SenhaHash, nameof(SenhaHash))
            };
            
        }
    }
}
