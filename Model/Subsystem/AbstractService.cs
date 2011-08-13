using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Subsystem
{
    public class AbstractService
    {
        protected ServiceLayer _sl;

        public void SetServiceLayer(ServiceLayer sl)
        {
            _sl = sl;
        }

    }
}
