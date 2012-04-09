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
    public class ImageService : AbstractService<Image>
    {


        protected override DbSet<Image> GetItemSet(CMSEntities ctx)
        {
            return ctx.images;
        }

        public Object Upload(HttpPostedFileBase small, HttpRequestBase request, String name)
        {
            var postfix = small == null ? "" : small.FileName;
            var path = Path.Combine(request.MapPath("~/files"), postfix);

            if (small != null)
            {
                small.SaveAs(path);
            }
            

            System.Drawing.Image img = System.Drawing.Image.FromFile(path);

            DomainModel.Entity.Image i = new DomainModel.Entity.Image()
                          {
                              Path = postfix,
                              Height = img.Height,
                              Width = img.Width,
                              TitleText = new Text()
                          };

            CMSEntities ctx = _sl.GetDBContext();
            ctx.images.Add(i);
            ctx.SaveChanges();

            return new {id = i.Id, path = postfix, name = small.FileName};
        }
    }
}
