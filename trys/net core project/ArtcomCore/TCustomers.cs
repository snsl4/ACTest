using System;
using System.Collections.Generic;

namespace ArtcomCore
{
    public partial class TCustomers
    {
        public TCustomers()
        {
            TOrder = new HashSet<TOrder>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public virtual ICollection<TOrder> TOrder { get; set; }
    }
}
