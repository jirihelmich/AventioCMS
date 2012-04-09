using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DomainModel.Entity;
using AventioCMS.Models.ViewModel;
using HTH8.Models.UrlModels;
using Model.Subsystem;
using Model.ViewModel;

namespace Model
{
    public class ServiceLayer
    {

        #region Properties
        
        /// <summary>
        /// An instance of EF context which is used to retreive entities from the underlying
        /// data provider.
        /// </summary>
        private CMSEntities _context = new CMSEntities();

        /// <summary>
        /// Cache of service objects which is designed to create at most one instance of
        /// each service class.
        /// </summary>
        private Dictionary<Type, object> _serviceCache = new Dictionary<Type, object>();

        public static String[] Langs = new string[] { "cs", "en", "de", "ru", "fr", "pl" };

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Factory method for the services of the Model subsystem.
        /// 
        /// Hand in hand with the _serviceCache ADT, it creates a new instance
        /// of the required service if there is no such instance already created.
        /// </summary>
        /// <typeparam name="T">Type of the service, derived from the Subsystem.AbstractService class</typeparam>
        /// <returns></returns>
        public T GetSubsystem<T>() where T : IModelService, new()
        {
            
            // "singleton"
            if (!_serviceCache.ContainsKey(typeof(T)))
            {
                // consider double-locking to provide thread-safe functionality
                IModelService instance = new T();
                instance.SetServiceLayer(this);

                // insert new instance
                _serviceCache.Add(typeof(T), instance);
            }
            return (T) _serviceCache[typeof(T)];
        }

        /// <summary>
        /// Returns a list of news ordered by publish date, descending. The returned list contains at most @count news
        /// with the newest one on the first position of the list and the @count-th
        /// newest one on the @count-th position of the list
        /// </summary>
        /// <param name="count">Maximum number of the news returned</param>
        /// <returns>List of the news, ordered by date, descending.</returns>
        public List<News> GetTopNews(int count)
        {
            return GetSubsystem<NewsService>().GetTop(count);
        }

        /// <summary>
        /// Returns a list of topmost categories, which are those whose have no parent.
        /// </summary>
        /// <returns>List of topmost categories.</returns>
        public List<Category> GetRootCategories()
        {
            return GetSubsystem<CategoryService>().GetRootCategories();
        }

        /// <summary>
        /// Returns a list of all pages.
        /// </summary>
        /// <returns></returns>
        public List<Page> GetAllPages()
        {
            return GetSubsystem<PageService>().ToList();
        }

        /// <summary>
        /// Returns data used on frontpage. Wrapped in the FrontpageViewModel class
        /// </summary>
        /// <returns>Up to 4 news and all of the root categories.</returns>
        public FrontpageViewModel GetFrontpage()
        {
            return new FrontpageViewModel() {News = GetTopNews(4), Categories = GetRootCategories()};
        }

        public CategoryViewModel GetCategory(CategoryUrlModel model)
        {
            long id = long.Parse(model.Categories.Last().Split(new char[] { '-' }, 2).First());

            CategoryService cs = GetSubsystem<CategoryService>();
            Category entity = cs.GetById(id);

            return new CategoryViewModel()
                {
                    Category = entity,
                    CategoryPath = model
                };
        }

        #endregion Public Methods

        #region Internal

        /// <summary>
        /// Getter of the EF data Context.
        /// </summary>
        /// <returns></returns>
        internal CMSEntities GetDBContext()
        {
            return _context;
        }

        #endregion Internal

        public ProductViewModel GetProductById(ProductUrlModel model)
        {
            return new ProductViewModel() {
                CategoryPath = new CategoryUrlModel() { Categories = model.Categories },
                Product = (GetSubsystem<ProductService>().GetById(model.Id))
            };
        }

        public ProductListViewModel GetProductsList(long? categoryId)
        {
            Category c = categoryId == null
                              ? null
                              : GetSubsystem<CategoryService>()
                                    .GetById((long) categoryId);

            return new ProductListViewModel()
                       {
                           Categories = GetSubsystem<CategoryService>().ToList(),
                           Products = (c == null ? null : c
                                                .CategoryProducts
                                                .OrderBy(x=>x.Ordering)
                                                .Select(x=>x.Product)
                                                .ToList()),
                            ActiveCategory = c
                       };
        }

