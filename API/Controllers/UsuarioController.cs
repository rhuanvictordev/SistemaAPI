using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.API.DTOs.Requests;
using RestauranteAPI.API.DTOs.Response;
using RestauranteAPI.Data;
using RestauranteAPI.Models;
using RestauranteAPI.Repository;
using RestauranteAPI.Service;
using SistemaAPI.API.Controllers;
using SistemaAPI.Services.Auth;

public class UsuarioController : BaseController
{
    UsuarioService service;

    public UsuarioController()
    {
        service = new UsuarioService();
    }

    [HttpPost]
    public ActionResult RegisterUser([FromBody] RegisterRequest request)
    {
        RepositorioRetorno retorno = service.Save(request);
        if (retorno.Success)
        {
            ControllerResponse response = new ControllerResponse() { Status = 200, Data = retorno.Result };
            return Ok(response);
        }

        return BadRequest(retorno.Message);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        return Ok(service.Query());
    }

    [HttpGet("{id}")]
    public ActionResult Carregar(long id)
    {
        return Ok(service.GetById(id));
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(long id)
    {
        RepositorioRetorno retorno = service.Delete(id);

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

        RepositorioRetorno retorno = service.Update(u);

        if (retorno.Success)
            return Ok(retorno);

        return BadRequest(retorno.Message);
    }

    [HttpPatch("{id}")]
    public ActionResult Patch(long id, [FromBody] Usuario request)
    {
        Usuario u = (Usuario) service.GetById(id).Result;

        if (u == null)
            return NotFound();

        if (request.Nome != null)
            u.Nome = request.Nome;

        if (request.Email != null)
            u.Email = request.Email;

        if (request.Senha != null)
            u.Senha = request.Senha;

        RepositorioRetorno retorno = service.Update(u);

        if (retorno.Success)
            return Ok(retorno);

        return BadRequest(retorno.Message);
    }
}