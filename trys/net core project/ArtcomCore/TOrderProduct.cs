using System;
using System.Collections.Generic;

namespace ArtcomCore
{
    public partial class TOrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        public virtual TOrder Order { get; set; }
        public virtual TProduct Product { get; set; }
    }
}
