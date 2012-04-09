using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DomainModel.Entity;

namespace Model.Subsystem
{
    public abstract class AbstractService<T> : IModelService where T : DomainModel.Entity.EntityBase, new()
    {
        protected ServiceLayer _sl;

        public void SetServiceLayer(ServiceLayer sl)
        {
            _sl = sl;
        }

        public virtual List<T> ToList()
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

        protected Text SetTextValue(Text text, string lang, string s)
        {
            Text outText;
            if (text == null) {
                outText = new Text();
                _sl.GetDBContext().texts.Add(outText);
            }
            else
            {
                outText = text;
            }

            TextValue dbVal = outText.Values == null ? null : outText.Values.SingleOrDefault(x => x.Culture == lang);
            TextValue value;
            if (dbVal == null)
            {
                value = new TextValue() {Culture = lang, Text = outText};
                _sl.GetDBContext().texts_values.Add(value);
            }
            else
            {
                value = dbVal;
            }
            
            value.Value = s;
            value.SeoValue = MakeAlias(s);

            return outText;
        }

        public string MakeAlias(string name)
        {
            if (name == null) return String.Empty;

            name = name.ToLower();
            string normalized = name.Normalize(NormalizationForm.FormD);
            int length = normalized.Length;

            StringBuilder b = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(normalized[i]);


                if (uc != UnicodeCategory.LowercaseLetter && uc != UnicodeCategory.DecimalDigitNumber)
                {
                    if (normalized[i] == ' ')
                    {
                        b.Append("-");
                    }
                }
                else if (uc != UnicodeCategory.NonSpacingMark)
                {
                    b.Append(normalized[i]);
                }
            }
            return (b.ToString().Normalize(NormalizationForm.FormC)).Replace("--", "-");
        }

        protected abstract System.Data.Entity.DbSet<T> GetItemSet(DomainModel.CMSEntities ctx);

        public virtual T Edit(long? id, FormCollection values)
        {
            T obj;

            if (id == null || id < 1)
            {
                obj = new T();
                GetItemSet().Add(obj);
            }
            else
            {
                obj = GetById((long)id);
            }

            return obj;
        }

        public void DeleteById(long id)
        {
            GetItemSet().Remove(GetById(id));
            _sl.GetDBContext().SaveChanges();
        }
    }
}
