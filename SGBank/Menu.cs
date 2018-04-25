using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.WorkFlows;

namespace SGBank
{
    public static class Menu
    {
        public static void Start()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("SG Bank Application ");
                Console.WriteLine("----------------------");
                Console.WriteLine("1. Lookup an Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("\nQ to quit");
                Console.WriteLine("\nEnter Selection:");

                string userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "1":
                        AccountLookupWorkflow LookupWorkFlow = new AccountLookupWorkflow();
                        LookupWorkFlow.Execute();
                        break;
                    case "2":
                        DepositWorkflow depositWorkflow = new DepositWorkflow();
                        depositWorkflow.Execute();
                        break;
                    case "3":
                        WithdrawWorkFlow WithdrawWorkflow = new WithdrawWorkFlow();
                        WithdrawWorkflow.Execute();
                        break;
                    case "Q":
                        return;
                    default: 
                        Console.WriteLine("That's not a selection");
                        break;


                }

            }
        }
    }
}
