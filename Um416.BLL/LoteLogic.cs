using System.Collections.Generic;
using Um416.BLL.Base;
using Um416.DAL;

namespace Um416.BLL
{
    public class LoteLogic : BaseLogic<Lote, LoteRepository>
    {
        public virtual IEnumerable<Lote> List(long? id)
        {
            return _dao.List(id);
        }
    }
}
