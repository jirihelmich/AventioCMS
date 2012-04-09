using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DomainModel.Entity;
using Model.ViewModel;

namespace Model.Subsystem
{
    public class NewsService : AbstractService<News>
    {
        public override News Edit(long? id, FormCollection values)
        {
            News n = base.Edit(id, values);

            if (values.AllKeys.Contains("date") && !String.IsNullOrEmpty(values["date"]))
            {
                n.Date = DateTime.Parse(values["date"]);
            }
            else
            {
                n.Date = DateTime.Now;
            }

            try
            {
                long imageId;
                long.TryParse(values["imageId"], out imageId);

                n.Image = _sl.GetSubsystem<ImageService>().GetItemSet().SingleOrDefault(x => x.Id == imageId);
            }
            catch (System.ArgumentException)
            {
            }

            try
            {
                long productId;
                long.TryParse(values["productId"], out productId);

                n.Product = _sl.GetSubsystem<ProductService>().GetItemSet().SingleOrDefault(x => x.Id == productId);
            }
            catch (System.ArgumentException)
            {
                n.Product = null;
            }

            foreach (String lang in ServiceLayer.Langs)
            {
                n.ShortDescriptionText = SetTextValue(n.ShortDescriptionText, lang, values["shortDescription[" + lang + "]"]);
                n.DescriptionText = SetTextValue(n.DescriptionText, lang, values["description[" + lang + "]"]);
                n.TitleText = SetTextValue(n.TitleText, lang, values["title[" + lang + "]"]);
            }

            _sl.GetDBContext().SaveChanges();

            return n;
        }

        public override List<News> ToList()
        {
            return ToListDescending<DateTime?>(x => x.Date);
        }

        public List<News> GetTop(int count)
        {
            return _sl.GetDBContext().News.OrderByDescending(x => x.Date).Take(count).ToList();
        }

        protected override System.Data.Entity.DbSet<News> GetItemSet(DomainModel.CMSEntities ctx)
        {
            return ctx.News;
        }

        internal NewsEditViewModel GetEditModel(long? id)
        {
            var list =
                _sl.GetSubsystem<ProductService>()
                    .ToList()
                    .Select(x => new {Name = x.TitleText.GetValue("cs"), x.Id})
                    .ToList();
            list.Add(new {Name = "Žádný produkt", Id = 0L});

            News edited = id == null ? new News() : GetById((long) id);

            return new NewsEditViewModel()
                       {
                           Edited = edited,
                           Products = new SelectList(
                                    list,
                                    "Id",
                                    "Name",
                                    id == null ? 0L : edited.ProductId
                               )
                       };
        }
    }
}
