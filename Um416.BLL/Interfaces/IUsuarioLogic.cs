using System;
using System.Linq.Expressions;

namespace Um416.BLL.Interfaces
{
    public interface IUsuarioLogic
    {
        Pessoa Get(string login);
    }
}
