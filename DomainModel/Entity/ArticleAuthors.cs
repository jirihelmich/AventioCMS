namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class ArticleAuthors
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public long AuthorId { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual Author Author { get; set; }
    }
}
