namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TagArticle
    {
        public long id { get; set; }
        public long tagsid { get; set; }
        public long articlesid { get; set; }
    
        public virtual Article articles { get; set; }
        public virtual Tag tags { get; set; }
    }
}
