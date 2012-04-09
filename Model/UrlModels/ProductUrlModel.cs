using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTH8.Models.UrlModels
{
    public class ProductUrlModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string[] Categories { get; set; }
    }
}