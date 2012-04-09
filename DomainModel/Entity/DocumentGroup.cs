using System.Linq;

namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;

    public partial class DocumentGroup : EntityBase
    {   
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public string GetNameByCulture(string culture)
        {
            var names = Documents.Where(x => x.Culture == culture);

            if (names.Count() == 0)
                names = Documents.Where(x => x.Culture == "en");

            if (names.Count() == 0)
                names = Documents;

            return names.First().TitleText.GetValue(names.First().Culture);
        }
    }
}
