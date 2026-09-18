using MySqlConnector;
using RestauranteAPI.Data;
using SistemaAPI.Models;

namespace SistemaAPI.API.Repositories
{
    public class GrupoUsuarioRepository
    {
        public RepositorioRetorno Save(GrupoUsuario g)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"INSERT INTO GRUPO_USUARIO (NOME, DESCRICAO, CRIADO, ALTERADO) VALUES (@nome, @descricao, @criado, @alterado)";
                    command.Parameters.AddWithValue("@nome", g.Nome);
                    command.Parameters.AddWithValue("@descricao", g.Descricao);
                    command.Parameters.AddWithValue("@criado", DateTime.Now);
                    command.Parameters.AddWithValue("@alterado", DateTime.Now);
                    command.ExecuteNonQuery();
                    g.IdGrupo = command.LastInsertedId;
                    return new RepositorioRetorno() { Success = true, Result = g };
                }
            }

            catch (MySqlException ex) when (ex.Number == 1062)
            {
                return new RepositorioRetorno() { Success = false, Message = "Já existe um grupo de usuário com essas informações" };
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
                    command.CommandText = @"SELECT IDGRUPO, NOME, DESCRICAO, CRIADO, ALTERADO FROM GRUPO_USUARIO WHERE IDGRUPO = @id";
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            GrupoUsuario g = new GrupoUsuario()
                            {
                                IdGrupo = long.Parse(reader[0].ToString()),
                                Nome = reader[1].ToString(),
                                Descricao = reader[2].ToString(),
                                Criado = DateTime.Parse(reader[3].ToString()),
                                Alterado = DateTime.Parse(reader[4].ToString()),
                            };

                            return new RepositorioRetorno() { Success = true, Result = g };
                        }
                    }
                }
                return new RepositorioRetorno() { Success = false, Message = "Grupo de usuário não encontrado." };
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
                List<GrupoUsuario> lista = new List<GrupoUsuario>();

                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @" SELECT IDGRUPO, NOME, DESCRICAO, CRIADO, ALTERADO FROM GRUPO_USUARIO";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new GrupoUsuario()
                            {
                                IdGrupo = long.Parse(reader[0].ToString()),
                                Nome = reader[1].ToString(),
                                Descricao = reader[2].ToString(),
                                Criado = DateTime.Parse(reader[3].ToString()),
                                Alterado = DateTime.Parse(reader[4].ToString()),
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

        public RepositorioRetorno Update(GrupoUsuario g)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"UPDATE GRUPO_USUARIO SET NOME = @nome, DESCRICAO = @descricao, ALTERADO = @alterado WHERE IDGRUPO = @id";
                    command.Parameters.AddWithValue("@nome", g.Nome);
                    command.Parameters.AddWithValue("@descricao", g.Descricao);
                    command.Parameters.AddWithValue("@alterado", DateTime.Now);
                    command.Parameters.AddWithValue("@id", g.IdGrupo);
                    bool success = command.ExecuteNonQuery() > 0;

                    return new RepositorioRetorno() { Success = success, Result = g, Message = success ? null : "Grupo de usuário não encontrado." };
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
                    command.CommandText = @"DELETE FROM GRUPO_USUARIO WHERE IDGRUPO = @id";
                    command.Parameters.AddWithValue("@id", id);
                    bool success = command.ExecuteNonQuery() > 0;
                    return new RepositorioRetorno() { Success = success, Message = success ? "Grupo de usuário deletado com sucesso" : "Grupo de usuário não encontrado." };
                }
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno() { Success = false, Message = ex.Message };
            }
        }
    }
}
