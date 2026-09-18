namespace SistemaAPI.API.DTOs.Requests
{
    public class CadastrarProdutoDTO
    {
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public required double Preco { get; set; }
        public required double Peso_KG { get; set; }
        public string? Imagem { get; set; }
    }
}
