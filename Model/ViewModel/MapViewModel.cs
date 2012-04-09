using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entity;

namespace Model.ViewModel
{
    public class MapViewModel
    {
        public ICollection<Category> Categories { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<Page> Pages { get; set; }
    }
}
