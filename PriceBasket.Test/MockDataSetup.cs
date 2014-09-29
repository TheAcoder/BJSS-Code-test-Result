using System;
using System.Collections.Generic;
using PriceBasket.Entities;
using PriceBasket.Repositories;
using Moq;
namespace PriceBasket.Test
{
    public static class MockDataSetup
    {
        public static Mock<IPriceRepository> SetMockPrices()
        {
            var mockPrice = new Mock<IPriceRepository>();
            var appleLineItem = new Price(1, "Apples", UOM.Bag, 1.00);
            var milkLineItem = new Price(2, "Milk", UOM.Bottle, 1.30);
            var soupLineItem = new Price(3, "Soup", UOM.Tin, 0.65);
            var breadLineItem = new Price(4, "Bread", UOM.Loaf, 0.80);

            mockPrice.Setup(mp => mp.Get("Apples")).Returns(appleLineItem);
            mockPrice.Setup(mp => mp.Get("Milk")).Returns(milkLineItem);
            mockPrice.Setup(mp => mp.Get("Soup")).Returns(soupLineItem);

            mockPrice.Setup(mp => mp.Get("Bread")).Returns(breadLineItem);
            return mockPrice;
        }
        public static Mock<IPromotionRepository> SetMockPromotions()
        {
            var mockPromotion = new Mock<IPromotionRepository>();

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

            mockPromotion.Setup(mpro => mpro.GetAll()).Returns(new List<Promotion>() { applePromo, soupAndBreadPromo });
            return mockPromotion;
        }
        public static Mock<IPromotionRepository> SetMockInvalidPromotions()
        {
            var mockPromotion = new Mock<IPromotionRepository>();



            var milkPromo = new Promotion(3, "invalid discount on Milk", PromotionType.PercentDiscount);
            milkPromo.BucketList = new List<Bucket>() { new Bucket() { ProductId = 2, Discount = 1.1, Count = 1, Operator = DiscountOperator.Fraction } };
            milkPromo.ValidFrom = DateTime.Now;
            milkPromo.ValidTo = DateTime.Now.AddDays(7);



            mockPromotion.Setup(mpro => mpro.GetAll()).Returns(new List<Promotion>() { milkPromo });
            return mockPromotion;
        }

        public static Mock<IPromotionRepository> SetMockOutOfRangePromotions()
        {
            var mockPromotion = new Mock<IPromotionRepository>();



            var milkPromo = new Promotion(3, "Out of range date discount on Milk", PromotionType.PercentDiscount);
            milkPromo.BucketList = new List<Bucket>() { new Bucket() { ProductId = 2, Discount = 0.1, Count = 1, Operator = DiscountOperator.Fraction } };
            milkPromo.ValidFrom = DateTime.Now.AddDays(3);
            milkPromo.ValidTo = DateTime.Now.AddDays(7);



            mockPromotion.Setup(mpro => mpro.GetAll()).Returns(new List<Promotion>() { milkPromo });
            return mockPromotion;
        }
    }
}
