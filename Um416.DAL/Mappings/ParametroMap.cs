using Dapper.FluentMap.Dommel.Mapping;

namespace Um416.DAL.Mappings
{
    public class ParametroMap : DommelEntityMap<Parametro>
    {
        public ParametroMap()
        {
            ToTable("Parametros");

            Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(x => x.UrlVenda).ToColumn("UrlVenda");
            Map(x => x.UrlCliente).ToColumn("UrlCliente");
        }
    }
}
