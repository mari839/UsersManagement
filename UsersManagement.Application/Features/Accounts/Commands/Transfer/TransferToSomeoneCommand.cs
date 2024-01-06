using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Application.Features.Accounts.Commands.NewFolder
{
    public class TransferToSomeoneCommand : IRequest<Decimal>
    {

        public string AccountFrom { get; set; }
        public string AccountTo { get; set; }
        public int Amount { get; set; }
    }
}
