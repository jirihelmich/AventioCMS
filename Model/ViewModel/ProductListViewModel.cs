using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entity;

namespace Model.ViewModel
{
    public class ProductListViewModel
    {
        public ICollection<Product> Products { get; set; }
        public ICollection<Category> Categories { get; set; }
        public Category ActiveCategory { get; set; }
    }
}
