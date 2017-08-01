using System.Collections.Generic;
using System.Web.Http;
using Um416.BLL.Interfaces;
using Um416.Interfaces;

namespace Um416.API.Controllers.Base
{
    public class BaseController<T, TBO> : ApiController
        where T : class, IBaseClass
        where TBO : class, IBaseLogic<T>, new()
    {
        protected TBO _bo;
        public BaseController()
        {
            _bo = new TBO();
        }

        // GET: api/Base
        public virtual IEnumerable<T> Get()
        {
            return _bo.List();
        }

        // GET: api/Base/5
        public virtual T Get(long id)
        {
            return _bo.Get(id);
        }

        // POST: api/Base
        public virtual void Post(T entity)
        {
            _bo.Save(entity);
        }

        // DELETE: api/Base/5
        public virtual void Delete(long id)
        {
            _bo.Delete(id);
        }
    }
}
