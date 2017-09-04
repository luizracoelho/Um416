using Um416.Base;

namespace Um416
{
    public abstract class Pessoa : BaseClass
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string TelFixo { get; set; }
        public string TelMovel { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
