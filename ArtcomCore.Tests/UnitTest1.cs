using System;
using Xunit;
using System.Collections.Generic;

namespace ArtcomCore.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestGetCustomer_ListTCustomers()
        {
            var db = new ArtcomDBContext();

            var result = Query.GetCustomer();

            Assert.IsAssignableFrom<IList<TCustomers>>(result);
        }

        [Fact]
        public void TestGetCustomerId_Name()
        {
            var db = new ArtcomDBContext();

            string exp = "Петров Владимир Иванович";
            string result = "";
            foreach (TCustomers customer in Query.GetCustomer(2))
                result = customer.CustomerName;

            Assert.Equal(exp, result);
        }
    }
}
