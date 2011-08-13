namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;

    public partial class Setting : EntityBase
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
