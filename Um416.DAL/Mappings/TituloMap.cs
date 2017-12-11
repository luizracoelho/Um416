using Dapper.FluentMap.Dommel.Mapping;

namespace Um416.DAL.Mappings
{
    public class TituloMap : DommelEntityMap<Titulo>
    {
        public TituloMap()
        {
            ToTable("Titulos");

            Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(x => x.Numero).ToColumn("Numero");
            Map(x => x.Valor).ToColumn("Valor");
            Map(x => x.ValorLiquido).ToColumn("ValorLiquido");
            Map(x => x.DataVencimento).ToColumn("DataVencimento");
            Map(x => x.Pago).ToColumn("Pago");
            Map(x => x.DataPgto).ToColumn("DataPgto");
            Map(x => x.ValorPgto).ToColumn("ValorPgto");
            Map(x => x.VendaId).ToColumn("VendaId");
            Map(x => x.BoletoGerado).ToColumn("BoletoGerado");
        }
    }
}
