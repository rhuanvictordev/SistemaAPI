using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.API.DTOs.Response;
using RestauranteAPI.Data;
using SistemaAPI.API.DTOs.Requests;
using SistemaAPI.Models;

namespace SistemaAPI.API.Controllers
{
    public class GrupoUsuarioController : BaseController
    {


        [HttpPost]
        public ActionResult CadastrarGrupoUsuario([FromBody] CriarGrupoUsuarioDTO req)
        {
            RepositorioRetorno retorno = GrupoUsuarioService.Save(req);
            if (retorno.Success)
                return StatusCode(201, new APIResponseDTO { Status = 201, Data = retorno.Result, Message = "Grupo de usuário cadastrado com sucesso" });

            return BadRequest(retorno.Message);
        }




        [HttpGet]
        public ActionResult ListarGruposUsuarios()
        {
            RepositorioRetorno retorno = GrupoUsuarioService.Query();
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = retorno.Result });

            return BadRequest(retorno.Message);
        }




        [HttpGet("{id}")]
        public ActionResult ObterGrupoUsuario(long id)
        {
            GrupoUsuario g = GrupoUsuarioService.GetById(id);
            if (g == null)
                return NotFound("Grupo de usuário não encontrado");

            return Ok(new APIResponseDTO { Status = 200, Data = g });
        }




        [HttpDelete("{id}")]
        public ActionResult DeletarGrupoUsuario(long id)
        {
            RepositorioRetorno retorno = GrupoUsuarioService.Delete(id);
            if (retorno.Success)
                return Ok("Grupo de usuário deletado com sucesso");

            return BadRequest(retorno.Message);
        }




        [HttpPut("{id}")]
        public ActionResult EditarGrupoUsuario(long id, [FromBody] GrupoUsuario request)
        {
            GrupoUsuario g = GrupoUsuarioService.GetById(id);
            if (g == null)
                return NotFound(new APIResponseDTO { Message = "Grupo de usuário não encontrado" });

            g.Nome = request.Nome;
            g.Descricao = request.Descricao;
            g.Alterado = DateTime.Now;

            RepositorioRetorno retorno = GrupoUsuarioService.Update(g);
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = g, Message = "Grupo de usuário editado com sucesso" });

            return BadRequest(retorno.Message);
        }




        [HttpPatch("{id}")]
        public ActionResult AlterarGrupoUsuario(long id, [FromBody] GrupoUsuario request)
        {
            GrupoUsuario g = GrupoUsuarioService.GetById(id);
            if (g == null)
                return NotFound(new APIResponseDTO { Message = "Grupo de usuário não encontrado" });

            if (request.IdGrupo != null && request.IdGrupo != g.IdGrupo)
                return NotFound(new APIResponseDTO { Message = "Não é possível alterar o ID do grupo de usuário" });

            g.Nome = request.Nome == null ? g.Nome : request.Nome;
            g.Descricao = request.Descricao == null ? g.Descricao : request.Descricao;

            RepositorioRetorno retorno = GrupoUsuarioService.Update(g);
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = g, Message = "Grupo de usuário atualizado com sucesso" });

            return BadRequest(retorno.Message);
        }



    }
}
