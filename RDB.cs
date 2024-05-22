using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressCheck
{
    internal static class RDB
    {
        private static SqlConnection? _connection;

        public static SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {

                    var connectionString = @"Data Source=localhost\SQLEXPRESS;"
                                    + "Initial Catalog=stress_check;"
                                    + "Persist Security Info=False;"
                                    + "Integrated Security=SSPI;"
                                    + "Encrypt=False;";


                    try
                    {
                        _connection = new SqlConnection(connectionString);
                        _connection.Open();
                    }
                    catch (SqlException ex)
                    {
                        ErrorMessage(ex);
                        Environment.Exit(ex.ErrorCode);
                    }
                }
                return _connection;
            }
        }

        public static void Disconnect()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }

        public static void ErrorMessage(SqlException ex)
        {
            var errorMessages = new StringBuilder();
            for (int i = 0; i < ex.Errors.Count; i++)
            {
                errorMessages.Append($"Index #{i}\n"
                                    + $"Server: {ex.Errors[i].Server}\n"
                                    + $"Message: {ex.Errors[i].Message}\n"
                                    + $"LineNumber: {ex.Errors[i].LineNumber}\n"
                                    + $"Source: {ex.Errors[i].Source}\n"
                                    + $"Procedure: {ex.Errors[i].Procedure}\n");

            }
            MessageBox.Show( errorMessages.ToString(), "SQLエラー" );
        }
    }
}
