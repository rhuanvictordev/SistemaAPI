using MySqlConnector;
using RestauranteAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAPI.Repository
{
    public class UsuarioRepository
    {
        string connectionString = "Server=localhost;Database=api;User ID=root;Password=root;";
        
        public UsuarioRepository() { }

        public RepositorioRetorno Save(Usuario u)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                    INSERT INTO USUARIOS (NOME, EMAIL, SENHA)
                    VALUES (@nome, @email, @senha)";

                        command.Parameters.AddWithValue("@nome", u.Nome);
                        command.Parameters.AddWithValue("@email", u.Email);
                        command.Parameters.AddWithValue("@senha", u.Senha);

                        command.ExecuteNonQuery();

                        u.Id = command.LastInsertedId;

                        return new RepositorioRetorno()
                        {
                            Success = true,
                            Result = u
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public RepositorioRetorno Load(long id)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                    SELECT ID, NOME, EMAIL, SENHA
                    FROM USUARIOS
                    WHERE ID = @id";

                        command.Parameters.AddWithValue("@id", id);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Usuario u = new Usuario()
                                {
                                    Id = reader.GetInt64(reader.GetOrdinal("ID")),
                                    Nome = reader.GetString(reader.GetOrdinal("NOME")),
                                    Email = reader.GetString(reader.GetOrdinal("EMAIL")),
                                    Senha = reader.GetString(reader.GetOrdinal("SENHA"))
                                };

                                return new RepositorioRetorno()
                                {
                                    Success = true,
                                    Result = u
                                };
                            }
                        }
                    }
                }

                return new RepositorioRetorno()
                {
                    Success = false,
                    Message = "Usuário não encontrado."
                };
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public RepositorioRetorno Query()
        {
            try
            {
                List<Usuario> lista = new List<Usuario>();

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                    SELECT ID, NOME, EMAIL, SENHA
                    FROM USUARIOS";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Usuario()
                                {
                                    Id = reader.GetInt64(reader.GetOrdinal("ID")),
                                    Nome = reader.GetString(reader.GetOrdinal("NOME")),
                                    Email = reader.GetString(reader.GetOrdinal("EMAIL")),
                                    Senha = reader.GetString(reader.GetOrdinal("SENHA"))
                                });
                            }
                        }
                    }
                }

                return new RepositorioRetorno()
                {
                    Success = true,
                    Result = lista
                };
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public RepositorioRetorno Update(Usuario u)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                    UPDATE USUARIOS
                    SET NOME = @nome,
                        EMAIL = @email,
                        SENHA = @senha
                    WHERE ID = @id";

                        command.Parameters.AddWithValue("@id", u.Id);
                        command.Parameters.AddWithValue("@nome", u.Nome);
                        command.Parameters.AddWithValue("@email", u.Email);
                        command.Parameters.AddWithValue("@senha", u.Senha);

                        bool success = command.ExecuteNonQuery() > 0;

                        return new RepositorioRetorno()
                        {
                            Success = success,
                            Result = u,
                            Message = success ? null : "Usuário não encontrado."
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public RepositorioRetorno Delete(long id)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                    DELETE FROM USUARIOS
                    WHERE ID = @id";

                        command.Parameters.AddWithValue("@id", id);

                        bool success = command.ExecuteNonQuery() > 0;

                        return new RepositorioRetorno()
                        {
                            Success = success,
                            Message = success
                                ? null
                                : "Usuário não encontrado."
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new RepositorioRetorno()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
