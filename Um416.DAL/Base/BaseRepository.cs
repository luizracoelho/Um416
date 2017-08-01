using Dommel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Um416.DAL.Interfaces;
using Um416.Interfaces;

namespace Um416.DAL.Base
{
    public class BaseRepository<T> : IBaseRepository<T> 
        where T : class, IBaseClass
    {
        protected string ConnectionString;

        protected BaseRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
        }

        public virtual IEnumerable<T> List()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.GetAll<T>();
            }
        }

        public virtual IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Select(predicate);
            }
        }

        public virtual T Get(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Get<T>(id);
            }
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Select(predicate).FirstOrDefault();
            }
        }

        public virtual long Insert(T entity)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return Convert.ToInt64(db.Insert(entity));
            }
        }

        public virtual bool Update(T entity)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Update(entity);
            }
        }

        public virtual bool Delete(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var entity = Get(id);

                if (entity == null)
                    return false;

                return db.Delete(entity);
            }
        }
    }
}
