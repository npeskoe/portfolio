using NUnit.Framework;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{
    [TestFixture]
    class PremiumAccountTests
    {
        [TestCase("55555", "Premium Account", 100, AccountType.Free, 250, false)]
        [TestCase("55555", "Premium Account", 100, AccountType.Premium, -100, false)]
        [TestCase("55555", "Premium Account", 100, AccountType.Premium, 250, true)]

        public void PremiumAccountDepositRulesTest(string accountNumber, string name, decimal balance, AccountType accountType,
        decimal amount, bool expectedResult)
        {
            IDeposit deposit;

            NoLimitDepositRule depositRule = new NoLimitDepositRule();

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

        [TestCase("33333", "Premium Account", 1500, AccountType.Premium, -1000, 1500, false)]
        [TestCase("33333", "Premium Account", 100, AccountType.Free, -100, 100, false)]
        [TestCase("33333", "Premium Account", 100, AccountType.Premium, 100, 100, false)]
        [TestCase("33333", "Premium Account", 150, AccountType.Premium, -200, -50, true)]
        [TestCase("33333", "Premium Account", 100, AccountType.Premium, -650, -550, false)]

        public void BasicAccountWithdrawRulesTest(string accountNumber, string name, decimal balance, AccountType accountType,
        decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdraw;

            BasicAccountWithdrawRule withdrawRule = new BasicAccountWithdrawRule();

            withdraw = withdrawRule;

            Account account = new Account();

            accountNumber = account.AccountNumber;
            name = account.Name;
            balance = account.Balance;
            accountType = account.Type;
            amount = account.Balance;

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);

            expectedResult = response.Success;

            Assert.AreEqual(expectedResult, response.Success);
        }

    }
}
