//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Nullable<long> UserId { get; set; }
        public string UserAlias { get; set; }
        public Nullable<long> ParentId { get; set; }
        public string Email { get; set; }
        public string IP { get; set; }
        public long ArticleId { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual ICollection<Comment> Children { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual User User { get; set; }
    }
}
