using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entity;

namespace Model.ViewModel
{
    public class SearchViewModel
    {
        public ICollection<Product> Products { get; set; }
    }
}
