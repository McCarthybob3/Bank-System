using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.BLL;
using SGBank.Models.Responses;

namespace SGBank.WorkFlows
{
    class DepositWorkflow
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
                Console.Write("Enter a deposit amount: ");


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



            AccountDepositResponse response = accountManager.Deposit(accountNumber, amount);


            if (response.Success)
            {
                Console.WriteLine("Deposit completed!");
                Console.WriteLine($"Account Number: {response.Account.AccountNumber}");
                Console.WriteLine($"Old Balance: {response.OldBalance:c}");
                Console.WriteLine($"Amount Deposited: {response.amount:c}");
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
