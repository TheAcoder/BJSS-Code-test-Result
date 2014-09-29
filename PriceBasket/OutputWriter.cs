using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceBasket
{
    /// <summary>
    /// The cleanest way to verify console output I found is to inject a writer into 
    /// the method writting output. This will also leave the option open in future to 
    /// extend/change this to output in a different manner. eg File/Event
    /// </summary>
    public interface IOutputWriter
    {
        void WriteLine(string s);
        void ReadLine();
    }

    // Use this console writer for your live code
    public class ConsoleOutputWriter : IOutputWriter
    {
        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }



        public void ReadLine()
        {
            Console.ReadLine();
        }
    }
}
