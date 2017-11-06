using Dapper.FluentMap.Dommel.Mapping;

namespace Um416.DAL.Mappings
{
    public class VendaMap : DommelEntityMap<Venda>
    {
        public VendaMap()
        {
            ToTable("Vendas");

            Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(x => x.Numero).ToColumn("Numero");
            Map(x => x.DataHora).ToColumn("DataHora");
            Map(x => x.Valor).ToColumn("Valor");
            Map(x => x.QuantParcelas).ToColumn("QuantParcelas");
            Map(x => x.DiaVencimento).ToColumn("DiaVencimento");
            Map(x => x.ClienteId).ToColumn("ClienteId");
            Map(x => x.LoteId).ToColumn("LoteId");
            Map(x => x.ValorParcela).ToColumn("ValorParcela");
            Map(x => x.IndicadorId).ToColumn("IndicadorId");

            Map(x => x.Pagas).Ignore();
            Map(x => x.Valida).Ignore();
            Map(x => x.Mensagem).Ignore();
        }
    }
}
