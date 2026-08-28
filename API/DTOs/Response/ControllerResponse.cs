using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAPI.API.DTOs.Response
{
    public class ControllerResponse
    {
        public int? StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Response { get; set; }
    }
}
