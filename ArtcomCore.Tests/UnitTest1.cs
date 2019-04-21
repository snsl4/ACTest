using System;
using Xunit;
using System.Collections.Generic;

namespace ArtcomCore.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var db = new ArtcomDBContext();

            var result = Query.GetCustomer();

            Assert.IsAssignableFrom<IList<TCustomers>>(result);
        }

        [Fact]
        public void Test2()
        {
            var db = new ArtcomDBContext();

            string exp = "Петров Владимир Иванович";
            string result = "";
            foreach (TCustomers lol in Query.GetCustomer(2))
                result = lol.CustomerName;

            Assert.Equal(exp, result);
        }
    }
}
