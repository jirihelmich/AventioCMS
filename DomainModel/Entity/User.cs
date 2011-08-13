namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public System.DateTime Date { get; set; }
        public long RoleId { get; set; }
    
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Role Role { get; set; }
    }
}
