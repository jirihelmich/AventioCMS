namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;

    public partial class Page : EntityBase
    {
        public long TitleTextId { get; set; }
        public long TextTextId { get; set; }
    
        public virtual Text TitleText { get; set; }
        public virtual Text ContentText { get; set; }
    }
}
