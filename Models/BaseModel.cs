using RestauranteAPI.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAPI.Models
{
    public abstract class BaseModel<T> where T : BaseModel<T>
    {
        public abstract List<Parametro> CriarParametros();
        public DbHelper db;

        public BaseModel()
        {
            db = new DbHelper();
        }

        public SaveModelResult Save()
        {
            var parametros = CriarParametros();
            string tableName = parametros[0].TableName.ToUpper();

            if (db.Exists(tableName, parametros))
            {
                db.Update(tableName, parametros);
            }
            else
            {
                SaveModelResult result = db.Insert(tableName, parametros);
                if (result.Success)
                {
                    Parametro parametroChave = parametros.First(p => p.Chave);
                    PropertyInfo atributoModel = GetType().GetProperty(parametroChave.Propriedade);

                    if (atributoModel != null)
                        atributoModel.SetValue(this, Convert.ChangeType(result.Id, atributoModel.PropertyType));

                    return new SaveModelResult() { Success = true };
                }
                else 
                {
                    return result;
                }
            }
            return new SaveModelResult() { Success = false };
        }

        public static List<T> Query()
        {
            return new List<T>();
        }
    }
}
