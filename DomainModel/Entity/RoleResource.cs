namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;

    public partial class RoleResource : EntityBase
    {
        public System.DateTime Date { get; set; }
        public long RoleId { get; set; }
        public long ResourceId { get; set; }
    
        public virtual Resource Resource { get; set; }
        public virtual Role Role { get; set; }
    }
}
