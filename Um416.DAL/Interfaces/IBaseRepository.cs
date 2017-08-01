using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Um416.Interfaces;

namespace Um416.DAL.Interfaces
{
    public interface IBaseRepository<T> where T : IBaseClass
    {
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        T Get(long id);
        T Get(Expression<Func<T, bool>> predicate);
        long Insert(T entity);
        bool Update(T entity);
        bool Delete(long id);
    }
}
