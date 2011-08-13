namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Setting
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
