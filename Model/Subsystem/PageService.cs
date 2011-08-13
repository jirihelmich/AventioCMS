using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entity;

namespace Model.Subsystem
{
    public class PageService : AbstractService<Page>
    {
        public Page GetById(long id)
        {
            return _sl.GetDBContext().pages.Single(x => x.Id == id);
        }

        protected override System.Data.Entity.DbSet<Page> GetItemSet(DomainModel.CMSEntities ctx)
        {
            return ctx.pages;
        }

    }
}
