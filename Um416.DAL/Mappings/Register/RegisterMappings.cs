using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;

namespace Um416.DAL.Mappings.Register
{
    public class RegisterMappings
    {
        public static void Register()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UsuarioAdminMap());
                config.AddMap(new UsuarioClienteMap());
                config.AddMap(new UsuarioVendedorMap());
                config.AddMap(new NotificacaoMap());
                config.AddMap(new LeituraNotificacaoMap());
                config.AddMap(new ChamadoMap());
                config.AddMap(new IteracaoChamadoMap());
                config.AddMap(new ImagemMap());
                config.AddMap(new LoteamentoMap());
                config.AddMap(new LoteMap());
                config.AddMap(new UsuarioEmpresaMap());
                config.AddMap(new ParametroMap());
                config.AddMap(new VendaMap());
                config.AddMap(new TituloMap());
                config.AddMap(new ConfiguracaoBoletoMap());

                config.ForDommel();
            });
        }
    }
}
