using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Application.Contracts;

namespace UsersManagement.Application.Features.Accounts.Queries
{
    public class GetAmountQueryHandler : IRequestHandler<GetAmountQuery, Decimal>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAmountQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Decimal> Handle(GetAmountQuery request, CancellationToken cancellationToken)
        {
            var amount = await _accountRepository.GetAmountByAccountNumber(request.AccountNumber);

            return amount;

        }
    }
}
