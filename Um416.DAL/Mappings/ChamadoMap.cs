using Dapper.FluentMap.Dommel.Mapping;

namespace Um416.DAL.Mappings
{
    public class ChamadoMap : DommelEntityMap<Chamado>
    {
        public ChamadoMap()
        {
            ToTable("Chamados");

            Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(x => x.Titulo).ToColumn("Titulo");
            Map(x => x.DataHoraCriacao).ToColumn("DataHoraCriacao");
            Map(x => x.DataHoraEncerramento).ToColumn("DataHoraEncerramento");
            Map(x => x.TipoEncerramento).ToColumn("TipoEncerramento");
            Map(x => x.AdminEncerramentoId).ToColumn("AdminEncerramentoId");
            Map(x => x.Encerrado).ToColumn("Encerrada");
            Map(x => x.ClienteId).ToColumn("ClienteId");
        }
    }
}
