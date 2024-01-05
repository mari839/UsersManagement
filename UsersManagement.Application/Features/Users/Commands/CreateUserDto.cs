using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Features.Users.Commands
{
    
    public class CreateUserDto
    {
        [JsonConstructor]
        public CreateUserDto() { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
