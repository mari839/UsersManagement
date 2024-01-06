using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Application.Contracts;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Features.Accounts.Commands.NewFolder
{
    public class TransferToSomeoneHandler : IRequestHandler<TransferToSomeoneCommand, Decimal>
    {
        private readonly IAccountRepository _accountRepository;
        public TransferToSomeoneHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Decimal> Handle(TransferToSomeoneCommand request, CancellationToken cancellationToken)
        {

            var amountFrom = await _accountRepository.GetAmountByAccountNumber(request.AccountFrom);
            if (amountFrom > request.Amount)
            {
                await _accountRepository.Transfer(request.AccountFrom, request.AccountTo, request.Amount);
            }
            amountFrom = await _accountRepository.GetAmountByAccountNumber(request.AccountFrom);
            return amountFrom;
        }
    }
}
