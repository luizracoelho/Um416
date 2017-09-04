using Um416.Enumerators;

namespace Um416
{
    public class UsuarioEmpresa : PessoaEndereco
    {
        public TipoPessoa TipoPessoa { get; set; }
        public bool Ativa { get; set; }
    }
}
