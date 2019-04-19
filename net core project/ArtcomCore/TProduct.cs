using System;
using System.Collections.Generic;

namespace ArtcomCore
{
    public partial class TProduct
    {
        public TProduct()
        {
            TOrderProduct = new HashSet<TOrderProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }

        public virtual ICollection<TOrderProduct> TOrderProduct { get; set; }
    }
}
