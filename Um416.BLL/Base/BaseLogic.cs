using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Um416.BLL.Interfaces;
using Um416.DAL.Interfaces;
using Um416.Interfaces;

namespace Um416.BLL.Base
{
    public class BaseLogic<T, TDAO> : IBaseLogic<T>
        where T : class, IBaseClass
        where TDAO : class, IBaseRepository<T>, new()
    {
        protected TDAO _dao;

        public BaseLogic()
        {
            _dao = new TDAO();
        }

        public virtual IEnumerable<T> List()
        {
            return _dao.List();
        }

        public virtual IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _dao.List(predicate);
        }

        public virtual T Get(long id)
        {
            return _dao.Get(id);
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return _dao.Get(predicate);
        }

        protected virtual void Insert(T entity)
        {
            entity.Id = _dao.Insert(entity);
        }

        protected virtual void Update(T entity)
        {
            _dao.Update(entity);
        }

        public virtual void Save(T entity)
        {
            if (entity.Id == 0)
                Insert(entity);
            else
                Update(entity);
        }

        public virtual bool Delete(long id)
        {
            return _dao.Delete(id);
        }
    }
}
