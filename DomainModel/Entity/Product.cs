namespace DomainModel.Entity
{
    using System;
    using System.Collections.Generic;

    public partial class Product : EntityBase
    {
        public long TitleTextId { get; set; }
        public long TextTextId { get; set; }
        public long SubtitleTextId { get; set; }
        public long DescriptionTextId { get; set; }
        public Nullable<long> ImageId { get; set; }
    
        public virtual Image Image { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual Text TitleText { get; set; }
        public virtual Text SubtitleText { get; set; }
        public virtual Text ShortDescriptionText { get; set; }
        public virtual Text DescriptionText { get; set; }
        public virtual ICollection<DocumentGroup> DocumentGroups { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Product> Similar { get; set; }
        public virtual ICollection<Product> SimilarTo { get; set; }
    }
}
