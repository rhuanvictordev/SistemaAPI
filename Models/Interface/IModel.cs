using RestauranteAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAPI.Models.Interface
{
    public interface IModel<T>
    {
        T Save(Database database, T model);
        bool Load(Database database, long id);
        bool Delete(Database database, long id);
        bool Update(Database database, T model);
    }
}
