using Azure.Core;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Application.Contracts;

namespace UsersManagement.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public AccountRepository( IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<Decimal> GetAmountByAccountNumber(string AccountNum)
        {
            await using SqlConnection sqlConnection = _dbConnectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_GetAmountByAccountNumber", sqlConnection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserInputAccountNumber", AccountNum);

            SqlParameter outputParameter = new SqlParameter();
            outputParameter.ParameterName = "@Amount";
            outputParameter.SqlDbType = System.Data.SqlDbType.Decimal;
            outputParameter.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(outputParameter);

            cmd.ExecuteNonQuery();
            if(outputParameter.Value != DBNull.Value)
            {
                return (Decimal)outputParameter.Value;
            }
            
            return 0;
        }


        public async Task Transfer(string accountFrom, string accountTo, int amount)
        {
            await using SqlConnection sqlConnection = _dbConnectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_TransferMoneyToSomeone", sqlConnection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SourceAccountNumber", accountFrom);
            cmd.Parameters.AddWithValue("@DestinationAccountNumber", accountTo);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.ExecuteNonQuery();
        }


        public async Task Withdraw(string accountFrom, int amount)
        {
            await using SqlConnection sqlConnection = _dbConnectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_WithdrawMoneyFromAccount", sqlConnection);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserInputAccountNumber", accountFrom);
            cmd.Parameters.AddWithValue("@WithdrawalAmount", amount);
            cmd.ExecuteNonQuery();
        }

        public async Task Deposit(string accountFrom, int amount)
        {
            await using SqlConnection sqlConnection = _dbConnectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_DepositMoney", sqlConnection);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserInputAccountNumber", accountFrom);
            cmd.Parameters.AddWithValue("@DepositAmount", amount);
            cmd.ExecuteNonQuery();
        }
    }
}
