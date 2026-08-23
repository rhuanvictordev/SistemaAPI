using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAPI.Models.Interface
{
    public interface IModel
    {
        public void Save();
        public bool Load(long id);
        public bool Delete(long id);
        public bool Update(Object obj);
    }
}
