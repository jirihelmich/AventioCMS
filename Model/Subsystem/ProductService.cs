using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DomainModel.Entity;
using Model.ViewModel;

namespace Model.Subsystem
{
    public class ProductService : AbstractService<Product>
    {
        public override Product Edit(long? id, FormCollection values)
        {
            Product p = base.Edit(id, values);

            try
            {
                long imageId;
                long.TryParse(values["imageId"], out imageId);

                p.Image = _sl.GetSubsystem<ImageService>().GetItemSet().SingleOrDefault(x => x.Id == imageId);
            }
            catch (System.ArgumentException)
            {
            }

            // array of longs
            var categoryIds = values["categoryIds"].Split(',').Select(x => long.Parse(x));

            p.ProductCategories = p.ProductCategories ?? new List<CategoryProduct>();

            // remove those which are no longer used
            foreach (CategoryProduct cp in p.ProductCategories.Where(x => !categoryIds.Contains(x.Category.Id)).ToArray())
            {
                p.ProductCategories.Remove(cp);
            }

            var newRelations = categoryIds.Where(cid => !p.ProductCategories.Select(x => x.Category.Id).Contains(cid));

            foreach (long cid in newRelations)
            {
                CategoryProduct pc = new CategoryProduct()
                    {
                    Product = p,
                    Category = _sl.GetSubsystem<CategoryService>().GetById(cid),
                    Ordering = p.ProductCategories.Count == 0 ? 1 : p.ProductCategories.Max(x => x.Ordering) + 1
                    };

                p.ProductCategories.Add(pc);
            }

            p.Similar = p.Similar ?? new List<Product>();

            if (!String.IsNullOrEmpty(values["ProductIds"]))
            {
                p.Similar.Clear();
                foreach (string value in values["ProductIds"].Split(','))
                {
                    long pid = long.Parse(value);
                    p.Similar.Add(_sl.GetSubsystem<ProductService>().GetById(pid));
                }
            }

            foreach (String lang in ServiceLayer.Langs)
            {
                p.ShortDescriptionText = SetTextValue(p.ShortDescriptionText, lang, values["shortDescription[" + lang + "]"]);
                p.DescriptionText = SetTextValue(p.DescriptionText, lang, values["description[" + lang + "]"]);
                p.SubtitleText = SetTextValue(p.SubtitleText, lang, values["subtitle[" + lang + "]"]);
                p.TitleText = SetTextValue(p.TitleText, lang, values["title[" + lang + "]"]);
            }

            _sl.GetDBContext().SaveChanges();

            return p;
        }

        internal ProductEditViewModel GetEditModel(long? id)
        {
            var list =
                _sl.GetSubsystem<CategoryService>()
                    .ToList()
                    .Select(x => new { Name = x.TitleText.GetValue("cs"), x.Id })
                    .ToList();

            Product edited = id == null ? new Product() : GetById((long)id);

            return new ProductEditViewModel()
            {
                Edited = edited,
                Categories = new MultiSelectList(
                         list,
                         "Id",
                         "Name",
                         id == null ? new[] { 0L } : edited.ProductCategories.Select(x => x.Category.Id).ToArray()
                    ),
                Products = new MultiSelectList(
                         _sl.GetSubsystem<ProductService>().ToList().Select(x => new { Name = x.TitleText.GetValue("cs"), x.Id })
                    .ToList(),
                         "Id",
                         "Name",
                         id == null ? new[] { 0L } : edited.Similar.Select(x => x.Id).ToArray()
                    )
            };
        }

        protected override System.Data.Entity.DbSet<Product> GetItemSet(DomainModel.CMSEntities ctx)
        {
            return ctx.products;
        }

        public DocumentsViewModel GetDocumentsModelById(long? id)
        {
            if (id == null || id == 0) return null;

            Product p = GetById((long) id);

            return new DocumentsViewModel()
                        {
                            DocumentGroups = p.DocumentGroups,
                            Product = p
                        };
        }
    }
}
