using RestauranteAPI.Data;
using SistemaAPI.API.DTOs.Requests;
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

        public RepositorioRetorno Save(CriarFornecedorDTO req)
        {
            Fornecedor f = new Fornecedor() 
            { 
                Nome = req.Nome,
                Estado = req.Estado, 
                Cidade = req.Cidade,
                Bairro = req.Bairro, 
                CPF_CNPJ = req.CPF_CNPJ,
                Numero = req.Numero, 
                Telefone1 = req.Telefone1, 
                Telefone2 = req.Telefone2
            };
            
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
