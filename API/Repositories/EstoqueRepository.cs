using MySqlConnector;
using RestauranteAPI.Data;
using SistemaAPI.Models;

namespace SistemaAPI.API.Repositories
{
    public class EstoqueRepository
    {
        public RepositorioRetorno Save(Estoque e)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"INSERT INTO ESTOQUE (NOME, DESCRICAO, PRECO, EAN, QTD, QTD_MIN, IDFORNECEDOR, REVENDA, PRECO_REVENDA, CRIADO, ALTERADO) VALUES (@nome, @descricao, @preco, @ean, @qtd, @qtd_min, @idFornecedor, @revenda, @preco_revenda, @criado, @alterado)";
                    command.Parameters.AddWithValue("@nome", e.Nome);
                    command.Parameters.AddWithValue("@descricao", e.Descricao);
                    command.Parameters.AddWithValue("@preco", e.Preco);
                    command.Parameters.AddWithValue("@ean", e.EAN);
                    command.Parameters.AddWithValue("@qtd", e.Qtd);
                    command.Parameters.AddWithValue("@qtd_min", e.Qtd_Min);
                    command.Parameters.AddWithValue("@idFornecedor", e.IdFornecedor);
                    command.Parameters.AddWithValue("@revenda", e.Revenda);
                    command.Parameters.AddWithValue("@preco_revenda", e.Preco_Revenda);
                    command.Parameters.AddWithValue("@criado", DateTime.Now);
                    command.Parameters.AddWithValue("@alterado", DateTime.Now);
                    command.ExecuteNonQuery();
                    e.IdEstoque = command.LastInsertedId;
                    return new RepositorioRetorno() { Success = true, Result = e };
                }
            }

            catch (MySqlException ex) when (ex.Number == 1062)
            {
                return new RepositorioRetorno() { Success = false, Message = "Já existe um estoque com essas informações" };
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
                    command.CommandText = @"SELECT IDESTOQUE, NOME, DESCRICAO, PRECO, EAN, QTD, QTD_MIN, IDFORNECEDOR, REVENDA, PRECO_REVENDA, CRIADO, ALTERADO FROM ESTOQUE WHERE IDESTOQUE = @id";
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Estoque e = new Estoque()
                            {
                                IdEstoque = long.Parse(reader[0].ToString()),
                                Nome = reader[1].ToString(),
                                Descricao = reader[2].ToString(),
                                Preco = double.Parse(reader[3].ToString()),
                                EAN = reader[4].ToString(),
                                Qtd = double.Parse(reader[5].ToString()),
                                Qtd_Min = double.Parse(reader[6].ToString()),
                                IdFornecedor = long.Parse(reader[7].ToString()),
                                Revenda = reader[8].ToString() == "S" ? true : false,
                                Preco_Revenda = double.Parse(reader[9].ToString()),
                                Criado = DateTime.Parse(reader[10].ToString()),
                                Alterado = DateTime.Parse(reader[11].ToString()),
                            };

                            return new RepositorioRetorno() { Success = true, Result = e };
                        }
                    }
                }
                return new RepositorioRetorno() { Success = false, Message = "Estoque não encontrado." };
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
                List<Estoque> lista = new List<Estoque>();

                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @" SELECT IDESTOQUE, NOME, DESCRICAO, PRECO, EAN, QTD, QTD_MIN, IDFORNECEDOR, REVENDA, PRECO_REVENDA, CRIADO, ALTERADO FROM ESTOQUE";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Estoque()
                            {
                                IdEstoque = long.Parse(reader[0].ToString()),
                                Nome = reader[1].ToString(),
                                Descricao = reader[2].ToString(),
                                Preco = double.Parse(reader[3].ToString()),
                                EAN = reader[4].ToString(),
                                Qtd = double.Parse(reader[5].ToString()),
                                Qtd_Min = double.Parse(reader[6].ToString()),
                                IdFornecedor = long.Parse(reader[7].ToString()),
                                Revenda = reader[8].ToString() == "S" ? true : false,
                                Preco_Revenda = double.Parse(reader[9].ToString()),
                                Criado = DateTime.Parse(reader[10].ToString()),
                                Alterado = DateTime.Parse(reader[11].ToString()),
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

        public RepositorioRetorno Update(Estoque e)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"UPDATE ESTOQUE SET NOME = @nome, DESCRICAO = @descricao, PRECO = @preco, EAN = @ean, QTD = @qtd, QTD_MIN = @qtd_min, IDFORNECEDOR = @idFornecedor, REVENDA = @revenda, PRECO_REVENDA = @preco_revenda, ALTERADO = @alterado WHERE IDESTOQUE = @id";
                    command.Parameters.AddWithValue("@nome", e.Nome);
                    command.Parameters.AddWithValue("@descricao", e.Descricao);
                    command.Parameters.AddWithValue("@preco", e.Preco);
                    command.Parameters.AddWithValue("@ean", e.EAN);
                    command.Parameters.AddWithValue("@qtd", e.Qtd);
                    command.Parameters.AddWithValue("@qtd_min", e.Qtd_Min);
                    command.Parameters.AddWithValue("@idFornecedor", e.IdFornecedor);
                    command.Parameters.AddWithValue("@revenda", e.Revenda == true ? "S" : "N");
                    command.Parameters.AddWithValue("@preco_revenda", e.Preco_Revenda);
                    command.Parameters.AddWithValue("@alterado", DateTime.Now);
                    command.Parameters.AddWithValue("@id", e.IdEstoque);
                    bool success = command.ExecuteNonQuery() > 0;

                    return new RepositorioRetorno() { Success = success, Result = e, Message = success ? null : "Estoque não encontrado." };
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
                    command.CommandText = @"DELETE FROM ESTOQUE WHERE IDESTOQUE = @id";
                    command.Parameters.AddWithValue("@id", id);
                    bool success = command.ExecuteNonQuery() > 0;
                    return new RepositorioRetorno() { Success = success, Message = success ? "Estoque deletado com sucesso" : "Estoque não encontrado." };
                }
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno() { Success = false, Message = ex.Message };
            }
        }
    }
}
