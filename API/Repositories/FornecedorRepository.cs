using MySqlConnector;
using RestauranteAPI.Data;
using SistemaAPI.Models;

namespace SistemaAPI.API.Repositories
{
    public class FornecedorRepository
    {


        public RepositorioRetorno Save(Fornecedor f)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"INSERT INTO FORNECEDORES (NOME, ESTADO, CIDADE, BAIRRO, NUMERO, CPF_CNPJ, TELEFONE1, TELEFONE2, CRIADO, ALTERADO) VALUES (@nome, @estado, @cidade, @bairro, @numero, @cpf_cnpj, @telefone1, @telefone2, @criado, @alterado)";
                    command.Parameters.AddWithValue("@nome", f.Nome);
                    command.Parameters.AddWithValue("@estado", f.Estado);
                    command.Parameters.AddWithValue("@cidade", f.Cidade);
                    command.Parameters.AddWithValue("@bairro", f.Bairro);
                    command.Parameters.AddWithValue("@numero", f.Numero);
                    command.Parameters.AddWithValue("@cpf_cnpj", f.CPF_CNPJ);
                    command.Parameters.AddWithValue("@telefone1", f.Telefone1);
                    command.Parameters.AddWithValue("@telefone2", f.Telefone2);
                    command.Parameters.AddWithValue("@criado", DateTime.Now);
                    command.Parameters.AddWithValue("@alterado", DateTime.Now);
                    command.ExecuteNonQuery();
                    f.IdFornecedor = command.LastInsertedId;
                    return new RepositorioRetorno() { Success = true, Result = f };
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


        public RepositorioRetorno GetById(long id)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"SELECT IDFORNECEDOR, NOME, ESTADO, CIDADE, BAIRRO, NUMERO, CPF_CNPJ, TELEFONE1, TELEFONE2, CRIADO, ALTERADO FROM FORNECEDORES WHERE IDFORNECEDOR = @id";
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Fornecedor f = new Fornecedor()
                            {
                                IdFornecedor = long.Parse(reader[0].ToString()),
                                Nome = reader[1].ToString(),
                                Estado = reader[2].ToString(),
                                Cidade = reader[3].ToString(),
                                Bairro = reader[4].ToString(),
                                Numero = reader[5].ToString(),
                                CPF_CNPJ = reader[6].ToString(),
                                Telefone1 = reader[7].ToString(),
                                Telefone2 = reader[8].ToString(),
                                Criado = DateTime.Parse(reader[9].ToString()),
                                Alterado = DateTime.Parse(reader[10].ToString()),
                            };

                            return new RepositorioRetorno() { Success = true, Result = f };
                        }
                    }
                }
                return new RepositorioRetorno() { Success = false, Message = "Fornecedor não encontrado." };
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno() { Success = false, Message = ex.Message };
            }
        }


        public RepositorioRetorno Query()
        {
            try
            {
                List<Fornecedor> lista = new List<Fornecedor>();

                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @" SELECT IDFORNECEDOR, NOME, ESTADO, CIDADE, BAIRRO, NUMERO, CPF_CNPJ, TELEFONE1, TELEFONE2, CRIADO, ALTERADO FROM FORNECEDORES";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Fornecedor()
                            {
                                IdFornecedor = long.Parse(reader[0].ToString()),
                                Nome = reader[1].ToString(),
                                Estado = reader[2].ToString(),
                                Cidade = reader[3].ToString(),
                                Bairro = reader[4].ToString(),
                                Numero = reader[5].ToString(),
                                CPF_CNPJ = reader[6].ToString(),
                                Telefone1 = reader[7].ToString(),
                                Telefone2 = reader[8].ToString(),
                                Criado = DateTime.Parse(reader[9].ToString()),
                                Alterado = DateTime.Parse(reader[10].ToString()),
                            });
                        }
                    }
                }

                return new RepositorioRetorno() { Success = true, Result = lista };
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno() { Success = false, Message = ex.Message };
            }
        }


        public RepositorioRetorno Update(Fornecedor f)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"UPDATE FORNECEDORES SET NOME = @nome, ESTADO = @estado, CIDADE = @cidade, BAIRRO = @bairro, NUMERO = @numero, CPF_CNPJ = @cpf_cnpj, TELEFONE1 = @telefone1, TELEFONE2 = @telefone2, ALTERADO = @alterado WHERE IDFORNECEDOR = @id";
                    command.Parameters.AddWithValue("@nome", f.Nome);
                    command.Parameters.AddWithValue("@estado", f.Estado);
                    command.Parameters.AddWithValue("@cidade", f.Cidade);
                    command.Parameters.AddWithValue("@bairro", f.Bairro);
                    command.Parameters.AddWithValue("@numero", f.Numero);
                    command.Parameters.AddWithValue("@cpf_cnpj", f.CPF_CNPJ);
                    command.Parameters.AddWithValue("@telefone1", f.Telefone1);
                    command.Parameters.AddWithValue("@telefone2", f.Telefone2);
                    command.Parameters.AddWithValue("@alterado", DateTime.Now);
                    command.Parameters.AddWithValue("@id", f.IdFornecedor);
                    bool success = command.ExecuteNonQuery() > 0;

                    return new RepositorioRetorno() { Success = success, Result = f, Message = success ? null : "Fornecedor não encontrado." };
                }
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno() { Success = false, Message = ex.Message };
            }
        }



        public RepositorioRetorno Delete(long id)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"DELETE FROM FORNECEDORES WHERE IDFORNECEDOR = @id";
                    command.Parameters.AddWithValue("@id", id);
                    bool success = command.ExecuteNonQuery() > 0;
                    return new RepositorioRetorno() { Success = success, Message = success ? "Fornecedor deletado com sucesso" : "Fornecedor não encontrado." };
                }
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno() { Success = false, Message = ex.Message };
            }
        }



    }
}
