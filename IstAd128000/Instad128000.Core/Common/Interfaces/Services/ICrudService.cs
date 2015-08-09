using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Instad128000.Core.Common.Models.DataModels;

namespace Instad128000.Core.Common.Interfaces.Data.Services
{
    public interface ICrudService<T> where T : BaseEntity, new()
    {
        Guid Update(T item);
        void Save();
        void Delete(Guid id, bool hard = false);
        T Get(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Where(Expression<Func<T, bool>> func, bool showDeleted = false);
        void Restore(Guid id);
        void Attach(BaseEntity t);
    }
}
