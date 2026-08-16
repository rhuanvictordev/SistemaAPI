using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public List<string> getUsers()
        { 
            var list = new List<string>() {"rhuan","ciclano","beltrano"};
            return list;
        }
    }
}
