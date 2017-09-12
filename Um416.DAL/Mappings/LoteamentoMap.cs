using Dapper.FluentMap.Dommel.Mapping;

namespace Um416.DAL.Mappings
{
    public class LoteamentoMap : DommelEntityMap<Loteamento>
    {
        public LoteamentoMap()
        {
            ToTable("Loteamentos");

            Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(x => x.Nome).ToColumn("Nome");
            Map(x => x.Descricao).ToColumn("Descricao");
            Map(x => x.DataCadastro).ToColumn("DataCadastro");
            Map(x => x.Logradouro).ToColumn("Logradouro");
            Map(x => x.Numero).ToColumn("Numero");
            Map(x => x.Bairro).ToColumn("Bairro");
            Map(x => x.Cidade).ToColumn("Cidade");
            Map(x => x.Uf).ToColumn("Uf");
            Map(x => x.Cep).ToColumn("Cep");
            Map(x => x.MapaId).ToColumn("MapaId");
            Map(x => x.EmpresaId).ToColumn("EmpresaId");
            Map(x => x.Mapa).Ignore();
            Map(x => x.Empresa).Ignore();
            Map(x => x.Url).Ignore();
            Map(x => x.NomeHashtag).Ignore();
            Map(x => x.Site).ToColumn("Site");
            Map(x => x.IndicadorMultinivel).ToColumn("IndicadorMultinivel");
        }
    }
}
