using System;

namespace TestLocalFile
{
    class TestReadLine
    {
        static int Main(string[] args)
        {
            // the below method will read the line
            var str = Console.ReadLine();
            // the below method will write what is read
            Console.WriteLine(str);


            Console.ReadKey();
            return 0;
        }
    }
}
