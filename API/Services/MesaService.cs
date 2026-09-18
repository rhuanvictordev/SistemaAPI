using RestauranteAPI.Data;
using SistemaAPI.API.DTOs.Requests;
using SistemaAPI.API.Enums;
using SistemaAPI.API.Repositories;
using SistemaAPI.Models;

namespace SistemaAPI.API.Services
{
    public class MesaService
    {
        MesaRepository repository;

        public MesaService()
        {
            repository = new MesaRepository();
        }

        public RepositorioRetorno Save(CadastrarMesaDTO req)
        {
            Mesa m = new Mesa() { Nome = req.Nome, Descricao = req.Descricao, Status = MesaStatus.MESA_LIVRE, IdConsumo = null };
            return repository.Save(m);
        }

        public RepositorioRetorno Query()
        {
            return repository.Query();
        }

        public Mesa GetById(long id)
        {
            RepositorioRetorno retorno = repository.GetById(id);
            if (retorno.Success)
            {
                return (Mesa)retorno.Result;
            }
            return null;
        }

        public RepositorioRetorno Delete(long id)
        {
            return repository.Delete(id);
        }

        public RepositorioRetorno Update(Mesa m)
        {
            return repository.Update(m);
        }
    }
}
