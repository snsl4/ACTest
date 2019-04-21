using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtcomCore
{
    public class Program
    {
        static void Main(string[] args)
        {
            WriteCust(Query.GetCustomer());

            WriteCust(Query.GetCustomer(3));

            Query.GetOrdDate();
            Query.Join();
            Query.LeftJoin();

            Console.ReadKey();
        }

        private static void WriteCust(List<TCustomers> q)
        {
            foreach (var c in q)
                Console.WriteLine("ID: {0}\tName: {1}", c.CustomerId, c.CustomerName);
            Console.WriteLine();
        }
    }
}
