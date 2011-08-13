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
        private CMSEntities _context = new CMSEntities();

        private Dictionary<Type, object> _serviceCache = new Dictionary<Type, object>();

        public T GetSubsystem<T>() where T : Subsystem.AbstractService, new()
        {
            if (!_serviceCache.ContainsKey(typeof(T)))
            {
                Subsystem.AbstractService instance = new T();
                instance.SetServiceLayer(this);

                _serviceCache.Add(typeof(T), instance);
            }
            return (T) _serviceCache[typeof(T)];
        }

        public List<News> GetTopNews(int count)
        {
            return _context.News.OrderByDescending(x => x.Date).Take(count).ToList();
        }

        public List<Category> GetCarousel()
        {
            return _context.categories.Where(x => x.Parent == null).ToList();
        }

        public FrontpageViewModel GetFrontpage()
        {
            return new FrontpageViewModel() {News = GetTopNews(4), Categories = GetCarousel()};
        }

        internal CMSEntities GetDBContext()
        {
            return _context;
        }
    }
}
