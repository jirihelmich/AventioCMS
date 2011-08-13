namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Introtext { get; set; }
        public string Fulltext { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime DatePublished { get; set; }
        public System.DateTime DatePullback { get; set; }
        public System.DateTime DateLastModified { get; set; }
        public long Hits { get; set; }
        public int ModificationsCount { get; set; }
        public int Published { get; set; }
        public long Level { get; set; }
    
        public virtual ICollection<ArticleAuthors> ArticleAuthor { get; set; }
        public virtual ICollection<ArticleCategory> ArticleCategory { get; set; }
        public virtual Role Roles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
        public virtual ICollection<TagArticle> ArticleTags { get; set; }
    }
}
