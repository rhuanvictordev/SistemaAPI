using RestauranteAPI.Data;
using SistemaAPI.API.DTOs.Requests;
using SistemaAPI.API.Repositories;
using SistemaAPI.Models;

namespace SistemaAPI.API.Services
{
    public class EstoqueService
    {
        EstoqueRepository repository;

        public EstoqueService()
        {
            repository = new EstoqueRepository();
        }

        public RepositorioRetorno Save(CadastrarEstoqueDTO req)
        {
            Estoque e = new Estoque() { Nome = req.Nome, Descricao = req.Descricao, Preco = req.Preco, Qtd = req.Qtd, EAN = req.EAN, IdFornecedor = req.IdFornecedor, Revenda = req.Revenda, Preco_Revenda = req.PrecoRevenda, Qtd_Min = req.Qtd_Min };
            return repository.Save(e);
        }

        public RepositorioRetorno Query()
        {
            return repository.Query();
        }

        public Estoque GetById(long id)
        {
            RepositorioRetorno retorno = repository.GetById(id);
            if (retorno.Success)
            {
                return (Estoque)retorno.Result;
            }
            return null;
        }

        public RepositorioRetorno Delete(long id)
        {
            return repository.Delete(id);
        }

        public RepositorioRetorno Update(Estoque e)
        {
            return repository.Update(e);
        }
    }
}
