using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DomainModel.Entity;

namespace Model.ViewModel
{
    public class ProductEditViewModel
    {
        public Product Edited { get; set; }
        public MultiSelectList Products { get; set; }
        public MultiSelectList Categories { get; set; }
    }
}
