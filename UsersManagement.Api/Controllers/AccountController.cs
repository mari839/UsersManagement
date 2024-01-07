using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Application.Features.Accounts.Commands.Deposit;
using UsersManagement.Application.Features.Accounts.Commands.NewFolder;
using UsersManagement.Application.Features.Accounts.Commands.Withdraw;
using UsersManagement.Application.Features.Accounts.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsersManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<AccountController>
        [HttpGet]
        public async Task<ActionResult<Decimal>> GetAmountFromAccount(string accNum)
        {
            var getAmountQuery = new GetAmountQuery() { AccountNumber = accNum };
            var res = await _mediator.Send(getAmountQuery);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<AccountController>
        [HttpPost("transfer")]
        public async Task<ActionResult<Decimal>> TransferMoney(TransferToSomeoneCommand transferToSomeoneCommand)
        {
            var res = await _mediator.Send(transferToSomeoneCommand);
            return Ok(res);
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult<Decimal>> WithdrawMoney(WithdrawAmountCommand withdrawAmountCommand)
        {
            var res = await _mediator.Send(withdrawAmountCommand);
            return Ok(res);
        }

        [HttpPost("deposit")]
        public async Task<ActionResult<Decimal>> DepositMoney(DepositAmountCommand depositAmountCommand)
        {
            var res = await _mediator.Send(depositAmountCommand);
            return Ok(res);
        }

    }
}
