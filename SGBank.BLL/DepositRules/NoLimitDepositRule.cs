using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using SGBank.Models;

namespace SGBank.BLL.DepositRules
{
    public class NoLimitDepositRule : IDeposit
    {
        public AccountDepositResponse Deposit(Account account, decimal amount)
        {
            AccountDepositResponse response = new AccountDepositResponse();
            if (account.Type == AccountType.Free )
            {
                response.Success = false;
                response.Message = "Error: only basic and premium accounts can deposit with no limit. Contact IT";
                return response;
            }
            if (amount <= 0)
            {
                response.Success = false;
                response.Message = "Deposit amounts must be positive!";
                return response;
            }

            response.OldBalance = account.Balance;
            account.Balance += amount;
            response.Account = account;
            response.amount = amount;
            response.Success = true;

            return response;

        }
    }
}
