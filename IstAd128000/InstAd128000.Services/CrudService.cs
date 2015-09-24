using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Interfaces.Data;
using Instad128000.Core.Common.Interfaces.Data.Services;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Common.Models.DataModels;
using Omu.ValueInjecter;

namespace InstAd128000.Services
{
    public class CrudService<T> : ICrudService<T> where T : BaseEntity, new()
    {
        protected IRepository<T> Repo;

        public CrudService(IRepository<T> repo)
        {
            this.Repo = repo;
        }

        public IEnumerable<T> GetAll()
        {
            return this.Repo.GetAll();
        }

        public T Get(Guid id)
        {
            return this.Repo.Get(id);
        }

        public virtual Guid Update(T item)
        {
            if (item.ID == Guid.Empty)
            {
                item.ID = Guid.NewGuid();
                var newItem = this.Repo.Insert(item);
                this.Repo.Save();
                return newItem.ID;
            }
            var itm = this.Get(item.ID);
            var date = itm.CreateDate;
            itm.InjectFrom(item);
            itm.CreateDate = date;
            this.Repo.Save();
            return item.ID;
        }

        public void Save()
        {
            this.Repo.Save();
        }

        public virtual void Delete(Guid id, bool hard = false)
        {
            this.Repo.Delete(this.Repo.Get(id), hard);
            this.Repo.Save();
        }
        public virtual void DeleteMany(IEnumerable<Guid> ids, bool hard = false)
        {
            foreach (var id in ids)
            {
                this.Repo.Delete(this.Repo.Get(id), hard);
            }

            this.Repo.Save();
        }

        public void Restore(Guid id)
        {
            this.Repo.Restore(this.Repo.Get(id));
            this.Repo.Save();
        }

        public void Attach(BaseEntity t)
        {
            this.Repo.Attach(t);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false)
        {
            return this.Repo.Where(predicate, showDeleted);
        }
    }
}
