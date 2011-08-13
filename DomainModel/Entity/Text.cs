using System.Linq;

namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;

    public partial class Text : EntityBase
    {
    
        public virtual ICollection<TextValue> Values { get; set; }

        public String GetValue(String Culture)
        {
            return Values.Single(x => x.Culture == Culture.Substring(0, 2)).Value;
        }

        public String GetSeoValue(String Culture)
        {
            return Values.Single(x => x.Culture == Culture.Substring(0, 2)).SeoValue;
        }
    }
}
