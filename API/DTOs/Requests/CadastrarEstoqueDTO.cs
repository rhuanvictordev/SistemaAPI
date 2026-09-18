namespace SistemaAPI.API.DTOs.Requests
{
    public class CadastrarEstoqueDTO
    {
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public required double Preco { get; set; }
        public required string EAN { get; set; }
        public required double Qtd { get; set; }
        public required double Qtd_Min { get; set; }
        public required long IdFornecedor { get; set; }
        public required bool Revenda { get; set; }
        public required double PrecoRevenda { get; set; }

    }
}
