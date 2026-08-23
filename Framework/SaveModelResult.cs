using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAPI.Framework
{
    public class SaveModelResult
    {
        public bool Success { get; set; }
        public string? Id { get; set; }

        private string _message;
        public string Message
        {
            get { return this._message != "" ? this._message : "Mensagem não implementada"; }
            set { this._message = value; }
        }
    }
}
