using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DomainModel.Entity;

namespace Model.Subsystem
{
    public class PageService : AbstractService<Page>
    {

        protected override System.Data.Entity.DbSet<Page> GetItemSet(DomainModel.CMSEntities ctx)
        {
            return ctx.pages;
        }

        public override Page Edit(long? id, FormCollection values)
        {
            Page p = base.Edit(id, values);

            foreach (String lang in ServiceLayer.Langs)
            {
                p.ContentText = SetTextValue(p.ContentText, lang, values["content[" + lang + "]"]);
                p.TitleText = SetTextValue(p.TitleText, lang, values["title[" + lang + "]"]);
            }

            _sl.GetDBContext().SaveChanges();

            return p;
        }

    }
}
