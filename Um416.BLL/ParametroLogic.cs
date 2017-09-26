using System;
using Um416.BLL.Base;
using Um416.DAL;

namespace Um416.BLL
{
    public class ParametroLogic : BaseLogic<Parametro, ParametroRepository>
    {
        public override void Save(Parametro entity)
        {
            //Verificar se a Url de Vendas foi preenchida
            if (string.IsNullOrEmpty(entity.UrlVenda))
                throw new Exception("Url de venda inválida!");

            //Verificar se não termina com '/', caso não, insere a '/'
            if (!entity.UrlVenda.EndsWith("/"))
                entity.UrlVenda += "/";

            //Verificar se a Url de Clientes foi preenchida
            if (string.IsNullOrEmpty(entity.UrlCliente))
                throw new Exception("Url de cliente inválida!");

            //Verificar se não termina com '/', caso não, insere a '/'
            if (!entity.UrlCliente.EndsWith("/"))
                entity.UrlCliente += "/";

            //Salva o parâmetro
            base.Save(entity);
        }
    }
}
