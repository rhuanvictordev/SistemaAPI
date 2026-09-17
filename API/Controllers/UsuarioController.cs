using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.API.DTOs.Requests;
using RestauranteAPI.API.DTOs.Response;
using RestauranteAPI.Data;
using RestauranteAPI.Models;
using SistemaAPI.API.Controllers;


public class UsuarioController : BaseController
{
    

    [HttpPost]
    public ActionResult CadastrarUsuario([FromBody] RegisterRequest request)
    {
        RepositorioRetorno retorno = Usuarioservice.Save(request);
        if (retorno.Success)
            return StatusCode(201, new APIResponse { Status = 201, Data = retorno.Result, Message = "Usuário cadastrado com sucesso" });
        
        return BadRequest(retorno.Message);
    }




    [HttpGet]
    public ActionResult ListarUsuarios()
    {
        RepositorioRetorno retorno = Usuarioservice.Query();
        if (retorno.Success)
            return Ok(new APIResponse { Status = 200, Data = retorno.Result });
        
        return BadRequest(retorno.Message);
    }




    [HttpGet("{id}")]
    public ActionResult ObterUsuario(long id)
    {
        Usuario u =  Usuarioservice.GetById(id);
        if (u == null)
            return NotFound("Usuário não encontrado");
        
        return Ok(new APIResponse { Status = 200, Data = u });
    }




    [HttpDelete("{id}")]
    public ActionResult DeletarUsuario(long id)
    {
        RepositorioRetorno retorno = Usuarioservice.Delete(id);
        if (retorno.Success)
            return Ok("Usuário deletado com sucesso");

        return BadRequest(retorno.Message);
    }




    [HttpPut("{id}")]
    public ActionResult EditarUsuario(long id, [FromBody] RegisterRequest request)
    {
        Usuario u = Usuarioservice.GetById(id);
        if (u == null)
            return NotFound(new APIResponse { Message = "Usuário não encontrado" });
        
        u.Nome = request.Nome;
        u.Email = request.Email;
        u.Senha = request.Senha;
        u.IdGrupo = request.IdGrupo;
        u.Alterado = DateTime.Now;

        RepositorioRetorno retorno = Usuarioservice.Update(u);
        if (retorno.Success)
            return Ok(new APIResponse { Status = 200, Data = u, Message = "Usuário editado com sucesso"});

        return BadRequest(retorno.Message);
    }




    [HttpPatch("{id}")]
    public ActionResult AlterarUsuario(long id, [FromBody] Usuario request)
    {
        Usuario u = Usuarioservice.GetById(id);
        if (u == null)
            return NotFound(new APIResponse { Message = "Usuário não encontrado" });

        if (request.IdUsuario != null && request.IdUsuario != u.IdUsuario)
            return NotFound(new APIResponse { Message = "Não é possível alterar o ID do usuário" });

        u.Nome = request.Nome == null ? u.Nome : request.Nome;
        u.Email = request.Email == null ? u.Email : request.Email;
        u.Senha = request.Senha == null ? u.Senha : request.Senha;
        u.IdGrupo = request.IdGrupo == null ? u.IdGrupo : request.IdGrupo;

        RepositorioRetorno retorno = Usuarioservice.Update(u);
        if (retorno.Success)
            return Ok(new APIResponse { Status = 200, Data = u, Message = "Usuário atualizado com sucesso" });

        return BadRequest(retorno.Message);
    }



}