using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp1.INTERFACE
{
    public interface IRepository <TEntity> where TEntity : class
    {
        // Declaring Common Methods, will Be Accesible By all Table Classes
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
    }
}
