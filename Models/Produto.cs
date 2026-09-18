namespace SistemaAPI.Models
{
    public class Produto
    {
        public long? IdProduto { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public double? Preco {  get; set; }
        public double? Peso_KG { get; set; }
        public string? Imagem { get; set; }
        public DateTime? Criado { get; set; }
        public DateTime? Alterado { get; set; }

    }
}
