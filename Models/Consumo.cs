namespace SistemaAPI.Models
{
    public class Consumo
    {
        public long? IdConsumo { get; set; }
        public long? IdMesa { get; set; }
        public DateTime? Inicio_Atendimento { get; set; }
        public DateTime? Fim_Atendimento { get; set; }
        public string? Cliente_Nome { get; set; }
        public int? Qtd_Pessoas { get; set; }
    }
}
