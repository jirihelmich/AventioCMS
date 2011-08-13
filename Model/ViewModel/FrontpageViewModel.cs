using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Entity;

namespace AventioCMS.Models.ViewModel
{
    public class FrontpageViewModel
    {
        public List<Category> Categories { get; set; }
        public List<News> News { get; set; }
    }
}