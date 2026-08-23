using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAPI.Framework
{
    public static class DbUtils
    {
        public static string MakeSqlInsert(string tableName, List<Parametro> parametros)
        {
            StringBuilder sb = new StringBuilder($"INSERT INTO {tableName.ToUpper()} (");

            foreach (var param in parametros)
                sb.Append(param.Nome.ToUpper() + ",");

            sb.Length--; sb.Append(") VALUES (");

            foreach (var param in parametros)
                sb.Append("?,");

            sb.Length--; sb.Append(")");
            return sb.ToString();
        }

        public static string MakeSqlUpdate(string tableName, List<Parametro> parametros)
        {
            StringBuilder sb = new StringBuilder($"UPDATE {tableName.ToUpper()} SET ");

            foreach (var param in parametros)
                if (!param.Chave)
                    sb.Append(param.Nome.ToUpper() + " = ?,");

            sb.Length--; sb.Append(" WHERE ");

            foreach (var param in parametros)
                if (param.Chave)
                {
                    sb.Append(param.Nome.ToUpper() + " = ?");
                    break;
                }

            return sb.ToString();
        }

        public static string MakeSqlDelete(string tableName, List<Parametro> parametros)
        {
            String sql = $"DELETE FROM {tableName.ToUpper()} WHERE ";

            foreach (var param in parametros)
                if (param.Chave)
                {
                    sql += $"{param.Nome.ToUpper()} = ?";
                    break;
                }

            return sql;
        }

        public static string MakeSqlExists(string tableName, List<Parametro> parametros)
        {
            string pkName = "";
            foreach (var param in parametros)
            { 
                if(param.Chave)
                    pkName  = param.Nome.ToUpper();
            }

            return $"SELECT * FROM {tableName.ToUpper()} WHERE {pkName} = ?";
        }
    }
}
