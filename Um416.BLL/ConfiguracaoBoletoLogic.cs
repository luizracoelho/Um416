using System;
using System.Text;
using Um416.BLL.Base;
using Um416.DAL;

namespace Um416.BLL
{
    public class ConfiguracaoBoletoLogic : BaseLogic<ConfiguracaoBoleto, ConfiguracaoBoletoRepository>
    {
        public override void Save(ConfiguracaoBoleto entity)
        {
            Validar(entity);

            base.Save(entity);
        }

        private static void Validar(ConfiguracaoBoleto entity)
        {
            var errorStringBuilder = new StringBuilder();

            //Código da Empresa
            if (string.IsNullOrEmpty(entity.CodigoEmpresa))
                errorStringBuilder.Append("- O campo Código da Empresa é obrigatório;\n");
            else if (entity.CodigoEmpresa.Length != 26)
                errorStringBuilder.Append("- O campo Código da Empresa deve conter 26 caracteres;\n");

            //Chave
            if (string.IsNullOrEmpty(entity.Chave))
                errorStringBuilder.Append("- O campo Chave é obrigatório;\n");
            else if (entity.Chave.Length != 16)
                errorStringBuilder.Append("- O campo Chave deve conter 16 caracteres;\n");

            //UrlRetorna
            if (entity.UrlRetorna?.Length > 60)
                errorStringBuilder.Append("- O campo Url de Retorno não pdoe exceder 60 caracteres;\n");

            //Observacao1
            if (entity.Observacao1?.Length > 60)
                errorStringBuilder.Append("- O campo Observação 1 não pdoe exceder 60 caracteres;\n");

            //Observacao2
            if (entity.Observacao2?.Length > 60)
                errorStringBuilder.Append("- O campo Observação 2 não pdoe exceder 60 caracteres;\n");

            //Observacao3
            if (entity.Observacao3?.Length > 60)
                errorStringBuilder.Append("- O campo Observação 3 não pdoe exceder 60 caracteres;\n");

            var errors = errorStringBuilder.ToString();

            if (!string.IsNullOrEmpty(errors))
                throw new Exception($"Os seguintes problemas foram encontrados:\n{errors}");
        }
    }
}
