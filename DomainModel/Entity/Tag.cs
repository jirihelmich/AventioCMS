namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tag : EntityBase
    {
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual ICollection<TagArticle> TagArticles { get; set; }
    }
}
