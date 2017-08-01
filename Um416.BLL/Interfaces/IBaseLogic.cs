using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Um416.BLL.Interfaces
{
    public interface IBaseLogic<T> where T : class
    {
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        T Get(long id);
        T Get(Expression<Func<T, bool>> predicate);
        void Save(T entity);
        bool Delete(long id);
    }
}
