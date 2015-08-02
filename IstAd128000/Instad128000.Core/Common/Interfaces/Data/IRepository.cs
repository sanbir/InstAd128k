using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Instad128000.Core.Common.Models.DataModels;

namespace Instad128000.Core.Common.Interfaces.Data
{
    public interface IRepository<T>
    {
        T Get(Guid id);
        IQueryable<T> GetAll(bool showDeleted = false);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false);
        T Insert(T o);
        void Save();
        void Delete(T o, bool hard);
        void Restore(T o);
        void Attach(BaseEntity b);
        void DeAttach(BaseEntity b);
    }
}
