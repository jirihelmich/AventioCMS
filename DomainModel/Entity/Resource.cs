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
    
    public partial class Resource
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual ICollection<RoleResource> RoleResources { get; set; }
    }
}
