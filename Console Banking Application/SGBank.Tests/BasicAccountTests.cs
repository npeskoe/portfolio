using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.BLL;
using SGBank.BLL.DepositRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;

namespace SGBank.Tests
{
    [TestFixture]
    class BasicAccountTests
    {
        [TestCase("33333", "Basic Account", 100, AccountType.Free, 250, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, -100, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, 250, true)]

        public void BasicAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType,
        decimal amount, bool expectedResult)
        {
            IDeposit deposit;

            FreeAccountDepositRule depositRule = new FreeAccountDepositRule();

            deposit = depositRule;

            Account account = new Account();

            accountNumber = account.AccountNumber;
            name = account.Name;
            balance = account.Balance;
            accountType = account.Type;
            amount = account.Balance;

            AccountDepositResponse response = deposit.Deposit(account, amount);

            expectedResult = response.Success;

            Assert.AreEqual(expectedResult, response.Success);
        }
        [TestCase("33333", "Basic Account", 1500, AccountType.Basic, -1000, 1500, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Free, -100, 100, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, 100, 100, false)]
        [TestCase("33333", "Basic Account", 150, AccountType.Basic, -50, 100, true)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, -150, -60, true)]

        public void BasicAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType,
        decimal amount, decimal newBalance, bool expectedResult)
        {
            IDeposit deposit;

            FreeAccountDepositRule depositRule = new FreeAccountDepositRule();

            deposit = depositRule;

            Account account = new Account();

            accountNumber = account.AccountNumber;
            name = account.Name;
            balance = account.Balance;
            accountType = account.Type;
            amount = account.Balance;

            AccountDepositResponse response = deposit.Deposit(account, amount);

            expectedResult = response.Success;

            Assert.AreEqual(expectedResult, response.Success);
        }
    }
}


