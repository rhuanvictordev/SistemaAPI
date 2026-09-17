using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAPI.API.DTOs.Response
{
    public class APIResponse
    {
        public int? Status { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }
    }
}
