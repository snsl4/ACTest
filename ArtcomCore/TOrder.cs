using System;
using System.Collections.Generic;

namespace ArtcomCore
{
    public partial class TOrder
    {
        public TOrder()
        {
            TOrderProduct = new HashSet<TOrderProduct>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }

        public virtual TCustomers Customer { get; set; }
        public virtual ICollection<TOrderProduct> TOrderProduct { get; set; }
    }
}
