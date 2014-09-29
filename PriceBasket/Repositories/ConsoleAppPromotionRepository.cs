using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceBasket.Entities;
namespace PriceBasket.Repositories
{
    /// <summary>
    ///  I didn't try to test it.
    /// I believe it's beyond the scope of this exercise.
    /// </summary>
    public class ConsoleAppPromotionRepository:IPromotionRepository
    {
        private List<Promotion> _promotions;

        public ConsoleAppPromotionRepository()
        {

            var applePromo = new Promotion(1, "10% discount on apples", PromotionType.PercentDiscount);
            applePromo.BucketList = new List<Bucket>() { new Bucket() { ProductId = 1, Discount = 0.1, Count = 1, Operator = DiscountOperator.Fraction } };
            applePromo.ValidFrom = DateTime.Now;
            applePromo.ValidTo = DateTime.Now.AddDays(7);



            var soupAndBreadPromo = new Promotion(2, "Buy 2 tins of soup and get a loaf of bread for half price", PromotionType.Multibuy);
            soupAndBreadPromo.BucketList = new List<Bucket>() { 
                new Bucket() { ProductId = 3, Discount = 0, Count = 2, Operator = DiscountOperator.Fraction } ,
                new Bucket() { ProductId = 4, Discount = 0.5, Count = 1, Operator = DiscountOperator.Fraction } };
            soupAndBreadPromo.ValidFrom = DateTime.Now;
            soupAndBreadPromo.ValidTo = DateTime.Now.AddDays(7);
            _promotions = new List<Promotion>() {applePromo,soupAndBreadPromo};
            
        }

        public Entities.Promotion Get(int identifier)
        {
            throw new NotImplementedException();
        }

        public List<Entities.Promotion> GetAll()
        {
            return _promotions;
        }

        public void Delete(Entities.Promotion entity)
        {
            throw new NotImplementedException();
        }
    }
}
