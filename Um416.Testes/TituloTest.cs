using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Um416.BLL;

namespace Um416.Testes
{
    [TestClass]
    public class TituloTest
    {
        [TestMethod]
        public void BaixarTitulo()
        {
            var tituloBo = new TituloLogic();
            
            tituloBo.BaixarTitulo(256);
        }
    }
}
