using MySqlConnector;
using RestauranteAPI.Data;
using RestauranteAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAPI.API.Repositories
{
    public class UsuarioRepository
    {
        public RepositorioRetorno Save(Usuario u)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"INSERT INTO USUARIOS (NOME, EMAIL, SENHA, IDGRUPO, CRIADO, ALTERADO) VALUES (@nome, @email, @senha, @idGrupo, @criado, @alterado)";
                    command.Parameters.AddWithValue("@nome", u.Nome);
                    command.Parameters.AddWithValue("@email", u.Email);
                    command.Parameters.AddWithValue("@senha", u.Senha);
                    command.Parameters.AddWithValue("@idGrupo", u.IdGrupo);
                    command.Parameters.AddWithValue("@criado", DateTime.Now);
                    command.Parameters.AddWithValue("@alterado", DateTime.Now);
                    command.ExecuteNonQuery();
                    u.IdUsuario = command.LastInsertedId;
                    return new RepositorioRetorno() { Success = true, Result = u };
                }
            }

            catch (MySqlException ex) when (ex.Number == 1062) // 1062 constraint violation exception
            {
                return new RepositorioRetorno() { Success = false, Message = "Já existe um usuário com essas informações" };
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
                    command.CommandText = @"SELECT IDUSUARIO, NOME, EMAIL, SENHA, IDGRUPO, CRIADO, ALTERADO FROM USUARIOS WHERE IDUSUARIO = @id";
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Usuario u = new Usuario()
                            {
                                IdUsuario = long.Parse(reader[0].ToString()),
                                Nome = reader[1].ToString(),
                                Email = reader[2].ToString(),
                                Senha = reader[3].ToString(),
                                IdGrupo = long.Parse(reader[4].ToString()),
                                Criado = DateTime.Parse(reader[5].ToString()),
                                Alterado = DateTime.Parse(reader[6].ToString()),
                            };

                            return new RepositorioRetorno() { Success = true, Result = u };
                        }
                    }
                }
                return new RepositorioRetorno() { Success = false, Message = "Usuário não encontrado." };
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno() { Success = false, Message = ex.Message };
            }
        }

        public RepositorioRetorno Login(string email, string senha)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"SELECT IDUSUARIO, NOME, EMAIL, SENHA, IDGRUPO, CRIADO, ALTERADO FROM USUARIOS WHERE EMAIL = @email AND SENHA = @senha";
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@senha", senha);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Usuario u = new Usuario()
                            {
                                IdUsuario = long.Parse(reader[0].ToString()),
                                Nome = reader[1].ToString(),
                                Email = reader[2].ToString(),
                                Senha = reader[3].ToString(),
                                IdGrupo = long.Parse(reader[4].ToString()),
                                Criado = DateTime.Parse(reader[5].ToString()),
                                Alterado = DateTime.Parse(reader[6].ToString()),
                            };

                            return new RepositorioRetorno() { Success = true, Result = u };
                        }
                    }
                }
                return new RepositorioRetorno() { Success = false, Message = "Usuário não encontrado." };
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
                List<Usuario> lista = new List<Usuario>();

                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @" SELECT IDUSUARIO, NOME, EMAIL, SENHA, IDGRUPO, CRIADO, ALTERADO FROM USUARIOS";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsuario = long.Parse(reader[0].ToString()),
                                Nome = reader[1].ToString(),
                                Email = reader[2].ToString(),
                                Senha = reader[3].ToString(),
                                IdGrupo = long.Parse(reader[4].ToString()),
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



        public RepositorioRetorno Update(Usuario u)
        {
            try
            {
                using (var command = Database.Connect().CreateCommand())
                {
                    command.CommandText = @"UPDATE USUARIOS SET NOME = @nome, EMAIL = @email, SENHA = @senha, IDGRUPO = @idGrupo, ALTERADO = @alterado WHERE IDUSUARIO = @id";
                    command.Parameters.AddWithValue("@nome", u.Nome);
                    command.Parameters.AddWithValue("@email", u.Email);
                    command.Parameters.AddWithValue("@senha", u.Senha);
                    command.Parameters.AddWithValue("@idGrupo", u.IdGrupo);
                    command.Parameters.AddWithValue("@alterado", DateTime.Now);
                    command.Parameters.AddWithValue("@id", u.IdUsuario);
                    bool success = command.ExecuteNonQuery() > 0;

                    return new RepositorioRetorno() { Success = success, Result = u, Message = success ? null : "Usuário não encontrado." };
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
                    command.CommandText = @"DELETE FROM USUARIOS WHERE IDUSUARIO = @id";
                    command.Parameters.AddWithValue("@id", id);
                    bool success = command.ExecuteNonQuery() > 0;
                    return new RepositorioRetorno() { Success = success, Message = success ? "Usuario deletado com sucesso" : "Usuário não encontrado." };
                }
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno() { Success = false, Message = ex.Message };
            }
        }
    }
}
