using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Models;

namespace RestauranteAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public List<Usuario> getUsers()
        { 
            var list = new List<Usuario>();
            Usuario u = new Usuario() { Id = 1, Nome = "Rhuan Victor", SenhaHash = "12345678", Email = "rhuan@email.com" };
            list.Add(u);
            return list;
        }
    }
}
