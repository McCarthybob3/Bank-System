using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.BLL;
using SGBank.Models;
using SGBank.Models.Responses;
using SGBank.Models.Interfaces;
using SGBank.BLL.WithdrawRules;
using SGBank.BLL.DepositRules;

namespace SGBank.Tests
{
    [TestFixture]
    public class FreeAccountTests
    {
        [Test]
        public void CanLoadFreeAccountTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();

            AccountLookUpResponse response = manager.LookupAccount("12345");




            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("12345", response.Account.AccountNumber);
        }

       
       [TestCase("12345", "Free Account", 100, AccountType.Free, 250, false)]
       [TestCase("12345", "Free Account",100,AccountType.Free,-100,false)]
       [TestCase("12345", "Free Account", 100, AccountType.Basic, 50, false)]
       [TestCase("12345", "Free Account", 100, AccountType.Free, 50, true)]
        public void FreeAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {

            IDeposit deposit = new FreeAccountDepositRule();

            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Balance = balance;
            account.Name = name;
            account.Type = accountType;

            AccountDepositResponse result = deposit.Deposit(account,amount);


        
            Assert.AreEqual(expectedResult, result.Success);


        }
        [TestCase ("12345","Free Account",100, AccountType.Free,50,false)]
        [TestCase("12345", "Free Account", 100, AccountType.Free, -200, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Basic, 50, false)]
        [TestCase("12345", "Free Account", 89, AccountType.Free, -90, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Free, -50, true)]
        public void FreeAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {

            IWithdraw withdraw = new FreeAccountWithdrawRule();

            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Balance = balance;
            account.Name = name;
            account.Type = accountType;

            AccountWithdrawResponse result = withdraw.Withdraw(account, amount);



            Assert.AreEqual(expectedResult, result.Success);
        }
    }
}
