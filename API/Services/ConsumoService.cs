using RestauranteAPI.Data;
using SistemaAPI.API.DTOs.Requests;
using SistemaAPI.API.Repositories;
using SistemaAPI.Models;

namespace SistemaAPI.API.Services
{
    public class ConsumoService
    {
        ConsumoRepository repository;

        public ConsumoService()
        {
            repository = new ConsumoRepository();
        }

        public RepositorioRetorno Save(CadastrarConsumoDTO req)
        {
            Consumo c = new Consumo() { IdMesa = req.IdMesa, Cliente_Nome = req.Cliente_Nome, Qtd_Pessoas = req.Qtd_Pessoas};
            return repository.Save(c);
        }

        public RepositorioRetorno Query()
        {
            return repository.Query();
        }

        public Consumo GetById(long id)
        {
            RepositorioRetorno retorno = repository.GetById(id);
            if (retorno.Success)
            {
                return (Consumo)retorno.Result;
            }
            return null;
        }

        public RepositorioRetorno Delete(long id)
        {
            return repository.Delete(id);
        }

        public RepositorioRetorno Update(Consumo c)
        {
            return repository.Update(c);
        }
    }
}
