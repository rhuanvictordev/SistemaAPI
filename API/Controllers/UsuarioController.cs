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

        public UsuarioController()
        {
        }

        [HttpGet]
        public List<Usuario> GetUsers()
        {
            var list = Usuario.Query();
            return list;
        }

        [HttpPost]
        public Usuario RegisterUser([FromBody] RegisterRequest request)
        {
            Usuario usuario = new Usuario() { Nome = request.Nome, Email = request.Email, SenhaHash = request.Senha};
            usuario.Save();
            return usuario;

            return null;
        }
    }
}
