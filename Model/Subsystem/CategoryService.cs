using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Subsystem
{
    public class CategoryService : AbstractService
    {
        public String GetTitleByIDCulture(String id, String culture)
        {
            long idLong = long.Parse(id);
            return GetTitleByIDCulture(idLong, culture);
        }

        public String GetTitleByIDCulture(long id, String culture)
        {
            return _sl.GetDBContext().categories.Single(x => x.Id == id).TitleText.GetValue(culture);
        }

    }
}
