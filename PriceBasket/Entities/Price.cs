using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceBasket.Entities
{
    /// <summary>
    /// product and price has 1-1 relation for now. So price and product information are clubbed together in one entity.
    /// If in future, something like different prices for different stores been introduced, then these two entities need to be separated.    
    /// </summary>
    public class Price
    {
        public Price(int productId, string description, UOM uom, double linePrice)
        {
            ProductId = productId;
            Description = description;
            UnitOfMeasurement = uom;
            LinePrice = linePrice;
           
        }

        public int ProductId { get; set; }
        public string Description { get; set; }
        public UOM UnitOfMeasurement { get; set; }
        public double LinePrice { get; set; }
        public int Quantity { get; set; }

    }

    public enum UOM
    {
        Tin,
        Loaf,
        Bottle,
        Bag
    }
}
