using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entity;

namespace Model.ViewModel
{
    public class DocumentsViewModel
    {
        public ICollection<DocumentGroup> DocumentGroups { get; set; }
        public Product Product { get; set; }
    }
}
