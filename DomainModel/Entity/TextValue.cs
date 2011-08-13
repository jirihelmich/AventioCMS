namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TextValue
    {
        public long Id { get; set; }
        public string Culture { get; set; }
        public string Value { get; set; }
        public string SeoValue { get; set; }
        public long TextId { get; set; }
    
        public virtual Text Text { get; set; }
    }
}
