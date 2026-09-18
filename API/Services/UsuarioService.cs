using RestauranteAPI.API.DTOs.Requests;
using RestauranteAPI.API.DTOs.Response;
using RestauranteAPI.Data;
using RestauranteAPI.Models;
using SistemaAPI.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAPI.API.Services
{
    public class UsuarioService
    {
        UsuarioRepository repository;

        public UsuarioService()
        {
            repository = new UsuarioRepository();
        }

        public RepositorioRetorno Save(RegisterDTO request)
        {
            Usuario u = new Usuario() { IdUsuario = 0, Nome = request.Nome, Email = request.Email, Senha = request.Senha, IdGrupo = request.IdGrupo };
            return repository.Save(u);
        }

        public RepositorioRetorno Query()
        { 
            return repository.Query();
        }

        public Usuario GetById(long id)
        {
            RepositorioRetorno retorno = repository.GetById(id);
            if (retorno.Success) 
            {
                return (Usuario) retorno.Result;
            }
            return null;
        }

        public RepositorioRetorno Delete(long id)
        { 
            return repository.Delete(id);
        }

        public RepositorioRetorno Update(Usuario u)
        {
            return repository.Update(u);
        }
    }
}
