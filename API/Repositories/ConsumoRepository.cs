using MySqlConnector;
using RestauranteAPI.Data;
using SistemaAPI.Models;

namespace SistemaAPI.API.Repositories
{
    public class ConsumoRepository
    {
        public RepositorioRetorno Save(Consumo c)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"INSERT INTO CONSUMOS (IDMESA, INICIO_ATENDIMENTO, FIM_ATENDIMENTO, CLIENTE_NOME, QTD_PESSOAS) VALUES (@idMesa, @inicio_atendimento, @fim_atendimento, @cliente_nome, @qtd_pessoas)";
                    command.Parameters.AddWithValue("@idMesa", c.IdMesa);
                    command.Parameters.AddWithValue("@inicio_atendimento", DateTime.Now);
                    command.Parameters.AddWithValue("@fim_atendimento", null);
                    command.Parameters.AddWithValue("@cliente_nome", c.Cliente_Nome);
                    command.Parameters.AddWithValue("@qtd_pessoas", c.Qtd_Pessoas);
                    command.ExecuteNonQuery();
                    c.IdConsumo = command.LastInsertedId;
                    return new RepositorioRetorno() { Success = true, Result = c };
                }
            }

            catch (MySqlException ex) when (ex.Number == 1062)
            {
                return new RepositorioRetorno() { Success = false, Message = "Já existe um consumo com essas informações" };
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
                    command.CommandText = @"SELECT IDCONSUMO, IDMESA, INICIO_ATENDIMENTO, FIM_ATENDIMENTO, CLIENTE_NOME, QTD_PESSOAS FROM CONSUMOS WHERE IDCONSUMO = @id";
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Consumo c = new Consumo()
                            {
                                IdConsumo = long.Parse(reader[0].ToString()),
                                IdMesa = long.Parse(reader[1].ToString()),
                                Inicio_Atendimento = DateTime.Parse(reader[2].ToString()),
                                Fim_Atendimento = reader[3] == DBNull.Value ? null : DateTime.Parse(reader[3].ToString()),
                                Cliente_Nome = reader[4] == DBNull.Value ? null : reader[4].ToString(),
                                Qtd_Pessoas = reader[5] == DBNull.Value ? null : int.Parse(reader[5].ToString()),
                            };

                            return new RepositorioRetorno() { Success = true, Result = c };
                        }
                    }
                }
                return new RepositorioRetorno() { Success = false, Message = "Consumo não encontrado." };
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
                List<Consumo> lista = new List<Consumo>();

                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @" SELECT IDCONSUMO, IDMESA, INICIO_ATENDIMENTO, FIM_ATENDIMENTO, CLIENTE_NOME, QTD_PESSOAS FROM CONSUMOS";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Consumo()
                            {
                                IdConsumo = long.Parse(reader[0].ToString()),
                                IdMesa = long.Parse(reader[1].ToString()),
                                Inicio_Atendimento = DateTime.Parse(reader[2].ToString()),
                                Fim_Atendimento = reader[3] == DBNull.Value ? null : DateTime.Parse(reader[3].ToString()),
                                Cliente_Nome = reader[4] == DBNull.Value ? null : reader[4].ToString(),
                                Qtd_Pessoas = reader[5] == DBNull.Value ? null : int.Parse(reader[5].ToString()),
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

        public RepositorioRetorno Update(Consumo c)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"UPDATE CONSUMOS SET IDMESA = @idMesa, INICIO_ATENDIMENTO = @inicio_atendimento, FIM_ATENDIMENTO = @fim_atendimento, CLIENTE_NOME = @cliente_nome, QTD_PESSOAS = @qtd_pessoas WHERE IDCONSUMO = @id";
                    command.Parameters.AddWithValue("@idMesa", c.IdMesa);
                    command.Parameters.AddWithValue("@inicio_atendimento", c.Inicio_Atendimento);
                    command.Parameters.AddWithValue("@fim_atendimento", c.Fim_Atendimento);
                    command.Parameters.AddWithValue("@cliente_nome", c.Cliente_Nome);
                    command.Parameters.AddWithValue("@qtd_pessoas", c.Qtd_Pessoas);
                    command.Parameters.AddWithValue("@id", c.IdConsumo);
                    bool success = command.ExecuteNonQuery() > 0;

                    return new RepositorioRetorno() { Success = success, Result = c, Message = success ? null : "Consumo não encontrado." };
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
                    command.CommandText = @"DELETE FROM CONSUMOS WHERE IDCONSUMO = @id";
                    command.Parameters.AddWithValue("@id", id);
                    bool success = command.ExecuteNonQuery() > 0;
                    return new RepositorioRetorno() { Success = success, Message = success ? "Consumo deletado com sucesso" : "Consumo não encontrado." };
                }
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno() { Success = false, Message = ex.Message };
            }
        }
    }
}
