using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entity;

namespace Model.Subsystem
{
    public class NewsService : AbstractService<News>
    {
        public News GetById(long id)
        {
            News n = _sl.GetDBContext().News.Single(x => x.Id == id);
            return n;
        }

        public List<News> GetTop(int count)
        {
            return _sl.GetDBContext().News.OrderByDescending(x => x.Date).Take(count).ToList();
        }

        protected override System.Data.Entity.DbSet<News> GetItemSet(DomainModel.CMSEntities ctx)
        {
            return ctx.News;
        }
    }
}
