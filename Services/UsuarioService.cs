using RestauranteAPI.API.DTOs.Requests;
using RestauranteAPI.API.DTOs.Response;
using RestauranteAPI.Data;
using RestauranteAPI.Models;
using RestauranteAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAPI.Service
{
    public class UsuarioService
    {
        UsuarioRepository repository;

        public UsuarioService()
        {
            repository = new UsuarioRepository();
        }

        public RepositorioRetorno Save(RegisterRequest request)
        {
            Usuario u = new Usuario() { Id = 0, Nome = request.Nome, Email = request.Email, Senha = request.Senha, Grupo = request.Grupo };
            return repository.Save(u);
        }

        public RepositorioRetorno Query()
        { 
            return repository.Query();
        }

        public RepositorioRetorno GetById(long id)
        {
            return repository.GetById(id);
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
