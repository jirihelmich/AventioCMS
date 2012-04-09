using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using DomainModel;
using DomainModel.Entity;

namespace Model.Subsystem
{
    public class DocumentService : AbstractService<DocumentGroup>
    {
        public Document Upload(HttpPostedFileBase file, string title, string culture, string rootedPath)
        {
            Document d = new Document();
            d.TitleText = new Text();
            d.TitleText.Values = new List<TextValue>();
            d.TitleText.Values.Add(new TextValue()
            {
                Culture = culture, SeoValue = MakeAlias(title), Value = title
            });

            d.Path = file.FileName;

            if (file.FileName != String.Empty)
            {
                file.SaveAs(Path.Combine(new string[] { rootedPath, file.FileName }));
            }

            return d;
        }

        protected override DbSet<DocumentGroup> GetItemSet(CMSEntities ctx)
        {
            return ctx.docgroups;
        }
    }
}
