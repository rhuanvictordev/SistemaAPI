using RestauranteAPI.Data;
using RestauranteAPI.Models;

namespace RestauranteAPI.Framework
{
    public class DbHelper
    {
        private Database db;

        public DbHelper()
        {
            db = new Database();
        }

        #region test generic query()
        /*public List<T> Query<T>(string tableName) where T : new()
        {
            List<T> lista = new List<T>();

            Database db = new Database();
            var command = db.CreateCommand();

            string tName = T.TableName;

            command.CommandText = $"SELECT * FROM {tName.ToUpper()}";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    T objeto = new T();

                    foreach (PropertyInfo propriedade in typeof(T).GetProperties())
                    {
                        if (!HasColumn(reader, propriedade.Name))
                            continue;

                        object valor = reader[propriedade.Name];

                        if (valor == DBNull.Value)
                            continue;

                        propriedade.SetValue(
                            objeto,
                            Convert.ChangeType(valor, propriedade.PropertyType)
                        );
                    }

                    lista.Add(objeto);
                }
            }

            return lista;
        }*/
        #endregion


        public bool Save(List<Parametro> parametros)
        {
            return false;
        }

        public bool Exists(string tableName, List<Parametro> parametros)
        {
            var command = db.CreateCommand();
            command.CommandText = DbUtils.MakeSqlExists(tableName, parametros);

            foreach (var param in parametros)
                command.Parameters.AddWithValue("?", param.Valor);

            var reader = command.ExecuteReader();
            if (reader.Read())
                return true;

            return false;
        }
        
        
        
        public BaseModelReturnSave Insert(string tableName, List<Parametro> parametros)
        {
            var command = db.CreateCommand();
            command.CommandText = DbUtils.MakeSqlInsert(tableName, parametros);

            foreach (var param in parametros)
                command.Parameters.AddWithValue("?", param.Valor);

            if (command.ExecuteNonQuery() > 0)
                return new BaseModelReturnSave() { Id = command.LastInsertedId.ToString(), Success = true };

            return new BaseModelReturnSave() { Id = "", Success = false };
        }

        public bool Update(string tableName, List<Parametro> parameters)
        {
            var command = db.CreateCommand();
            command.CommandText = DbUtils.MakeSqlUpdate(tableName, parameters);

            foreach (var param in parameters)
                command.Parameters.AddWithValue("?", param.Valor);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(string tableName, List<Parametro> parametros)
        {
            var command = db.CreateCommand();
            command.CommandText = DbUtils.MakeSqlDelete(tableName, parametros);
            return command.ExecuteNonQuery() > 0;
        }
    }
}
