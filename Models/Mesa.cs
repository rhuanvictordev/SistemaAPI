namespace SistemaAPI.Models
{
    public class Mesa
    {
        public long? IdMesa { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Status { get; set; }
        public long? IdConsumo { get; set; }
        public DateTime? Criado { get; set; }
        public DateTime? Alterado { get; set; }
    }
}
