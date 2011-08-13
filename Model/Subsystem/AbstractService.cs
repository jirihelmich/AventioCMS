using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Subsystem
{
    public abstract class AbstractService<T> : IModelService where T : DomainModel.Entity.EntityBase
    {
        protected ServiceLayer _sl;

        public void SetServiceLayer(ServiceLayer sl)
        {
            _sl = sl;
        }

        public List<T> ToList()
        {
            return ToList<long>(x => x.Id);
        }

        public List<T> ToListDescending<TKey>(System.Linq.Expressions.Expression<Func<T, TKey>> orderKey)
        {
            return GetItemSet().OrderByDescending<T, TKey>(orderKey).ToList();
        }

        public List<T> ToList<TKey>(System.Linq.Expressions.Expression<Func<T, TKey>> orderKey)
        {
            return GetItemSet().OrderBy<T, TKey>(orderKey).ToList();
        }

        public T GetById(long Id)
        {
            return GetItemSet().Single(x => x.Id == Id);
        }

        public System.Data.Entity.DbSet<T> GetItemSet()
        {
            return GetItemSet(_sl.GetDBContext());
        }

        protected abstract System.Data.Entity.DbSet<T> GetItemSet(DomainModel.CMSEntities ctx);
    }
}
