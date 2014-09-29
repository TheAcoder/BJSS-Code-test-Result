using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceBasket.Entities
{
    public class Promotion
    {
        
        public Promotion(int id, string description, PromotionType type)
        {
            Id = id;
            Description = description;
            RewardType = type;            
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public PromotionType RewardType { get; set; }
        public List<Bucket> BucketList { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
    //Buckets are related products grouped in for a promotion
    //This will satisfy following promotions that I can think of
    //Buy 1 get 1 free.
    //1/3 off. 
    //Buy 2 get something else half price.
    //x% off
    public class Bucket
    {
        public int ProductId { get; set; }
        //relative discount on lineprice
        public int Count { get; set; }
        public double Discount { get; set; }
        public DiscountOperator Operator { get; set; }
       
    }

    public enum PromotionType
    {
        CashDiscount,
        Multibuy,
        GroupSave,
        PercentDiscount
    }
    public enum DiscountOperator
    {
        Fraction,
        Absolute
    }
}
