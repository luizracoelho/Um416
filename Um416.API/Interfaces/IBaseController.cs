using System.Collections.Generic;
using Um416.BLL.Interfaces;
using Um416.Interfaces;

namespace Um416.API.Interfaces
{
    public interface IBaseController<T, TBO>
            where T : class, IBaseClass
            where TBO : class, IBaseLogic<T>, new()
    {
        IEnumerable<T> Get();
        T Get(long id);
        void Post(T entity);
        void Delete(long id);
    }
}