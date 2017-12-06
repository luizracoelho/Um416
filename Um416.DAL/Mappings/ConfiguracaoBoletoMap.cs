using Dapper.FluentMap.Dommel.Mapping;

namespace Um416.DAL.Mappings
{
    public class ConfiguracaoBoletoMap : DommelEntityMap<ConfiguracaoBoleto>
    {
        public ConfiguracaoBoletoMap()
        {
            ToTable("ConfiguracoesBoletos");

            Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(x => x.CodigoEmpresa).ToColumn("CodigoEmpresa");
            Map(x => x.Chave).ToColumn("Chave");
            Map(x => x.UrlRetorna).ToColumn("UrlRetorna");
            Map(x => x.Observacao1).ToColumn("Observacao1");
            Map(x => x.Observacao2).ToColumn("Observacao2");
            Map(x => x.Observacao3).ToColumn("Observacao3");
        }
    }
}
