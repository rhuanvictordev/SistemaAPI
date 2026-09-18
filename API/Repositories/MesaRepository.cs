using MySqlConnector;
using RestauranteAPI.Data;
using SistemaAPI.Models;

namespace SistemaAPI.API.Repositories
{
    public class MesaRepository
    {
        public RepositorioRetorno Save(Mesa m)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"INSERT INTO MESAS (NOME, DESCRICAO, STATUS, IDCONSUMO, CRIADO, ALTERADO) VALUES (@nome, @descricao, @status, @idConsumo, @criado, @alterado)";
                    command.Parameters.AddWithValue("@nome", m.Nome);
                    command.Parameters.AddWithValue("@descricao", m.Descricao);
                    command.Parameters.AddWithValue("@status", m.Status);
                    command.Parameters.AddWithValue("@idConsumo", m.IdConsumo);
                    command.Parameters.AddWithValue("@criado", DateTime.Now);
                    command.Parameters.AddWithValue("@alterado", DateTime.Now);
                    command.ExecuteNonQuery();
                    m.IdMesa = command.LastInsertedId;
                    return new RepositorioRetorno() { Success = true, Result = m };
                }
            }

            catch (MySqlException ex) when (ex.Number == 1062)
            {
                return new RepositorioRetorno() { Success = false, Message = "Já existe uma mesa com essas informações" };
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
                    command.CommandText = @"SELECT IDMESA, NOME, DESCRICAO, STATUS, IDCONSUMO, CRIADO, ALTERADO FROM MESAS WHERE IDMESA = @id";
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Mesa m = new Mesa()
                            {
                                IdMesa = long.Parse(reader[0].ToString()),
                                Nome = reader[1].ToString(),
                                Descricao = reader[2].ToString(),
                                Status = reader[3].ToString(),
                                IdConsumo = long.Parse(reader[4].ToString()),
                                Criado = DateTime.Parse(reader[5].ToString()),
                                Alterado = DateTime.Parse(reader[6].ToString()),
                            };

                            return new RepositorioRetorno() { Success = true, Result = m };
                        }
                    }
                }
                return new RepositorioRetorno() { Success = false, Message = "Mesa não encontrada." };
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
                List<Mesa> lista = new List<Mesa>();

                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"SELECT IDMESA, NOME, DESCRICAO, STATUS, IDCONSUMO, CRIADO, ALTERADO FROM MESAS";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Mesa()
                            {
                                IdMesa = long.Parse(reader[0].ToString()),
                                Nome = reader[1].ToString(),
                                Descricao = reader[2].ToString(),
                                Status = reader[3].ToString(),
                                IdConsumo = reader[4] == DBNull.Value ? null : long.Parse(reader[4].ToString()),
                                Criado = DateTime.Parse(reader[5].ToString()),
                                Alterado = DateTime.Parse(reader[6].ToString()),
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

        public RepositorioRetorno Update(Mesa m)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"UPDATE MESAS SET NOME = @nome, DESCRICAO = @descricao, STATUS = @status, IDCONSUMO = @idConsumo, ALTERADO = @alterado WHERE IDMESA = @id";
                    command.Parameters.AddWithValue("@nome", m.Nome);
                    command.Parameters.AddWithValue("@descricao", m.Descricao);
                    command.Parameters.AddWithValue("@status", m.Status);
                    command.Parameters.AddWithValue("@idConsumo", m.IdConsumo);
                    command.Parameters.AddWithValue("@alterado", DateTime.Now);
                    command.Parameters.AddWithValue("@id", m.IdMesa);
                    bool success = command.ExecuteNonQuery() > 0;

                    return new RepositorioRetorno() { Success = success, Result = m, Message = success ? null : "Mesa não encontrada." };
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
                    command.CommandText = @"DELETE FROM MESAS WHERE IDMESA = @id";
                    command.Parameters.AddWithValue("@id", id);
                    bool success = command.ExecuteNonQuery() > 0;
                    return new RepositorioRetorno() { Success = success, Message = success ? "Mesa deletada com sucesso" : "Mesa não encontrada." };
                }
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno() { Success = false, Message = ex.Message };
            }
        }
    }
}
