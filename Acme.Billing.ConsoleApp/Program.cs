using System;

namespace Acme.Billing.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the path to the customer CSV file:");
            string csvPath = Console.ReadLine();
        }
    }
}
