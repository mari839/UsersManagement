using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Application.Contracts;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Application.Features.Users.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IConfiguration _configuration;
        public CreateUserHandler(IConfiguration configuration, IDbConnectionFactory dbConnectionFactory) 
        {
            _configuration = configuration;
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var token = "";
            if(request != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim("first_name", request.FirstName),
                    new Claim("last_name", request.FirstName),
                    new Claim("password", request.Password),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                token = CreateToken(claims);
            }


            Random random = new Random();
            string num = new string(Enumerable.Repeat("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 22).Select(s => s[random.Next(s.Length)]).ToArray());
            string amount = new string(Enumerable.Repeat("0123456789", 3).Select(s => s[random.Next(s.Length)]).ToArray());


            await using SqlConnection sqlConnection = _dbConnectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_AddUsers", sqlConnection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", request.FirstName);
            cmd.Parameters.AddWithValue("@LastName", request.LastName);
            cmd.Parameters.AddWithValue("@Email", request.Email);
            cmd.Parameters.AddWithValue("@Password", request.Password);
            cmd.Parameters.AddWithValue("@AccountNumber", num);
            cmd.Parameters.AddWithValue("@Amount", amount);

            cmd.ExecuteNonQuery();

            return token;
        }


        private string CreateToken(List<Claim> claims)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(_configuration.GetSection("JwtSettings:Issuer").Value, _configuration.GetSection("JwtSettings:Audience").Value, claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
