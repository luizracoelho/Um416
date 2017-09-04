using Dapper.FluentMap.Dommel.Mapping;

namespace Um416.DAL.Mappings
{
    public class UsuarioEmpresaMap : DommelEntityMap<UsuarioEmpresa>
    {
        public UsuarioEmpresaMap()
        {
            ToTable("Empresas");

            Map(x => x.Id).ToColumn("Id").IsKey().IsIdentity();
            Map(x => x.Nome).ToColumn("Nome");
            Map(x => x.Cpf).ToColumn("Cpf");
            Map(x => x.TipoPessoa).ToColumn("TipoPessoa");
            Map(x => x.Ativa).ToColumn("Ativa");
            Map(x => x.Email).ToColumn("Email");
            Map(x => x.TelFixo).ToColumn("TelFixo");
            Map(x => x.TelMovel).ToColumn("TelMovel");
            Map(x => x.Login).ToColumn("Login");
            Map(x => x.Senha).ToColumn("Senha");
            Map(x => x.DataCadastro).ToColumn("DataCadastro");
            Map(x => x.Rg).ToColumn("Rg");
            Map(x => x.Logradouro).ToColumn("Logradouro");
            Map(x => x.Numero).ToColumn("Numero");
            Map(x => x.Complemento).ToColumn("Complemento");
            Map(x => x.Bairro).ToColumn("Bairro");
            Map(x => x.Cidade).ToColumn("Cidade");
            Map(x => x.Uf).ToColumn("Uf");
            Map(x => x.Cep).ToColumn("Cep");
        }
    }
}