        public ICollection<Page> GetPagesList()
        {
            return GetSubsystem<PageService>().ToList();
        }

        public ICollection<News> GetNewsList()
        {
            return GetSubsystem<NewsService>().ToList();
        }

        public ICollection<Category> GetCategoriesList()
        {
            return GetSubsystem<CategoryService>().GetRootCategories();
        }

        public object SaveUploadedImage(HttpRequestBase Request)
        {
            return GetSubsystem<ImageService>().Upload(Request.Files[0], Request, Request.Files[0].FileName);
        }

        public CategoryEditViewModel GetCategoryEditModel(long? id)
        {
            CategoryService service = GetSubsystem<CategoryService>();
            return service.GetEditModel(id);
        }

        public NewsEditViewModel GetNewsEditModel(long? id)
        {
            NewsService service = GetSubsystem<NewsService>();
            return service.GetEditModel(id);
        }

        public ProductEditViewModel GetProductEditModel(long? id)
        {
            ProductService service = GetSubsystem<ProductService>();
            return service.GetEditModel(id);
        }

        public DocumentsViewModel GetDocumentsModelByProductId(long? id)
        {
            return GetSubsystem<ProductService>().GetDocumentsModelById(id);
        }

        public void SaveUploadedDocuments(HttpRequestBase Request, long productId, FormCollection values)
        {
            DocumentService service = GetSubsystem<DocumentService>();

            DocumentGroup dg = new DocumentGroup();
            dg.Documents = new List<Document>();

            GetSubsystem<ProductService>().GetById(productId).DocumentGroups.Add(dg);

            foreach (string lang in Langs)
            {
                string key = "file[" + lang + "]";
                if (Request.Files.AllKeys.Contains(key))
                {
                    var file = Request.Files[key];

                    dg.Documents.Add(service.Upload(file, values["title[" + lang + "]"], lang, Request.MapPath("~/files")));
                }
            }

            try
            {
                GetDBContext().SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        public void DeleteDocumentsGroup(long id)
        {
            DocumentService ds = GetSubsystem<DocumentService>();
            DocumentGroup dg = ds.GetById(id);

            foreach (Product p in dg.Products)
            {
                p.DocumentGroups.Remove(dg);
            }

            GetDBContext().SaveChanges();

        }

        public MapViewModel GetMapData()
        {
            return new MapViewModel()
                       {
                           Categories = GetSubsystem<CategoryService>().ToList(),
                           Pages = GetSubsystem<PageService>().ToList(),
                           News = GetSubsystem<NewsService>().ToList()
                       };
        }

        public SearchViewModel Search(string s)
        {
            return new SearchViewModel()
                       {
                           Products = GetSubsystem<ProductService>()
                                        .ToList()
                                        .Where(
                                            x=>x.TitleText
                                                .Values
                                                .Single(y=>y.Culture == System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName)
                                                .Value.ToLower().Contains(s.ToLower())
                                        ).ToList()
                       };
        }

        public void ReorderCategories(OrderingModel model)
        {
            CategoryService service = GetSubsystem<CategoryService>();

            int order = 0;

            foreach (long key in model.Parent.Keys)
            {
                Category c = service.GetById(key);
                c.Parent = (model.Parent[key] == null
                            ? null :
                            service.GetById((long)model.Parent[key])
                            );
                c.Ordering = ++order;
            }

            GetDBContext().SaveChanges();
        }

        public void ReorderProducts(long categoryId, ProductOrderModel model)
        {
            CategoryService service = GetSubsystem<CategoryService>();
            Category c = service.GetById(categoryId);

            int i = 0;
            foreach (long pId in model.Products)
            {
                c.CategoryProducts.Single(x => x.Product.Id == pId).Ordering = i++;
            }

            GetDBContext().SaveChanges();
        }
    }
}
