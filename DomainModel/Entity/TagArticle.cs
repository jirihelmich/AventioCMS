using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Entity
{
    public class TagArticle : EntityBase
    {
        public long ArticleId { get; set; }
        public long TagId { get; set; }

        public Article Article { get; set; }
        public Tag Tag { get; set; }
    }
}
