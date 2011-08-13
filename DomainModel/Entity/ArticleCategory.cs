namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class ArticleCategory
    {
        public long Id { get; set; }
        public System.DateTime Date { get; set; }
        public long ArticleId { get; set; }
        public long CategoryId { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual Category Category { get; set; }
    }
}
