using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.API.DTOs.Response;
using RestauranteAPI.Data;
using SistemaAPI.API.DTOs.Requests;
using SistemaAPI.API.Services;
using SistemaAPI.Models;

namespace SistemaAPI.API.Controllers
{
    public class EstoqueController : BaseController
    {


        [HttpPost]
        public ActionResult CadastrarEstoque([FromBody] CadastrarEstoqueDTO req)
        {
            RepositorioRetorno retorno = EstoqueService.Save(req);
            if (retorno.Success)
                return StatusCode(201, new APIResponseDTO { Status = 201, Data = retorno.Result, Message = "Estoque cadastrado com sucesso" });

            return BadRequest(retorno.Message);
        }




        [HttpGet]
        public ActionResult ListarEstoques()
        {
            RepositorioRetorno retorno = EstoqueService.Query();
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = retorno.Result });

            return BadRequest(retorno.Message);
        }




        [HttpGet("{id}")]
        public ActionResult ObterEstoque(long id)
        {
            Estoque e = EstoqueService.GetById(id);
            if (e == null)
                return NotFound("Estoque não encontrado");

            return Ok(new APIResponseDTO { Status = 200, Data = e });
        }




        [HttpDelete("{id}")]
        public ActionResult DeletarEstoque(long id)
        {
            RepositorioRetorno retorno = EstoqueService.Delete(id);
            if (retorno.Success)
                return Ok("Estoque deletado com sucesso");

            return BadRequest(retorno.Message);
        }




        [HttpPut("{id}")]
        public ActionResult EditarEstoque(long id, [FromBody] Estoque request)
        {
            Estoque e = EstoqueService.GetById(id);
            if (e == null)
                return NotFound(new APIResponseDTO { Message = "Estoque não encontrado" });

            e.Nome = request.Nome;
            e.Descricao = request.Descricao;
            e.Preco = request.Preco;
            e.EAN = request.EAN;
            e.Qtd = request.Qtd;
            e.Qtd_Min = request.Qtd_Min;
            e.IdFornecedor = request.IdFornecedor;
            e.Revenda = request.Revenda;
            e.Preco_Revenda = request.Preco_Revenda;
            e.Alterado = DateTime.Now;

            RepositorioRetorno retorno = EstoqueService.Update(e);
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = e, Message = "Estoque editado com sucesso" });

            return BadRequest(retorno.Message);
        }




        [HttpPatch("{id}")]
        public ActionResult AlterarEstoque(long id, [FromBody] Estoque request)
        {
            Estoque e = EstoqueService.GetById(id);
            if (e == null)
                return NotFound(new APIResponseDTO { Message = "Estoque não encontrado" });

            if (request.IdEstoque != null && request.IdEstoque != e.IdEstoque)
                return NotFound(new APIResponseDTO { Message = "Não é possível alterar o ID do estoque" });

            e.Nome = request.Nome == null ? e.Nome : request.Nome;
            e.Descricao = request.Descricao == null ? e.Descricao : request.Descricao;
            e.Preco = request.Preco == null ? e.Preco : request.Preco;
            e.EAN = request.EAN == null ? e.EAN : request.EAN;
            e.Qtd = request.Qtd == null ? e.Qtd : request.Qtd;
            e.Qtd_Min = request.Qtd_Min == null ? e.Qtd_Min : request.Qtd_Min;
            e.IdFornecedor = request.IdFornecedor == null ? e.IdFornecedor : request.IdFornecedor;
            e.Revenda = request.Revenda == null ? e.Revenda : request.Revenda;
            e.Preco_Revenda = request.Preco_Revenda == null ? e.Preco_Revenda : request.Preco_Revenda;

            RepositorioRetorno retorno = EstoqueService.Update(e);
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = e, Message = "Estoque atualizado com sucesso" });

            return BadRequest(retorno.Message);
        }



    }
}
