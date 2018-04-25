using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SGBank.Data;

namespace SGBank.BLL
{
   public static class AccountManagerFactory
    {
        public static AccountManager Create()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            string mode = ConfigurationSettings.AppSettings["Mode"].ToString();
#pragma warning restore CS0618 // Type or member is obsolete

            switch (mode)
            {
                case "FreeTest":
                    return new AccountManager(new FreeAccountTestRespository());
                case "BasicTest":
                    return new AccountManager(new BasicAccountTestRepository());
                case "PremiumTest":
                    return new AccountManager(new PremiumAccountTestRepository());
                case "FromFile":
                    return new AccountManager(new FileAccountRepository(@".\ReadThis\Accounts.txt"));
                default:
                    throw new Exception("Mode Value in app config is not valid");
            }
        }
    }
}
