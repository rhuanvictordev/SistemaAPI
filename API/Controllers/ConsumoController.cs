using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.API.DTOs.Response;
using RestauranteAPI.Data;
using SistemaAPI.API.DTOs.Requests;
using SistemaAPI.API.Services;
using SistemaAPI.Models;

namespace SistemaAPI.API.Controllers
{
    public class ConsumoController : BaseController
    {


        [HttpPost]
        public ActionResult CadastrarConsumo([FromBody] CadastrarConsumoDTO req)
        {
            RepositorioRetorno retorno = ConsumoService.Save(req);
            if (retorno.Success)
                return StatusCode(201, new APIResponseDTO { Status = 201, Data = retorno.Result, Message = "Consumo cadastrado com sucesso" });

            return BadRequest(retorno.Message);
        }




        [HttpGet]
        public ActionResult ListarConsumos()
        {
            RepositorioRetorno retorno = ConsumoService.Query();
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = retorno.Result });

            return BadRequest(retorno.Message);
        }




        [HttpGet("{id}")]
        public ActionResult ObterConsumo(long id)
        {
            Consumo c = ConsumoService.GetById(id);
            if (c == null)
                return NotFound("Consumo não encontrado");

            return Ok(new APIResponseDTO { Status = 200, Data = c });
        }




        [HttpDelete("{id}")]
        public ActionResult DeletarConsumo(long id)
        {
            RepositorioRetorno retorno = ConsumoService.Delete(id);
            if (retorno.Success)
                return Ok("Consumo deletado com sucesso");

            return BadRequest(retorno.Message);
        }




        [HttpPut("{id}")]
        public ActionResult EditarConsumo(long id, [FromBody] Consumo request)
        {
            Consumo c = ConsumoService.GetById(id);
            if (c == null)
                return NotFound(new APIResponseDTO { Message = "Consumo não encontrado" });

            c.IdMesa = request.IdMesa;
            c.Inicio_Atendimento = request.Inicio_Atendimento;
            c.Fim_Atendimento = request.Fim_Atendimento;
            c.Cliente_Nome = request.Cliente_Nome;
            c.Qtd_Pessoas = request.Qtd_Pessoas;

            RepositorioRetorno retorno = ConsumoService.Update(c);
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = c, Message = "Consumo editado com sucesso" });

            return BadRequest(retorno.Message);
        }




        [HttpPatch("{id}")]
        public ActionResult AlterarConsumo(long id, [FromBody] Consumo request)
        {
            Consumo c = ConsumoService.GetById(id);
            if (c == null)
                return NotFound(new APIResponseDTO { Message = "Consumo não encontrado" });

            if (request.IdConsumo != null && request.IdConsumo != c.IdConsumo)
                return NotFound(new APIResponseDTO { Message = "Não é possível alterar o ID do consumo" });

            c.IdMesa = request.IdMesa == null ? c.IdMesa : request.IdMesa;
            c.Inicio_Atendimento = request.Inicio_Atendimento;
            c.Fim_Atendimento = request.Fim_Atendimento;
            c.Cliente_Nome = request.Cliente_Nome;
            c.Qtd_Pessoas = request.Qtd_Pessoas;

            RepositorioRetorno retorno = ConsumoService.Update(c);
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = c, Message = "Consumo atualizado com sucesso" });

            return BadRequest(retorno.Message);
        }



    }
}
