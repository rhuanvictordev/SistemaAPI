using MySqlConnector;
using RestauranteAPI.Data;

namespace SistemaAPI.Models
{
    public class Fornecedor
    {
        public long? IdFornecedor { get; set; }
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

        public Fornecedor()
        {
        }

        public RepositorioRetorno Save()
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = "INSERT INTO FORNECEDORES (NOME, ESTADO, CIDADE, BAIRRO, NUMERO, CPF_CNPJ, TELEFONE1, TELEFONE2, CRIADO, ALTERADO) VALUES (@nome, @estado, @cidade, @bairro, @numero, @cpf_cnpj, @telefone1, @telefone2, @criado, @alterado)";
                    command.Parameters.AddWithValue("@nome", this.Nome);
                    command.Parameters.AddWithValue("@estado", this.Estado);
                    command.Parameters.AddWithValue("@cidade", this.Cidade);
                    command.Parameters.AddWithValue("@bairro", this.Bairro);
                    command.Parameters.AddWithValue("@numero", this.Numero);
                    command.Parameters.AddWithValue("@cpf_cnpj", this.CPF_CNPJ);
                    command.Parameters.AddWithValue("@telefone1", this.Telefone1);
                    command.Parameters.AddWithValue("@telefone2", this.Telefone2);
                    command.Parameters.AddWithValue("@criado", DateTime.Now);
                    command.Parameters.AddWithValue("@alterado", DateTime.Now);
                    command.ExecuteNonQuery();
                    this.IdFornecedor = command.LastInsertedId;
                    return new RepositorioRetorno { Success = true, Result = this, Message = "Fornecedor salvo com sucesso" };
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062) // 1062 constraint violation exception
            {
                return new RepositorioRetorno() { Success = false, Message = "Já existe um fornecedor com essas informações" };
            }

            catch (Exception ex)
            {
                return new RepositorioRetorno() { Success = false, Message = ex.Message };
            }
        }


    }
}
