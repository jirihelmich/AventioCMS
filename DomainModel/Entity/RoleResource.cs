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
    
    public partial class RoleResource
    {
        public long id { get; set; }
        public long resourcesid { get; set; }
        public long rolesid { get; set; }
        public System.DateTime date { get; set; }
    
        public virtual Resource Resource { get; set; }
        public virtual Role Role { get; set; }
        public virtual RoleResource roles_resources1 { get; set; }
        public virtual RoleResource roles_resources2 { get; set; }
    }
}
