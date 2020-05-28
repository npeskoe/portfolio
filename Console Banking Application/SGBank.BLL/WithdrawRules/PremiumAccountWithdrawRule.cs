using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL.WithdrawRules
{
    public class PremiumAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            if (account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Message = "Error: A non-basic account hit the Basic Withdraw Rule. Contact IT.";
                return response;
            }
            if (amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdrawl amounts must be negative!";
                return response;
            }
            if (account.Balance + amount < (-500))
            {
                response.Success = false;
                response.Message = "Premium accounts can only overdraft up to $500 without a processing fee!";
                return response;
            }
            else
            {
                response.OldBalance = account.Balance;
                account.Balance += amount;
                response.Account = account;
                response.Amount = amount;
                response.Success = true;

                return response;
            }
        }
    }
}
