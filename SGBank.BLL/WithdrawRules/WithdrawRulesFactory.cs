﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models.Responses;
using SGBank.Models;
using SGBank.Models.Interfaces;

namespace SGBank.BLL.WithdrawRules
{
   public static class WithdrawRulesFactory
    {
        public static IWithdraw Create(AccountType AccountType)
        {
            switch (AccountType)
            {
                case AccountType.Free:
                    return new FreeAccountWithdrawRule();

                case AccountType.Basic:
                    return new BasicAccountWithdrawRule();
                case AccountType.Premium:
                    return new PremiumAccountWithdrawRule();

                default:
                    throw new Exception("Account type is not supported!");
            }
        }

    }
}
