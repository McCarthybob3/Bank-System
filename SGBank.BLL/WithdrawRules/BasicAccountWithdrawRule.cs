﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using SGBank.Models;

namespace SGBank.BLL.WithdrawRules
{
    public class BasicAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();
            if(account.Type != AccountType.Basic)
            {
                response.Success = false;
                response.Message = "Error: a non-basic account hit the basic Withdraw Rule, Contact IT";
                return response;
            }
            if(amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdrawal amounts must be negative";
                return response;
            }

            if (amount < -500)
            {
                response.Success = false;
                response.Message = "Basic accounts cannot withdraw more than $500!";
                return response;
            }

            if(account.Balance + amount < -100)
            {
                response.Success = false;
                response.Message = "This amount with overdraft more than your $100 dollar limit";
                return response;
            }

            response.OldBalance = account.Balance;
            account.Balance = account.Balance + amount;
            response.Account = account;
            if (account.Balance < 0)
            {
                Console.WriteLine("Looks like someone has an overdraft fee");
                account.Balance = account.Balance-10;

            }
           
                response.amount = amount;
            
            
            response.Success = true;

            return response;
        }
    }
}
