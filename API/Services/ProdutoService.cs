using RestauranteAPI.Data;
using SistemaAPI.API.DTOs.Requests;
using SistemaAPI.API.Repositories;
using SistemaAPI.Models;

namespace SistemaAPI.API.Services
{
    public class ProdutoService
    {
        ProdutoRepository repository;

        public ProdutoService()
        {
            repository = new ProdutoRepository();
        }

        public RepositorioRetorno Save(CadastrarProdutoDTO req)
        {
            Produto p = new Produto() { Nome = req.Nome, Descricao = req.Descricao, Preco = req.Preco, Peso_KG = req.Peso_KG, Imagem = req.Imagem };
            return repository.Save(p);
        }

        public RepositorioRetorno Query()
        {
            return repository.Query();
        }

        public Produto GetById(long id)
        {
            RepositorioRetorno retorno = repository.GetById(id);
            if (retorno.Success)
            {
                return (Produto)retorno.Result;
            }
            return null;
        }

        public RepositorioRetorno Delete(long id)
        {
            return repository.Delete(id);
        }

        public RepositorioRetorno Update(Produto p)
        {
            return repository.Update(p);
        }
    }
}
