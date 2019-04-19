using System;
using System.Linq;

namespace ArtcomCore
{
    class Program
    {
        static void Main(string[] args)
        {
            GetCust();
            GetOrdDate();
            Join();
            LeftJoin();

            Console.ReadKey();
        }

        private static void GetCust()
        {
            using (ArtcomDBContext db= new ArtcomDBContext())
            {
                var query = db.TCustomers.ToList();

                foreach (var c in query)
                    Console.WriteLine("ID: {0}\tName: {1}", c.CustomerId, c.CustomerName);
                Console.WriteLine();
            }
        }

        private static void GetOrdDate()
        {
            using (ArtcomDBContext db = new ArtcomDBContext())
            {
                var query = from o in db.TOrder
                            join c in db.TCustomers on o.CustomerId equals c.CustomerId
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
            using (ArtcomDBContext db = new ArtcomDBContext())
            {

                var query = from c in db.TCustomers
                            join o in db.TOrder on c.CustomerId equals o.CustomerId
                            join op in db.TOrderProduct on o.OrderId equals op.OrderId
                            join p in db.TProduct on op.ProductId equals p.ProductId
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
            using (ArtcomDBContext db = new ArtcomDBContext())
            {

                var query = from c in db.TCustomers
                            join o in db.TOrder on c.CustomerId equals o.CustomerId
                            into qo
                            from oJoined in qo.DefaultIfEmpty()

                            join op in db.TOrderProduct on oJoined.OrderId equals op.OrderId
                            into qop
                            from opJoined in qop.DefaultIfEmpty()

                            join p in db.TProduct on opJoined.ProductId equals p.ProductId
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
