using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAPI.API.DTOs.Requests
{
    public class RegisterRequest
    {
        public string Nome {  get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
