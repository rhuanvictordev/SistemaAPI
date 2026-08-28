using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.API.Controllers;
using RestauranteAPI.API.DTOs.Requests;
using RestauranteAPI.API.DTOs.Response;
using RestauranteAPI.Models;
using RestauranteAPI.Repository;

[Route("api/[controller]")]
public class UsuarioController : BaseController
{
    UsuarioRepository rep;

    public UsuarioController()
    {
        rep = new UsuarioRepository();
    }

    [HttpPost]
    public ActionResult RegisterUser([FromBody] RegisterRequest request)
    {
        Usuario u = new Usuario()
        {
            Id = 0,
            Nome = request.Nome,
            Email = request.Email,
            Senha = request.Senha
        };

        RepositorioRetorno retorno = rep.Save(u);

        if (retorno.Success)
        {
            ControllerResponse response = new ControllerResponse();
            response.StatusCode = 200;
            response.Response = retorno.Result;

            return Ok(response);
        }

        return BadRequest(retorno.Message);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        return Ok(rep.Query());
    }

    [HttpGet("{id}")]
    public ActionResult Carregar(long id)
    {
        return Ok(rep.Load(id));
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(long id)
    {
        RepositorioRetorno retorno = rep.Delete(id);

        if (retorno.Success)
        {
            return Ok(retorno);
        }

        return BadRequest(retorno.Message);
    }

    [HttpPut("{id}")]
    public ActionResult Update(long id, [FromBody] RegisterRequest request)
    {
        Usuario u = new Usuario()
        {
            Id = id,
            Nome = request.Nome,
            Email = request.Email,
            Senha = request.Senha
        };

        RepositorioRetorno retorno = rep.Update(u);

        if (retorno.Success)
            return Ok(retorno);

        return BadRequest(retorno.Message);
    }

    [HttpPatch("{id}")]
    public ActionResult Patch(long id, [FromBody] Usuario request)
    {
        Usuario u = (Usuario) rep.Load(id).Result;

        if (u == null)
            return NotFound();

        if (request.Nome != null)
            u.Nome = request.Nome;

        if (request.Email != null)
            u.Email = request.Email;

        if (request.Senha != null)
            u.Senha = request.Senha;

        RepositorioRetorno retorno = rep.Update(u);

        if (retorno.Success)
            return Ok(retorno);

        return BadRequest(retorno.Message);
    }
}