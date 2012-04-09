using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DomainModel.Entity;

namespace Model.ViewModel
{
    public class CategoryEditViewModel
    {
        public SelectList Categories { get; set; }
        public Category Edited { get; set; }
    }
}
