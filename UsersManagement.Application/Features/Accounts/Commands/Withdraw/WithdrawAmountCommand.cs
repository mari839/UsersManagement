using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Application.Features.Accounts.Commands.Withdraw
{
    public class WithdrawAmountCommand : IRequest<Decimal>
    {
        public string AccountNumber { get; set; }
        public int Amount { get; set; }
    }
}
