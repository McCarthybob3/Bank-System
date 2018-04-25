using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.BLL.DepositRules;
using SGBank.Models.Responses;
using SGBank.BLL.WithdrawRules;

namespace SGBank.Tests
{
    [TestFixture]
    class PremiumAccountTests
    {

        [TestCase("11111","Premium Account", 100, AccountType.Free, 250, false)]
        [TestCase("11111", "Premium Account", 100, AccountType.Premium, -100, false)]
        [TestCase("11111", "Premium Account", 100, AccountType.Premium, 250, true)]
      public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResults)
        {
            IDeposit deposit = new NoLimitDepositRule();

            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Balance = balance;
            account.Name = name;
            account.Type = accountType;

            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(expectedResults, response.Success);

        }
        [TestCase("11111", "Premium Account", 1500, AccountType.Premium, -1000, 1500, true)]
        [TestCase("11111", "Premium Account", 100, AccountType.Free, -100, 100, false)]
        [TestCase("11111", "Premium Account", 100, AccountType.Premium, 100, 100, false)]
        [TestCase("11111", "Premium Account", 100, AccountType.Premium, -50, 100, true)]
        [TestCase("11111", "Premium Account", 100, AccountType.Premium, -150, -60, true)]
        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResults)
        {
            IWithdraw withdraw = new PremiumAccountWithdrawRule();

            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Balance = balance;
            account.Name = name;
            account.Type = accountType;


            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);
            Assert.AreEqual(expectedResults, response.Success);
            if(response.Success == true)
            {
                newBalance = response.Account.Balance;
            }


        }



    }
}
