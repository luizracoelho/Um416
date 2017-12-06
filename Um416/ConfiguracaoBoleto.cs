using Um416.Base;

namespace Um416
{
    public class ConfiguracaoBoleto : BaseClass
    {
        private string _codigoEmpresa;
        private string _chave;
        private string _urlRetorna;
        private string _observacao1;
        private string _observacao2;
        private string _observacao3;

        public string CodigoEmpresa
        {
            get { return _codigoEmpresa; }
            set { _codigoEmpresa = value?.ToUpper(); }
        }
        public string Chave
        {
            get { return _chave; }
            set { _chave = value?.ToUpper(); }
        }
        public string UrlRetorna
        {
            get { return _urlRetorna; }
            set { _urlRetorna = value?.ToUpper(); }
        }
        public string Observacao1
        {
            get { return _observacao1; }
            set { _observacao1 = value?.ToUpper(); }
        }
        public string Observacao2
        {
            get { return _observacao2; }
            set { _observacao2 = value?.ToUpper(); }
        }
        public string Observacao3
        {
            get { return _observacao3; }
            set { _observacao3 = value?.ToUpper(); }
        }
    }
}
