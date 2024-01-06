using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Application.Contracts;

namespace UsersManagement.Application.Features.Accounts.Commands.Deposit
{
    public class DepositAmountHandler : IRequestHandler<DepositAmountCommand, Decimal>
    {
        private readonly IAccountRepository _accountRepository;
        public DepositAmountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Decimal> Handle(DepositAmountCommand request, CancellationToken cancellationToken)
        {
            await _accountRepository.Deposit(request.AccountNumber, request.Amount);

            Decimal amount = await _accountRepository.GetAmountByAccountNumber(request.AccountNumber);
            
            return amount;
        }
    }
}
