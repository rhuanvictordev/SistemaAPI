namespace SistemaAPI.Models
{
    public class GrupoUsuario
    {
        public long? IdGrupo {  get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? Criado { get; set; }
        public DateTime? Alterado { get; set; }
    }
}
