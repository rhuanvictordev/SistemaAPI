using RestauranteAPI.Data;
using SistemaAPI.API.DTOs.Requests;
using SistemaAPI.API.Repositories;
using SistemaAPI.Models;

namespace SistemaAPI.API.Services
{
    public class GrupoUsuarioService
    {
        GrupoUsuarioRepository repository;

        public GrupoUsuarioService()
        {
            repository = new GrupoUsuarioRepository();
        }

        public RepositorioRetorno Save(CriarGrupoUsuarioDTO req)
        {
            GrupoUsuario g = new GrupoUsuario() { Nome = req.Nome, Descricao = req.Descricao };
            return repository.Save(g);
        }

        public RepositorioRetorno Query()
        {
            return repository.Query();
        }

        public GrupoUsuario GetById(long id)
        {
            RepositorioRetorno retorno = repository.GetById(id);
            if (retorno.Success)
            {
                return (GrupoUsuario)retorno.Result;
            }
            return null;
        }

        public RepositorioRetorno Delete(long id)
        {
            return repository.Delete(id);
        }

        public RepositorioRetorno Update(GrupoUsuario g)
        {
            return repository.Update(g);
        }
    }
}
