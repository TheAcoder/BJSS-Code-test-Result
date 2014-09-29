using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceBasket.Entities;
namespace PriceBasket.Repositories
{
    public interface IPriceRepository : IRepository<Price, string>
    {
        //Optional repository methods here. Scaffholding for future expansion
        //an example
        //IEnumerable<Price> GetPriceGreaterThan();
        
    }
}
