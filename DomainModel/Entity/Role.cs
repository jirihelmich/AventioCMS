namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;

    public partial class Role : EntityBase
    {
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<long> ParentId { get; set; }
    
        public virtual ICollection<RoleResource> RoleResources { get; set; }
        public virtual ICollection<Role> Children { get; set; }
        public virtual Role Parent { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
