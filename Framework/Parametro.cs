using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAPI.Framework
{
    public class Parametro
    {
        public string TableName { get; set; }
        public bool Chave {  get; set; }
        public string Nome {  get; set; }
        public object? Valor { get; set; }
        public string Propriedade { get; set; }


        public Parametro(string tableName, bool chave, string nome, object? valor, string propriedade)
        {
            TableName = tableName;
            Chave = chave;
            Nome = nome; 
            Valor = valor;
            Propriedade = propriedade;
        }
    }

}
