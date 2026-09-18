namespace SistemaAPI.API.DTOs.Requests
{
    public class CadastrarConsumoDTO
    {
        public required long IdMesa { get; set; }
        public required string Cliente_Nome { get; set; }
        public required int Qtd_Pessoas { get; set; }
    }
}
