using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Interfaces.Data;
using Instad128000.Core.Common.Models.DataModels;
using Omu.ValueInjecter;

namespace InstAd128000.SqlLite
{
    public class Repository<T> : IRepository<T>
       where T : BaseEntity, new()
    {
        protected readonly DbContext DbContext;

        public Repository(IDbContextFactory f)
        {
            this.DbContext = f.GetContext();
        }

        public void Save()
        {
            var entityes = this.DbContext.ChangeTracker.Entries().Where(x => (x.State == EntityState.Modified || x.State == EntityState.Added || x.State == EntityState.Deleted));
            foreach (DbEntityEntry entity in entityes)
            {
                if (entity.Entity is BaseEntity)
                {
                    var e = entity.Entity as BaseEntity;

                    e.ModifyDate = DateTime.UtcNow;

                    if (e.CreateDate <= DateTime.MinValue)
                    {
                        e.CreateDate = DateTime.UtcNow;
                    }
                }
            }

            try
            {
                this.DbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult dbEntityValidationResult in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError dbValidationError in dbEntityValidationResult.ValidationErrors)
                    {
                        /* Logger.Current.Error(
                             string.Format(
                                 "ValidationError: {0} : {1}",
                                 dbValidationError.PropertyName,
                                 dbValidationError.ErrorMessage));*/
                    }
                }
                throw new Exception("Database validation error!");
            }
            catch (Exception e)
            {
                throw new Exception("Database error! " + e.Message);
            }
        }


        public T Insert(T o)
        {
            var t = this.DbContext.Set<T>().Create();
            t.InjectFrom(o);
            t.CreateDate = DateTime.UtcNow;
            this.DbContext.Set<T>().Add(t);
            return t;
        }

        public virtual void Delete(T o, bool hard)
        {
            if (hard)
            {
                var a = this.Get(o.ID);
                this.DbContext.Set<T>().Remove(a);
                this.DbContext.SaveChanges();
                return;
            }
            var j = this.Get(o.ID);
            j.IsDeleted = true;
            j.ModifyDate = DateTime.UtcNow;
            this.Save();
        }

        public T Get(Guid id)
        {
            return this.DbContext.Set<T>().Find(id);
        }

        public void Restore(T o)
        {
            var j = this.Get(o.ID);
            j.IsDeleted = false;
            j.ModifyDate = DateTime.UtcNow;
            this.Save();
        }

        public void Attach(BaseEntity b)
        {
            var q = b.GetType();
            var set = this.DbContext.Set(q);
            set.Attach(b);
        }

        public void DeAttach(BaseEntity b)
        {
            ((IObjectContextAdapter)DbContext).ObjectContext.Detach(b);
            //this.DbContext.Entry(b).State = EntityState.Detached;
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false)
        {
            return showDeleted ? this.DbContext.Set<T>().Where(predicate) : this.DbContext.Set<T>().Where(x => !x.IsDeleted).Where(predicate);
        }

        public virtual IQueryable<T> GetAll(bool showDeleted = false)
        {
            return showDeleted ? this.DbContext.Set<T>() : this.DbContext.Set<T>().Where(x => !x.IsDeleted);
        }
    }
}
