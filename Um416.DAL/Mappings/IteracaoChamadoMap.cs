using Dapper.FluentMap.Dommel.Mapping;

namespace Um416.DAL.Mappings
{
    public class IteracaoChamadoMap : DommelEntityMap<IteracaoChamado>
    {
        public IteracaoChamadoMap()
        {
            ToTable("IteracoesChamados");

            Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(x => x.Conteudo).ToColumn("Conteudo");
            Map(x => x.DataHora).ToColumn("DataHora");
            Map(x => x.AdminId).ToColumn("AdminId");
            Map(x => x.ChamadoId).ToColumn("ChamadoId");
        }
    }
}
