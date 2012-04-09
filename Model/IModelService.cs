using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public interface IModelService
    {
        void SetServiceLayer(ServiceLayer sl);
        void DeleteById(long id);
    }
}
