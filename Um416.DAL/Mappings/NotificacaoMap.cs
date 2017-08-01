using Dapper.FluentMap.Dommel.Mapping;

namespace Um416.DAL.Mappings
{
    public class NotificacaoMap : DommelEntityMap<Notificacao>
    {
        public NotificacaoMap()
        {
            ToTable("Notificacoes");

            Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(x => x.Titulo).ToColumn("Titulo");
            Map(x => x.Mensagem).ToColumn("Mensagem");
            Map(x => x.DataHora).ToColumn("DataHora");
        }
    }
}
