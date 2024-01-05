using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<CreateUserDto>
    {
        public CreateUserCommand(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName; 
            Email = email; 
            Password = password;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
