using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.BLL;
using SGBank.Models.Responses;
using SGBank.Models.Interfaces;

namespace SGBank.WorkFlows
{
     class WithdrawWorkFlow
    {
        public void Execute()
        {
            decimal amount;
            bool Loop = true;
            Console.Clear();
            AccountManager accountManager = AccountManagerFactory.Create();

            Console.WriteLine("Enter an account number: ");
            string accountNumber = Console.ReadLine();


            do
            {
                Console.Write("Enter a Withdrawal amount: ");


                bool result = decimal.TryParse(Console.ReadLine(), out amount);
                if (result)
                {
                    Loop = false;
                }
                else
                {
                    Console.WriteLine("That's not an amount, try again, friend");
                    Loop = true;
                }
            }
            while (Loop == true);



            AccountWithdrawResponse response = accountManager.Withdraw(accountNumber, amount);

            if (response.Success)
            {
                Console.WriteLine("Withdrawal completed!");
                Console.WriteLine($"Account Number: {response.Account.AccountNumber}");
                Console.WriteLine($"Old Balance: {response.OldBalance:c}");
                Console.WriteLine($"Amount Withdrawn: {response.amount:c}");
                Console.WriteLine($"New balance: {response.Account.Balance:c}");



            }
            else
            {
                Console.WriteLine("An error occurred.");
                Console.WriteLine(response.Message);
            }


            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }

    }
}
