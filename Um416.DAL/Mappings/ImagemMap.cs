using Dapper.FluentMap.Dommel.Mapping;

namespace Um416.DAL.Mappings
{
    public class ImagemMap : DommelEntityMap<Imagem>
    {
        public ImagemMap()
        {
            ToTable("Imagens");

            Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(x => x.Source).ToColumn("Source");
        }
    }
}
