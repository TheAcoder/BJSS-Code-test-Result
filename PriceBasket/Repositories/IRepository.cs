using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceBasket.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity Get(TKey identifier);
        List<TEntity> GetAll();        
        void Delete(TEntity entity);
    }
}
