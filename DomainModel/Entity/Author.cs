namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Author
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Description { get; set; }
        public System.DateTime Date { get; set; }
        public long usersid { get; set; }
    
        public virtual ICollection<ArticleAuthors> AuthorArticles { get; set; }
        public virtual User User { get; set; }
    }
}
