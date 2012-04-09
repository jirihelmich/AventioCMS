using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DomainModel.Entity;
using HTH8.Models.UrlModels;
using Model.ViewModel;

namespace Model.Subsystem
{
    public class CategoryService : AbstractService<Category>
    {
        public override Category Edit(long? id, FormCollection values)
        {
            Category c = base.Edit(id, values);

            c.Date = DateTime.Now;

            try
            {
                long parentId;
                long.TryParse(values["ParentId"], out parentId);

                c.Parent = GetItemSet().SingleOrDefault(x => x.Id == parentId);
            }catch(System.ArgumentException)
            {
            }

            foreach (String lang in ServiceLayer.Langs)
            {
                c.ShortDescriptionText = SetTextValue(c.ShortDescriptionText, lang, values["shortDescription[" + lang + "]"]);
                c.DescriptionText = SetTextValue(c.DescriptionText, lang, values["description[" + lang + "]"]);
                c.TitleText = SetTextValue(c.TitleText, lang, values["title[" + lang + "]"]);
            }

            _sl.GetDBContext().SaveChanges();

            return c;
        }

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
            return GetItemSet().Where(x => x.Parent == null).OrderBy(x=>x.Ordering).ToList();
        }

        protected override System.Data.Entity.DbSet<Category> GetItemSet(DomainModel.CMSEntities ctx)
        {
            return ctx.categories;
        }

        public override List<Category> ToList()
        {
            return ToList<int>(x => x.Ordering);
        }

        internal ViewModel.CategoryEditViewModel GetEditModel(long? id)
        {
            Category edited = (id == null ? new Category() : GetById((long)id));

            long selected = 0;
            if (id != null && edited.Parent != null) { selected = edited.Parent.Id; }

            var list = ToList()
                              .Where(x => x.Id != edited.Id)
                              .Select(x => new { x.Id, Name = x.TitleText.GetValue("cs") })
                              .ToList();

            list.Add(new { Id = 0L, Name = "Žádný rodič" });

            return new CategoryEditViewModel()
            {
                Categories =
                    new SelectList(
                    list,
                    "Id",
                    "Name",
                    selected
                    ),
                Edited = edited
            };
        }
    }
}
