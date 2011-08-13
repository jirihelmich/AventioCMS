namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Resource
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual ICollection<RoleResource> ResourceRoles { get; set; }
    }
}
