using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models.Interfaces;
using SGBank.Data;
using SGBank.Models;
using System.IO;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {

       
            private string _filePath;

            public FileAccountRepository(string filePath)
            {
                _filePath = filePath;
            
            }
        int counter = 0;
        public List<Account> List()
        {
            List<Account> Accounts = new List<Account>();
           
            using (StreamReader sr = new StreamReader(_filePath))
                {
                    sr.ReadLine();
                    string line;

                while ((line = sr.ReadLine()) != null)
                {
                    Account newAccount = new Account();

                    string[] columns = line.Split(',');

                    newAccount.AccountNumber = columns[0];
                    newAccount.Name = columns[1];
                    newAccount.Balance = Decimal.Parse(columns[2]);
                    string Thetype = columns[3];
                    if (Thetype == "F")
                    {
                        newAccount.Type = AccountType.Free;
                    }
                    else if(Thetype == "B")
                    {
                        newAccount.Type = AccountType.Basic;
                    }
                    else if(Thetype == "P")
                    {
                        newAccount.Type = AccountType.Premium;
                    }
                   // else { throw new Exception("There seems to be an issue in the accounts text file"); }


                        Accounts.Add(newAccount);
                    }
                }

                return Accounts;
            
            }

        public Account LoadAccount(string AccountNumber)
        {
         
            var accounts = List();

            CreateStudentFile(accounts);
            for (int i = 0; i < accounts.Count; i++)
            {


                if (accounts[i].AccountNumber == AccountNumber)
                {
                    return accounts[i];
                }
                counter++;
            }
            return null;
        }

        public void SaveAccount(Account account)
        {
            var accounts = List();
            accounts[counter] = account;
            CreateStudentFile(accounts);
        }
        private string CreateCsvForStudent(Account student)
        {
            string shorthand="";
            
            if(student.Type == AccountType.Basic)
            {
                shorthand = "B";
            }
            if (student.Type == AccountType.Premium)
            {
                shorthand = "P";
            }
            if(student.Type == AccountType.Free)
            {
                shorthand = "F";
            }


            return string.Format("{0},{1},{2},{3}", student.AccountNumber,
                    student.Name, student.Balance, shorthand);
        }
      
        private void CreateStudentFile(List<Account> students)
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            using (StreamWriter sr = new StreamWriter(_filePath))
            {
                sr.WriteLine("AccountNumber,Name,Balance,Type");
                foreach (var student in students)
                {
                    sr.WriteLine(CreateCsvForStudent(student));
                }
            }
        }
       

    }
}
