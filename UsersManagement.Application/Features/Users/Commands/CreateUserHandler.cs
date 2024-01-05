using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Application.Contracts;

namespace UsersManagement.Application.Features.Users.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserDto>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        
        public CreateUserHandler(IDbConnectionFactory dbConnectionFactory) 
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<CreateUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            CreateUserDto userDto = new CreateUserDto()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
            };

            await using SqlConnection sqlConnection = _dbConnectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("spAddUser", sqlConnection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", userDto.FirstName);
            cmd.Parameters.AddWithValue("@LastName", userDto.LastName);
            cmd.Parameters.AddWithValue("@Email", userDto.Email);
            cmd.Parameters.AddWithValue("@Password", userDto.Password);

            cmd.ExecuteNonQuery();

            return userDto;
            //Account should be created at the same time as account but user should enter any data
        }
    }
}
