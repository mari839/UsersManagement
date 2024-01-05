using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Application.Contracts;

namespace UsersManagement.Infrastructure
{
    internal sealed class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection CreateConnection()
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("UsersConnectionString"));
            connection.Open();
            return connection;
        }
    }
}
