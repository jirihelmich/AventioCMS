namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class DocumentGroup
    {
        public long Id { get; set; }
    
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
