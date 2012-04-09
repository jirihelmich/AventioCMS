using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DomainModel.Entity;

namespace Model.ViewModel
{
    public class NewsEditViewModel
    {
        public News Edited { get; set; }
        public SelectList Products { get; set; }
    }
}
