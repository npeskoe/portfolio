using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SGBank.Data
{
    public class FileAccountTestRepository : IAccountRepository
    {
        private static string _filePath = @"C:\Data\SGBank\Accounts.txt";

        public List<Account> GetAccounts(string filePath)
        {
            _filePath = filePath;

            List<Account> accounts = new List<Account>();

            using (StreamReader sr = new StreamReader(_filePath))
            {
                sr.ReadLine();
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    Account account = new Account();

                    string[] columns = line.Split(',');

                    account.AccountNumber = columns[0];
                    account.Name = columns[1];
                    account.Balance = decimal.Parse(columns[2]);

                    string typeCode = columns[3];

                    switch (typeCode)
                    {
                        case "F":
                            account.Type = AccountType.Free;
                            break;
                        case "B":
                            account.Type = AccountType.Basic;
                            break;
                        case "P":
                            account.Type = AccountType.Premium;
                            break;
                    }

                    accounts.Add(account);
                }
                return accounts;
            }
        }

        public Account LoadAccount(string AccountNumber)
        {
            var loadAccounts = GetAccounts(_filePath);

            var loadedAccount = loadAccounts.Where(a => a.AccountNumber == AccountNumber);

            foreach (var _account in loadedAccount)
            {
                return _account;
            }
            return null;
        }

        public void SaveAccount(Account account)
        {
            var loadAccounts = GetAccounts(_filePath);

            var targetAccount = loadAccounts.Where(a => a.AccountNumber == account.AccountNumber);

            Account _account;

            foreach (var a in targetAccount)
            {
                _account = account;
            }

            int index = loadAccounts.FindIndex(a => a.AccountNumber == account.AccountNumber);

            loadAccounts.RemoveAt(index);

            loadAccounts.Add(account);

            using (StreamWriter sr = new StreamWriter(_filePath))
            {
                sr.WriteLine("AccountNumber,Name,Balance,Type");

                foreach (var acct in loadAccounts)
                {
                    string accType = null;

                    switch (acct.Type)
                    {
                        case AccountType.Basic:
                            accType = "B";
                            break;
                        case AccountType.Free:
                            accType = "F";
                            break;
                        case AccountType.Premium:
                            accType = "P";
                            break;
                    }

                    sr.WriteLine(acct.AccountNumber + "," + acct.Name + "," + acct.Balance + "," + accType);  
                }                
            }
        }
    }
}


