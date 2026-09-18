using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.API.DTOs.Response;
using RestauranteAPI.Data;
using SistemaAPI.API.DTOs.Requests;
using SistemaAPI.API.Services;
using SistemaAPI.Models;

namespace SistemaAPI.API.Controllers
{
    public class ProdutoController : BaseController
    {


        [HttpPost]
        public ActionResult CadastrarProduto([FromBody] CadastrarProdutoDTO req)
        {
            RepositorioRetorno retorno = ProdutoService.Save(req);
            if (retorno.Success)
                return StatusCode(201, new APIResponseDTO { Status = 201, Data = retorno.Result, Message = "Produto cadastrado com sucesso" });

            return BadRequest(retorno.Message);
        }




        [HttpGet]
        public ActionResult ListarProdutos()
        {
            RepositorioRetorno retorno = ProdutoService.Query();
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = retorno.Result });

            return BadRequest(retorno.Message);
        }




        [HttpGet("{id}")]
        public ActionResult ObterProduto(long id)
        {
            Produto p = ProdutoService.GetById(id);
            if (p == null)
                return NotFound("Produto não encontrado");

            return Ok(new APIResponseDTO { Status = 200, Data = p });
        }




        [HttpDelete("{id}")]
        public ActionResult DeletarProduto(long id)
        {
            RepositorioRetorno retorno = ProdutoService.Delete(id);
            if (retorno.Success)
                return Ok("Produto deletado com sucesso");

            return BadRequest(retorno.Message);
        }




        [HttpPut("{id}")]
        public ActionResult EditarProduto(long id, [FromBody] Produto request)
        {
            Produto p = ProdutoService.GetById(id);
            if (p == null)
                return NotFound(new APIResponseDTO { Message = "Produto não encontrado" });

            p.Nome = request.Nome;
            p.Descricao = request.Descricao;
            p.Preco = request.Preco;
            p.Peso_KG = request.Peso_KG;
            p.Imagem = request.Imagem;
            p.Alterado = DateTime.Now;

            RepositorioRetorno retorno = ProdutoService.Update(p);
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = p, Message = "Produto editado com sucesso" });

            return BadRequest(retorno.Message);
        }




        [HttpPatch("{id}")]
        public ActionResult AlterarProduto(long id, [FromBody] Produto request)
        {
            Produto p = ProdutoService.GetById(id);
            if (p == null)
                return NotFound(new APIResponseDTO { Message = "Produto não encontrado" });

            if (request.IdProduto != null && request.IdProduto != p.IdProduto)
                return NotFound(new APIResponseDTO { Message = "Não é possível alterar o ID do produto" });

            p.Nome = request.Nome == null ? p.Nome : request.Nome;
            p.Descricao = request.Descricao == null ? p.Descricao : request.Descricao;
            p.Preco = request.Preco == null ? p.Preco : request.Preco;
            p.Peso_KG = request.Peso_KG == null ? p.Peso_KG : request.Peso_KG;
            p.Imagem = request.Imagem == null ? p.Imagem : request.Imagem;

            RepositorioRetorno retorno = ProdutoService.Update(p);
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = p, Message = "Produto atualizado com sucesso" });

            return BadRequest(retorno.Message);
        }



    }
}
