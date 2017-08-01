using Dapper.FluentMap.Dommel.Mapping;

namespace Um416.DAL.Mappings
{
    public class UsuarioAdminMap : DommelEntityMap<UsuarioAdmin>
    {
        public UsuarioAdminMap()
        {
            ToTable("UsuariosAdmins");

            Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(x => x.Nome).ToColumn("Nome");
            Map(x => x.Cpf).ToColumn("Cpf");
            Map(x => x.Email).ToColumn("Email");
            Map(x => x.TelFixo).ToColumn("TelFixo");
            Map(x => x.TelMovel).ToColumn("TelMovel");
            Map(x => x.Login).ToColumn("Login");
            Map(x => x.Senha).ToColumn("Senha");
        }
    }
}
