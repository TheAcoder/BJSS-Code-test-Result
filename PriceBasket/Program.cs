using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceBasket.Service;
using PriceBasket.Repositories;
namespace PriceBasket
{
    public static class Program
    {
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            try
            {
                if (args.Count().Equals(0))
                    throw new ArgumentNullException("args");
                var itemList = args.ToList();
                //TODO: resolve these three with unity
                WriteConsoleOutput(itemList, new ConsoleOutputWriter(),
                    new ConsoleAppPriceRepository(), new ConsoleAppPromotionRepository());
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error has occurred. " + ex.Message);
            }
        }
        /// <summary>
        /// Method taking in args, writer, priceRepository and PromotionRepository.
        /// Calculate Basket, set promotions and churn output to the writer
        /// </summary>
        /// <param name="args"></param>
        /// <param name="writer"></param>
        /// <param name="priceRep"></param>
        /// <param name="promoRep"></param>
        public static void WriteConsoleOutput(List<string> args, IOutputWriter writer, IPriceRepository priceRep, IPromotionRepository promoRep)
        {
            var output = new StringBuilder();
            BasketService service = new BasketService(priceRep,promoRep);
            service.SetBasket(args);
            service.CalculatePromotions();
            writer.WriteLine(string.Format("Subtotal: {0}", service.SubTotal.ToString("c")));
            writer.WriteLine(service.PromotionText);
            writer.WriteLine("Total: " + service.Total.ToString("c"));
            writer.ReadLine();

            
            

        }
    }
}
