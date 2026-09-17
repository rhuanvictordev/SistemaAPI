namespace SistemaAPI.API.DTOs.Requests
{
    public class CriarFornecedorRequestDTO
    {
        public string? Nome { get; set; }
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public string? Bairro { get; set; }
        public string? Numero { get; set; }
        public string? CPF_CNPJ { get; set; }
        public string? Telefone1 { get; set; }
        public string? Telefone2 { get; set; }
        public DateTime? Criado { get; set; }
        public DateTime? Alterado { get; set; }
    }
}
