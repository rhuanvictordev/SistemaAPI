namespace SistemaAPI.Models
{
    public class Estoque
    {
        public long? IdEstoque {  get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public double? Preco { get; set; }
        public string? EAN { get; set; }
        public double? Qtd { get; set; }
        public double? Qtd_Min { get; set; }
        public long? IdFornecedor { get; set; }
        public bool? Revenda { get; set; }
        public double? Preco_Revenda { get; set; }
        public DateTime? Criado { get; set; }
        public DateTime? Alterado { get; set; }
    }
}
