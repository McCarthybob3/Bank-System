using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models.Interfaces;
using SGBank.Models;

namespace SGBank.BLL.DepositRules
{
    public class DepositRulesFactory
    {
        public static IDeposit Create(AccountType type)
        {
            switch (type)
            {
                case AccountType.Free:
                    return new FreeAccountDepositRule();

                case AccountType.Basic:
                    return new NoLimitDepositRule();

                case AccountType.Premium:
                    return new NoLimitDepositRule();

                default:
                    throw new Exception("Account type is not supported!");
            }

            throw new Exception("Account type is not supported!");
        }
    }
}
