using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel;
using DomainModel.Entity;
using AventioCMS.Models.ViewModel;

namespace Model
{
    public class ServiceLayer
    {
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

        /// <summary>
        /// Factory method for the services of the Model subsystem.
        /// 
        /// Hand in hand with the _serviceCache ADT, it creates a new instance
        /// of the required service if there is no such instance already created.
        /// </summary>
        /// <typeparam name="T">Type of the service, derived from the Subsystem.AbstractService class</typeparam>
        /// <returns></returns>
        public T GetSubsystem<T>() where T : Subsystem.AbstractService, new()
        {
            // "singleton"
            if (!_serviceCache.ContainsKey(typeof(T)))
            {
                // consider double-locking to provide thread-safe functionality
                Subsystem.AbstractService instance = new T();
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
            return _context.News.OrderByDescending(x => x.Date).Take(count).ToList();
        }

        /// <summary>
        /// Returns a list of topmost categories, which are those whose have no parent.
        /// </summary>
        /// <returns>List of topmost categories.</returns>
        public List<Category> GetRootCategories()
        {
            return _context.categories.Where(x => x.Parent == null).ToList();
        }

        /// <summary>
        /// Returns a list of all pages.
        /// </summary>
        /// <returns></returns>
        public List<Page> GetPages()
        {
            return _context.pages.ToList();
        }

        /// <summary>
        /// Returns data used on frontpage. Wrapped in the FrontpageViewModel class
        /// </summary>
        /// <returns>Up to 4 news and all of the root categories.</returns>
        public FrontpageViewModel GetFrontpage()
        {
            return new FrontpageViewModel() {News = GetTopNews(4), Categories = GetRootCategories()};
        }

        /// <summary>
        /// Getter of the EF data Context.
        /// </summary>
        /// <returns></returns>
        internal CMSEntities GetDBContext()
        {
            return _context;
        }
    }
}
