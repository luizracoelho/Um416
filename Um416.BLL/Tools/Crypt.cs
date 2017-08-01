using Onsoft.Security;
using Onsoft.Security.Configuratios;

namespace Um416.BLL.Tools
{
    public class Crypt : OnCryptography
    {
        public Crypt()
        {
            Configuration = new OnCryptographyConfiguration
            {
                PasswordHash = "Um4D&z6$",
                SaltKey = "1%4&1A6z",
                VIKey = "U4m&QZu3a$tSr&o1"
            };
        }
    }
}
