using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Application.Contracts
{
    public interface IAccountRepository
    {
        Task<Decimal> GetAmountByAccountNumber(string AccountNum);

        Task Transfer(string accountFrom, string accountTo, int amount);

        Task Withdraw(string accountFrom, int amount);

        Task Deposit(string accountFrom, int amount);
    }
}
