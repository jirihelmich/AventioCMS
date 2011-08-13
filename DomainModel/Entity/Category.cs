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
    
        public virtual ICollection<ArticleCategory> CategoryArticles { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        public virtual Category Parent { get; set; }
        public virtual Text TitleText { get; set; }
        public virtual Text ShortDescriptionText { get; set; }
        public virtual Text DescriptionText { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
