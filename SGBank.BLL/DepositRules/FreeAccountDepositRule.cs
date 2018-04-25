using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models.Responses;
using SGBank.Models;

namespace SGBank.BLL.DepositRules
{
    public class FreeAccountDepositRule : IDeposit
    {
        public AccountDepositResponse Deposit(Account account, decimal amount)
        {
            AccountDepositResponse response = new AccountDepositResponse();
            if(account.Type != AccountType.Free)
            {
                response.Success = false;
                response.Message = "Error: a non free account hit the Free Deposit Rule.";
                return response;
            }
            
            if(amount >100)
            {
                response.Success = false;
                response.Message = "Free accounts cannot deposit more than $100 at a time";
                return response;

            }
            if (amount <=0)
            {
                response.Success = false;
                response.Message = "Deposits must be greater than zero.";
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
