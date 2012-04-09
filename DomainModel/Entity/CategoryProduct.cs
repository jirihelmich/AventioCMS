using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Entity
{
    public partial class CategoryProduct
    {
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        public int Ordering { get; set; }

        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}
