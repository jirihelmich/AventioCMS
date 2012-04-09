using System.Linq;

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

        private static String[] Langs = new string[] { "cs", "en", "de", "ru", "fr", "pl" };

        public string Culture
        {
            get
            {
                var culture = Langs
                    .Where(x => !TitleText.IsNullOrEmpty(x));

                if (culture.Count() == 0)
                {
                    return null;
                }

                return culture.First();
            }
        }

    }
}
