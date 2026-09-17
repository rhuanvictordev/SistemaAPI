using RestauranteAPI.Data;
using SistemaAPI.API.Repositories;
using SistemaAPI.Models;

namespace SistemaAPI.API.Services
{
    public class FornecedorService
    {
        FornecedorRepository repository;

        public FornecedorService()
        {
            repository = new FornecedorRepository();
        }

        public RepositorioRetorno Save(Fornecedor f)
        {
            return repository.Save(f);
        }

        public RepositorioRetorno Query()
        {
            return repository.Query();
        }

        public Fornecedor GetById(long id)
        {
            RepositorioRetorno retorno = repository.GetById(id);
            if (retorno.Success)
            {
                return (Fornecedor)retorno.Result;
            }
            return null;
        }

        public RepositorioRetorno Delete(long id)
        {
            return repository.Delete(id);
        }

        public RepositorioRetorno Update(Fornecedor f)
        {
            return repository.Update(f);
        }
    }
}
