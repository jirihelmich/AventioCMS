using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entity;
using HTH8.Models.UrlModels;

namespace Model.ViewModel
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public CategoryUrlModel CategoryPath { get; set; }
    }
}
