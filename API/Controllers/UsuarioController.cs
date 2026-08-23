using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.API.DTOs.Requests;
using RestauranteAPI.Framework;
using RestauranteAPI.Models;

namespace RestauranteAPI.API.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : BaseController
    {
        
        [HttpGet]
        public List<Usuario> GetUsers()
        {
            var list = Usuario.Query();
            return list;
        }


        [HttpPost]
        public ActionResult RegisterUser([FromBody] RegisterRequest request)
        {
            Usuario usuario = new Usuario() { Nome = request.Nome, Email = request.Email, SenhaHash = request.Senha };
            SaveModelResult result = usuario.Save();

            if (result.Success)
                return Ok(usuario);

            return StatusCode(409, new SaveModelResultOutputDTO()
            {
                Status = 409,
                Success = false,
                Message = result.Message
            });
        }
    }
}
