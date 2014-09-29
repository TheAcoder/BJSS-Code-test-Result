using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceBasket.Entities;
using PriceBasket.Repositories;
using PriceBasket.Service;
using Moq;
using System.Collections.Generic;
namespace PriceBasket.Test
{
    [TestClass]
    public class BasketServiceTest
    {
        
        BasketService _basketService;
        [TestInitialize]
        public void TestInitialize()
        {
            var mockPrice = MockDataSetup.SetMockPrices();
            var mockPromotion = MockDataSetup.SetMockPromotions();
            _basketService = new BasketService(mockPrice.Object, mockPromotion.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Set_Basket_with_Null()
        {
            _basketService.SetBasket(null);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Set_Basket_with_No_items()
        {
            _basketService.SetBasket(new List<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(System.IO.InvalidDataException))]
        public void Set_Basket_with_Invalid_Item()
        {
            _basketService.SetBasket(new List<string>(){"Potato"});
        }
        [TestMethod]
        public void Set_Basket_with_Check_Items_Count()
        {
            //act
            _basketService.SetBasket(new List<string>() { "Apples","Milk" });
            //assert
            Assert.AreEqual( 2,_basketService.LineItems.Count);
        }

        [TestMethod]
        public void Check_Basket_SubTotal()
        {
            //act
            _basketService.SetBasket(new List<string>() { "Apples", "Milk" });
            //assert
            Assert.AreEqual( 2.30,_basketService.SubTotal);
        }

        [TestMethod]
        public void Check_Basket_No_Promotion_Message()
        {
            //act
            _basketService.SetBasket(new List<string>() {  "Milk" });
            _basketService.CalculatePromotions();
            //assert
            Assert.AreEqual( "(No offers available)",_basketService.PromotionText);
        }

       
        [TestMethod]
        public void Check_Basket_Total_For_Simple_PriceCut_Promotion()
        {
            //act
            _basketService.SetBasket(new List<string>() { "Apples", "Milk" });
            _basketService.CalculatePromotions();
            //assert
            Assert.AreEqual(2.20, double.Parse( _basketService.Total.ToString("#.##")));
        }

        [TestMethod]
        public void Check_Basket_Valid_Promotion_Message()
        {
            //act
            _basketService.SetBasket(new List<string>() { "Apples", "Milk" });
            _basketService.CalculatePromotions();
            //assert
            Assert.IsTrue(_basketService.PromotionText.Contains("10% discount on apples"));
        }

        [TestMethod]
        public void Check_Basket_Total_For_GroupSave_Promotion()
        {
            //act
            _basketService.SetBasket(new List<string>() { "Apples", "Milk", "Soup", "Bread", "Soup" });
            _basketService.CalculatePromotions();
            //assert
            Assert.AreEqual(3.90, double.Parse(_basketService.Total.ToString("#.##")));
        }

        [TestMethod]
        public void Check_Basket_Total_For_Missed_GroupSave_Promotion_On_Quantity()
        {
            //act
            _basketService.SetBasket(new List<string>() { "Apples", "Milk", "Soup", "Bread" });
            _basketService.CalculatePromotions();
            //assert
            Assert.AreEqual(3.65, double.Parse(_basketService.Total.ToString("#.##")));
        }

        [TestMethod]
        [ExpectedException(typeof(System.IO.InvalidDataException))]
        public void Check_Basket_Invalid_Promotion()
        {
            //arrange
            _basketService = new BasketService(MockDataSetup.SetMockPrices().Object, MockDataSetup.SetMockInvalidPromotions().Object);
            //act
            _basketService.SetBasket(new List<string>() {  "Milk" });
            _basketService.CalculatePromotions();
            
        }

        [TestMethod]        
        public void Out_of_Date_Range_Promotion_Ignored()
        {
            //arrange
            _basketService = new BasketService(MockDataSetup.SetMockPrices().Object, MockDataSetup.SetMockOutOfRangePromotions().Object);
            //act
            _basketService.SetBasket(new List<string>() { "Milk" });
            _basketService.CalculatePromotions();
            Assert.AreEqual(1.30, _basketService.Total);

        }

        
    }
}
