using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLocalFile
{
    class TestGetSet
    {
        public string Name { get; set; }



        static void Main(string[] args)
        {
            Console.WriteLine("Test c# get set ... ");
            Console.WriteLine();

            TestGetSet t1 = new TestGetSet();
            t1.Name = "Alex";

            TestGetSet t2 = new TestGetSet();
            t2.Name = "Bob";

            TestGetSet t3 = new TestGetSet();
            t3.Name = "Charie";

            TestGetSet t4 = new TestGetSet();
            t4.Name = "Dav";

            TestGetSet t5 = new TestGetSet();
            t5.Name = "Ellon";

            Console.WriteLine(t1.Name);
            Console.WriteLine(t2.Name);
            Console.WriteLine(t3.Name);
            Console.WriteLine(t4.Name);
            Console.WriteLine(t5.Name);

            
            Console.Read();
        }

    }
}
