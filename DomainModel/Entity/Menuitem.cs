namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class MenuItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<long> ArticleId { get; set; }
        public Nullable<long> CategoryId { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual Category Category { get; set; }
    }
}
