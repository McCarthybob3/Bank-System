using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using SGBank.Models;

namespace SGBank.BLL.WithdrawRules
{
   public class PremiumAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();
            if (account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Message = "Error: a non-Premium account hit the basic Withdraw Rule, Contact IT";
                return response;
            }
            if (amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdrawal amounts must be negative";
                return response;
            }

            if (account.Balance + amount < -500)
            {
                response.Success = false;
                response.Message = "This amount with overdraft is more than your $500 dollar limit";
                return response;
            }

            response.OldBalance = account.Balance;
            account.Balance = account.Balance + amount;
            response.Account = account;
            response.amount = amount;
            response.Success = true;

            response.Success = true;

            return response;
        }
    }
}
