using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Um416.BLL;
using Um416.DAL.Mappings.Register;

namespace Um416.Testes
{
    [TestClass]
    public class VendaTest
    {
        VendaLogic vendaBo;

        public VendaTest()
        {
            RegisterMappings.Register();
            vendaBo = new VendaLogic();
        }

        [TestMethod]
        public void Save()
        {

            var venda = new Venda
            {
                QuantParcelas = 36,
                DiaVencimento = 18,
                ClienteId = 1,
                LoteId = 12,
            };

            vendaBo.Save(venda);
        }

        [TestMethod]
        public void Delete()
        {
            vendaBo.Delete(11);
        }
    }
}
