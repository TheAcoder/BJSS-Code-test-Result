using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriceBasket.Repositories;
using PriceBasket.Entities;
namespace PriceBasket.Service
{
    /// <summary>
    /// Class for all things related to basket.
    /// I had a dilemma whether I will create a promotionservice for for all things related to promotion.
    /// But in the end I have decided to stick it all in basketservice.
    /// </summary>
    public class BasketService
    {

        #region private properties
        private const string NO_PROMO_MSG="(No offers available)";
        private IPriceRepository _priceRepository;
        private IPromotionRepository _promotionRepository;
        private double? _subTotal;
        private double? _total;
        private string _promoText;        
        private bool basketChanged = false;
        private List<Price> _lineItems = new List<Price>();
        private List<Promotion> _appliedPromotions=new List<Promotion>();
        #endregion

        #region public methods and constructor

        public BasketService(IPriceRepository priceRepository, IPromotionRepository promoRepository)
        {
            _priceRepository = priceRepository;
            _promotionRepository = promoRepository;
        }
        /// <summary>
        /// This is the method to call when users update the basket from console
        /// </summary>
        /// <param name="items"></param>
        public void SetBasket(List<string> items)
        {
            try
            {
                if (items==null)
                    throw new ArgumentNullException("Items");
                if (items.Count == 0)
                    throw new ArgumentOutOfRangeException("Items"," Basket item count must be atleast one");
                basketChanged = true;
              
                foreach (string item in items.Distinct())
                {
                    
                    var lineitem = _priceRepository.Get(item);                    
                    //items.Skip( lineitem.Quantity - 1);
                    if (null == lineitem)
                        throw new System.IO.InvalidDataException(string.Format("Item {0} does not exist",item));
                    //There are two ways I could have done this.
                    //One is add the quantity in the argument from the cmd prompt.
                    //Second is to keep cmd line arguments simpler, and resolve it later in here.
                    lineitem.Quantity = items.Count(i => i.Equals(item));
                    LineItems.Add(lineitem);
                }
            }
            catch
            {
                //what else is needed here?
                throw;
            }
        }
        /// <summary>
        /// Juice of the program.
        /// </summary>
        public void CalculatePromotions()
        {
            //set sub total and total first
            calculateSubTotal();
            
            var allPromos=_promotionRepository.GetAll();
            //filter by date range
            allPromos=allPromos.Where(p=>((p.ValidFrom<=DateTime.Now) && (DateTime.Now<=p.ValidTo))).ToList();
            if (allPromos == null)
            {
                _promoText= NO_PROMO_MSG;
                return;
            }
            StringBuilder promotext=new StringBuilder();
            foreach(Promotion reward in allPromos)
            {
                //get total discount 
                var promotionDiscount = 0.0;

                var buckets=reward.BucketList;
                //is everything that's in bucket also in item list
                bool isValidBucket = !buckets.Select(b=>b.ProductId).Except(_lineItems.Select(li=>li.ProductId)).Any();
                //valid bucket.. now update line item prices if counts are right
                if (isValidBucket)
                {
                    foreach (Bucket bucket in buckets)
                    {
                        
                        //is still valid bucket after counter match
                        if (isValidBucket)
                        {
                            var lineItem=_lineItems.FirstOrDefault(li => li.ProductId.Equals(bucket.ProductId));
                            //match the count. if count's not matched, promotion not valid.                            
                            if (bucket.Count <= lineItem.Quantity)
                            {
                                double absoluteDiscount = 0;

                                switch (bucket.Operator)
                                {
                                    case DiscountOperator.Fraction:
                                        //throw or ignore. I choose Throw. Data issue
                                        if (bucket.Discount > 1.0) throw new System.IO.InvalidDataException("Invalid promotion");
                                        absoluteDiscount = lineItem.LinePrice * bucket.Discount;
                                        break;
                                    case DiscountOperator.Absolute:                                        
                                        absoluteDiscount = bucket.Discount;
                                        if (absoluteDiscount > lineItem.LinePrice ) throw new System.IO.InvalidDataException("Invalid promotion");
                                        break;
                                }
                                _total = _total - absoluteDiscount;
                                promotionDiscount = +absoluteDiscount;
                            }
                            else
                                isValidBucket = false;

                        }
                    }
                    if(isValidBucket) promotext.Append(string.Format("{0}- {1} , ", reward.Description, promotionDiscount.ToString("c")));
                }
                

            }
            _promoText = promotext.Length > 0 ? promotext.ToString() : NO_PROMO_MSG;
            
            
        }

        #endregion

        #region public properties
        /// <summary>
        /// Encapsulation of Subtotal calculation in a public property
        /// </summary>
        public double SubTotal 
        { 
            get
            {
                if(!_subTotal.HasValue || basketChanged)                
                    calculateSubTotal();
                return _subTotal.Value;                
            }
        }

        /// <summary>
        /// Encapsulation of Promotion text building in a public property
        /// </summary>
        public string PromotionText 
        { 

            get 
            { 
                
                return _promoText; 
            } 
        }

        public double Total { get { return _total.HasValue?_total.Value:0; } }

        public List<Price> LineItems 
        { 
            get
            {
                return _lineItems;
            }
        
        }

        #endregion
     
        
        private void calculateSubTotal()
        {
            _subTotal = LineItems.Sum(l => (l.LinePrice* l.Quantity));
            _total = _subTotal;
            basketChanged = false;
        }
    }
}
