namespace SistemaAPI.API.DTOs.Requests
{
    public class CriarFornecedorDTO
    {
        public required string Nome { get; set; }
        public required string Estado { get; set; }
        public required string Cidade { get; set; }
        public required string Bairro { get; set; }
        public required string Numero { get; set; }
        public required string CPF_CNPJ { get; set; }
        public required string? Telefone1 { get; set; }
        public required string? Telefone2 { get; set; }
    }
}
