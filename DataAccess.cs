using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DatabaseCommon
{
    public static class DataAccess
    {
        public static DataTable GetDataTable(
            string serverName,
            string databaseName,
            string sql
        )
        {
            string connectionString = GetConnectionString(serverName, databaseName);
            var returnDataset = new DataSet();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    var dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(returnDataset);
                }
            }
            return returnDataset.Tables[0];
        }

        private static string GetConnectionString(
            string serverName,
            string databaseName
        )
        {
            return GetConnectionString(serverName, databaseName, string.Empty, string.Empty);
        }

        public static string GetConnectionString(
            string serverName,
            string databaseName,
            string password,
            string login
            //,bool multipleActiveResultSets
       )
        {
            string connectionString = "server=" + serverName + ";database=" + databaseName + ";";
            // If a password is provided, add login and password to connection string
            // Otherwise, use Windows authentication
            if (password.Length == 0)
            {
                connectionString += "integrated security=sspi;";
            }
            else
            {
                connectionString += "User ID=" + login + ";password=" + password + ";";
            }
            //if (multipleActiveResultSets)
            //{
            //    connectionString += "MultipleActiveResultSets=True;";
            //}
            return connectionString;
        }
    }
}
