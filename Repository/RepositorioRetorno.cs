using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAPI.Repository
{
    public class RepositorioRetorno
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object? Result { get; set; }
    }
}
