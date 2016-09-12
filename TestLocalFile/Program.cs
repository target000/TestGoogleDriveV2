using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLocalFile
{
    class Program
    {
        // This method is used to test working with local files
        static void Main(string[] args)
        {
            string fileName = @"‪C:\Users\xlu\Desktop\hello_drive_1.txt";

            if (File.Exists(fileName))
            {
                Console.WriteLine("Yes the file is there");
            } 
            else
            {
                Console.WriteLine("the file does NOT exists");
            }

            Console.ReadLine();

        }
    }
}
