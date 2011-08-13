namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;

    public partial class Document : EntityBase
    {
        public string Path { get; set; }
        public Nullable<long> NameTextId { get; set; }
        public Nullable<long> DocumentGroupId { get; set; }
    
        public virtual DocumentGroup DocumentGroup { get; set; }
        public virtual Text TitleText { get; set; }
    }
}
