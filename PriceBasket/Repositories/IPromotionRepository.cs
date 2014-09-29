using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceBasket.Entities;
namespace PriceBasket.Repositories
{
    public interface IPromotionRepository:IRepository<Promotion, int>
    {   
        
        //IEnumerable<Promotion> GetPromosByType(PromotionType type);
    }
}
