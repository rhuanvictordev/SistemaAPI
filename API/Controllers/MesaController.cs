using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.API.DTOs.Response;
using RestauranteAPI.Data;
using SistemaAPI.API.DTOs.Requests;
using SistemaAPI.API.Services;
using SistemaAPI.Models;

namespace SistemaAPI.API.Controllers
{
    public class MesaController : BaseController
    {


        [HttpPost]
        public ActionResult CadastrarMesa([FromBody] CadastrarMesaDTO req)
        {
            RepositorioRetorno retorno = MesaService.Save(req);
            if (retorno.Success)
                return StatusCode(201, new APIResponseDTO { Status = 201, Data = retorno.Result, Message = "Mesa cadastrada com sucesso" });

            return BadRequest(retorno.Message);
        }




        [HttpGet]
        public ActionResult ListarMesas()
        {
            RepositorioRetorno retorno = MesaService.Query();
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = retorno.Result });

            return BadRequest(retorno.Message);
        }




        [HttpGet("{id}")]
        public ActionResult ObterMesa(long id)
        {
            Mesa m = MesaService.GetById(id);
            if (m == null)
                return NotFound("Mesa não encontrada");

            return Ok(new APIResponseDTO { Status = 200, Data = m });
        }




        [HttpDelete("{id}")]
        public ActionResult DeletarMesa(long id)
        {
            RepositorioRetorno retorno = MesaService.Delete(id);
            if (retorno.Success)
                return Ok("Mesa deletada com sucesso");

            return BadRequest(retorno.Message);
        }




        [HttpPut("{id}")]
        public ActionResult EditarMesa(long id, [FromBody] Mesa request)
        {
            Mesa m = MesaService.GetById(id);
            if (m == null)
                return NotFound(new APIResponseDTO { Message = "Mesa não encontrada" });

            m.Nome = request.Nome;
            m.Descricao = request.Descricao;
            m.Status = request.Status;
            m.IdConsumo = request.IdConsumo;
            m.Alterado = DateTime.Now;

            RepositorioRetorno retorno = MesaService.Update(m);
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = m, Message = "Mesa editada com sucesso" });

            return BadRequest(retorno.Message);
        }




        [HttpPatch("{id}")]
        public ActionResult AlterarMesa(long id, [FromBody] Mesa request)
        {
            Mesa m = MesaService.GetById(id);
            if (m == null)
                return NotFound(new APIResponseDTO { Message = "Mesa não encontrada" });

            if (request.IdMesa != null && request.IdMesa != m.IdMesa)
                return NotFound(new APIResponseDTO { Message = "Não é possível alterar o ID da mesa" });

            m.Nome = request.Nome == null ? m.Nome : request.Nome;
            m.Descricao = request.Descricao == null ? m.Descricao : request.Descricao;
            m.Status = request.Status == null ? m.Status : request.Status;
            m.IdConsumo = request.IdConsumo == null ? m.IdConsumo : request.IdConsumo;

            RepositorioRetorno retorno = MesaService.Update(m);
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = m, Message = "Mesa atualizada com sucesso" });

            return BadRequest(retorno.Message);
        }



    }
}
