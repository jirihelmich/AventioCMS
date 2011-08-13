namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;

    public partial class Comment : EntityBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string UserAlias { get; set; }
        public string Email { get; set; }
        public string IP { get; set; }
        public Nullable<long> UserId { get; set; }
        public long ArticleId { get; set; }
        public Nullable<long> ParentId { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual ICollection<Comment> Children { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual User User { get; set; }
    }
}
