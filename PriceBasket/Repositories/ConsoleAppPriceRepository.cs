using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceBasket.Entities;
namespace PriceBasket.Repositories
{
    /// <summary>
    /// I didn't try to test it.
    /// I believe it's beyond the scope of this exercise.
    /// </summary>
    public class ConsoleAppPriceRepository:IPriceRepository
    {
        private List<Price> _prices;

        public ConsoleAppPriceRepository()
        {
            _prices= new List<Price>()
            {new Price(1, "Apples", UOM.Bag, 1.00),
            new Price(2, "Milk", UOM.Bottle, 1.30),
            new Price(3, "Soup", UOM.Tin, 0.65),
             new Price(4, "Bread", UOM.Loaf, 0.80)};
        }

        public Entities.Price Get(string identifier)
        {
           return  _prices.FirstOrDefault(p => p.Description.Equals(identifier));
        }

        public List<Entities.Price> GetAll()
        {
            return _prices;

        }

        public void Delete(Entities.Price entity)
        {
            throw new NotImplementedException();
        }
    }
}
