namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;

    public partial class Category : EntityBase
    {
        public System.DateTime Date { get; set; }
        public Nullable<long> ParentId { get; set; }
        public long DescriptionTextId { get; set; }
        public Nullable<long> CarouselTextId { get; set; }
        public long NameTextId { get; set; }
        public int Ordering { get; set; }
    
        public virtual ICollection<ArticleCategory> CategoryArticles { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        public virtual Category Parent { get; set; }
        public virtual Text TitleText { get; set; }
        public virtual Text ShortDescriptionText { get; set; }
        public virtual Text DescriptionText { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }

        public string[] Parents { get { return GetParentsAndSelf().ToArray(); } }

        public List<String> GetParentsAndSelf()
        {
            Category c = this;
            List<String> list = new List<String>();

            while (c != null)
            {
                list.Add(c.ToString());
                c = c.Parent;
            }

            list.Reverse();

            return list;
        }

        public new String ToString()
        {
            return Id + "-" + TitleText.GetSeoValue(System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
        }
    }
}
