using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Application.Features.Accounts.Queries
{
    public class GetAmountQuery : IRequest<Decimal>
    {
        public string AccountNumber { get; set; }
    }
}
