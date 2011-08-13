using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entity;

namespace Model.Subsystem
{
    public class CategoryService : AbstractService<Category>
    {
        public String GetTitleByIDCulture(String id, String culture)
        {
            long idLong = long.Parse(id);
            return GetTitleByIDCulture(idLong, culture);
        }

        public String GetTitleByIDCulture(long id, String culture)
        {
            return GetItemSet().Single(x => x.Id == id).TitleText.GetValue(culture);
        }

        public List<Category> GetRootCategories()
        {
            return GetItemSet().Where(x => x.Parent == null).ToList();
        }

        protected override System.Data.Entity.DbSet<Category> GetItemSet(DomainModel.CMSEntities ctx)
        {
            return ctx.categories;
        }

    }
}
