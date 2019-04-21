using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtcomTest2
{
    class Program
    {
        static void Main(string[] args)
        {

            GetCust();
            GetOrdDate();
            Join();
            LeftJoin();

            Console.ReadLine();
        }

        private static void GetCust()
        {
            using (ArtcomDBEntities1 db = new ArtcomDBEntities1())
            {
                var query = from c in db.Customers
                            select c;

                foreach(var c in query)
                    Console.WriteLine("ID: {0}\tName: {1}", c.CustomerId, c.CustomerName);
                Console.WriteLine();
            }
        }

        private static void GetOrdDate()
        {
            using (ArtcomDBEntities1 db = new ArtcomDBEntities1())
            {
                var query = from o in db.Orders
                            join c in db.Customers on o.CustomerId equals c.CustomerId
                            where o.Date == DateTime.Today
                            select new
                            {
                                Oid = o.OrderId,
                                CustName = c.CustomerName,
                                Date = o.Date
                            };

                foreach (var o in query)
                    Console.WriteLine("OID: {0}\tCID: {1}\tDate: {2}", o.Oid, o.CustName, o.Date);
                Console.WriteLine();
            }
        }

        private static void Join()
        {
            using (ArtcomDBEntities1 db = new ArtcomDBEntities1())
            {

                var query = from c in db.Customers
                            join o in db.Orders on c.CustomerId equals o.CustomerId
                            join op in db.OrderProducts on o.OrderId equals op.OrderId
                            join p in db.Products on op.ProductId equals p.ProductId
                            select new
                            {
                                CusName = c.CustomerName,
                                ProdName = p.ProductName,
                                ProdCount = op.Count
                            };

                foreach (var q in query)
                {
                    Console.WriteLine("Customer: {0}\tProduct: {1}\tCount: {2}", q.CusName, q.ProdName, q.ProdCount);
                }
                Console.WriteLine();
            }
        }

        private static void LeftJoin()
        {
            using (ArtcomDBEntities1 db = new ArtcomDBEntities1())
            {

                var query = from c in db.Customers
                            join o in db.Orders on c.CustomerId equals o.CustomerId
                            into qo
                            from oJoined in qo.DefaultIfEmpty()

                            join op in db.OrderProducts on oJoined.OrderId equals op.OrderId
                            into qop
                            from opJoined in qop.DefaultIfEmpty()

                            join p in db.Products on opJoined.ProductId equals p.ProductId
                            into qp
                            from pJoined in qp.DefaultIfEmpty()

                            select new
                            {
                                CusName = c.CustomerName,
                                ProdName = pJoined.ProductName,
                                ProdCount = opJoined == null ? 0 : opJoined.Count,
                                Date = oJoined.Date.ToString()
                            };

                foreach (var q in query)
                {
                    Console.WriteLine("Customer: {0}\tProduct: {1}\tCount: {2}\tDate: {3}", q.CusName, q.ProdName, q.ProdCount, q.Date);
                }
                Console.WriteLine();
            }
        }
    }
}
