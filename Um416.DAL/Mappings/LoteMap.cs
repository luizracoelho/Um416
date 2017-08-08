using Dapper.FluentMap.Dommel.Mapping;

namespace Um416.DAL.Mappings
{
    public class LoteMap : DommelEntityMap<Lote>
    {
        public LoteMap()
        {
            ToTable("Lotes");

            Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(x => x.Codigo).ToColumn("Codigo");
            Map(x => x.Descricao).ToColumn("Descricao");
            Map(x => x.Area).ToColumn("Area");
            Map(x => x.Valor).ToColumn("Valor");
            Map(x => x.TipoLote).ToColumn("TipoLote");
            Map(x => x.Logradouro).ToColumn("Logradouro");
            Map(x => x.Numero).ToColumn("Numero");
            Map(x => x.Complemento).ToColumn("Complemento");
            Map(x => x.Bairro).ToColumn("Bairro");
            Map(x => x.Cidade).ToColumn("Cidade");
            Map(x => x.Uf).ToColumn("Uf");
            Map(x => x.Cep).ToColumn("Cep");
            Map(x => x.LoteamentoId).ToColumn("LoteamentoId");
        }
    }
}
