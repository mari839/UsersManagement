using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Application.Contracts;

namespace UsersManagement.Application.Features.Accounts.Commands.Withdraw
{
    public class WithdrawAmountHandler : IRequestHandler<WithdrawAmountCommand, Decimal>
    {
        private readonly IAccountRepository _accountRepository;
        public WithdrawAmountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Decimal> Handle(WithdrawAmountCommand request, CancellationToken cancellationToken)
        {
            var current = await _accountRepository.GetAmountByAccountNumber(request.AccountNumber);
            if (current > request.Amount)
            {
                await _accountRepository.Withdraw(request.AccountNumber, request.Amount);
            }
            current = await _accountRepository.GetAmountByAccountNumber(request.AccountNumber);
            return current;
        }
    }
}
