using MySqlConnector;
using RestauranteAPI.Data;
using SistemaAPI.Models;

namespace SistemaAPI.API.Repositories
{
    public class ProdutoRepository
    {
        public RepositorioRetorno Save(Produto p)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"INSERT INTO PRODUTOS (NOME, DESCRICAO, PRECO, PESO_KG, IMAGEM, CRIADO, ALTERADO) VALUES (@nome, @descricao, @preco, @peso_kg, @imagem, @criado, @alterado)";
                    command.Parameters.AddWithValue("@nome", p.Nome);
                    command.Parameters.AddWithValue("@descricao", p.Descricao);
                    command.Parameters.AddWithValue("@preco", p.Preco);
                    command.Parameters.AddWithValue("@peso_kg", p.Peso_KG);
                    command.Parameters.AddWithValue("@imagem", p.Imagem);
                    command.Parameters.AddWithValue("@criado", DateTime.Now);
                    command.Parameters.AddWithValue("@alterado", DateTime.Now);
                    command.ExecuteNonQuery();
                    p.IdProduto = command.LastInsertedId;
                    return new RepositorioRetorno() { Success = true, Result = p };
                }
            }

            catch (MySqlException ex) when (ex.Number == 1062)
            {
                return new RepositorioRetorno() { Success = false, Message = "Já existe um produto com essas informações" };
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
                    command.CommandText = @"SELECT IDPRODUTO, NOME, DESCRICAO, PRECO, PESO_KG, IMAGEM, CRIADO, ALTERADO FROM PRODUTOS WHERE IDPRODUTO = @id";
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Produto p = new Produto()
                            {
                                IdProduto = long.Parse(reader[0].ToString()),
                                Nome = reader[1].ToString(),
                                Descricao = reader[2].ToString(),
                                Preco = double.Parse(reader[3].ToString()),
                                Peso_KG = double.Parse(reader[4].ToString()),
                                Imagem = reader[5].ToString(),
                                Criado = DateTime.Parse(reader[6].ToString()),
                                Alterado = DateTime.Parse(reader[7].ToString()),
                            };

                            return new RepositorioRetorno() { Success = true, Result = p };
                        }
                    }
                }
                return new RepositorioRetorno() { Success = false, Message = "Produto não encontrado." };
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
                List<Produto> lista = new List<Produto>();

                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @" SELECT IDPRODUTO, NOME, DESCRICAO, PRECO, PESO_KG, IMAGEM, CRIADO, ALTERADO FROM PRODUTOS";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Produto()
                            {
                                IdProduto = long.Parse(reader[0].ToString()),
                                Nome = reader[1].ToString(),
                                Descricao = reader[2].ToString(),
                                Preco = double.Parse(reader[3].ToString()),
                                Peso_KG = double.Parse(reader[4].ToString()),
                                Imagem = reader[5].ToString(),
                                Criado = DateTime.Parse(reader[6].ToString()),
                                Alterado = DateTime.Parse(reader[7].ToString()),
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

        public RepositorioRetorno Update(Produto p)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"UPDATE PRODUTOS SET NOME = @nome, DESCRICAO = @descricao, PRECO = @preco, PESO_KG = @peso_kg, IMAGEM = @imagem, ALTERADO = @alterado WHERE IDPRODUTO = @id";
                    command.Parameters.AddWithValue("@nome", p.Nome);
                    command.Parameters.AddWithValue("@descricao", p.Descricao);
                    command.Parameters.AddWithValue("@preco", p.Preco);
                    command.Parameters.AddWithValue("@peso_kg", p.Peso_KG);
                    command.Parameters.AddWithValue("@imagem", p.Imagem);
                    command.Parameters.AddWithValue("@alterado", DateTime.Now);
                    command.Parameters.AddWithValue("@id", p.IdProduto);
                    bool success = command.ExecuteNonQuery() > 0;

                    return new RepositorioRetorno() { Success = success, Result = p, Message = success ? null : "Produto não encontrado." };
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
                    command.CommandText = @"DELETE FROM PRODUTOS WHERE IDPRODUTO = @id";
                    command.Parameters.AddWithValue("@id", id);
                    bool success = command.ExecuteNonQuery() > 0;
                    return new RepositorioRetorno() { Success = success, Message = success ? "Produto deletado com sucesso" : "Produto não encontrado." };
                }
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno() { Success = false, Message = ex.Message };
            }
        }
    }
}
