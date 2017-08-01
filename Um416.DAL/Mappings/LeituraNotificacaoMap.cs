using Dapper.FluentMap.Dommel.Mapping;

namespace Um416.DAL.Mappings
{
    public class LeituraNotificacaoMap : DommelEntityMap<LeituraNotificacao>
    {
        public LeituraNotificacaoMap()
        {
            ToTable("LeiturasNotificacoes");

            Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(x => x.NotificacaoId).ToColumn("NotificacaoId");
            Map(x => x.ClienteId).ToColumn("ClienteId");
            Map(x => x.DataHoraLeitura).ToColumn("DataHoraLeitura");
            Map(x => x.Lida).ToColumn("Lida");
        }
    }
}
