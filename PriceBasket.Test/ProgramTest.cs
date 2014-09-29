using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceBasket.Entities;
using PriceBasket.Repositories;
using PriceBasket.Service;
using Moq;
using System.Collections.Generic;
using PriceBasket;
namespace PriceBasket.Test
{
    [TestClass]
    public class ProgramTest
    {


        [TestInitialize]
        public void TestInitialize()
        {

        }
        [TestMethod]
        public void Check_Console_output_Number_Of_Times()
        {
            var writer = new Mock<IOutputWriter>();
            Program.WriteConsoleOutput(new List<string>() { "Milk", "Apples" }, writer.Object,
                MockDataSetup.SetMockPrices().Object,MockDataSetup.SetMockPromotions().Object);
            writer.Verify(w=>w.WriteLine( It.IsAny<string>()),Times.Exactly(3));
            
        }

    }
}
