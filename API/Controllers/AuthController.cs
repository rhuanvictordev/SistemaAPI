using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.API.DTOs.Requests;
using RestauranteAPI.Data;
using RestauranteAPI.Models;
using SistemaAPI.API.Repositories;
using SistemaAPI.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAPI.API.Controllers
{
    [Route("api")]
    public class AuthController : ControllerBase
    {
        
        UsuarioRepository repo;

        public AuthController()
        {
            repo = new UsuarioRepository();
        }

        
        [HttpPost]
        public IActionResult Auth([FromBody] LoginRequest request)
        {
            RepositorioRetorno r = repo.Login(request.Email, request.Senha);
            if (r.Success)
            {
                Usuario u = (Usuario)r.Result;
                var token = TokenGenerator.GenerateToken(u);
                return Ok(token);
            }
            else
            {
                return BadRequest("Credenciais inválidas");
            }
        }
    }
}
