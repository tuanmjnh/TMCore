using System;
using System.Collections.Generic;
using System.Text;

namespace TM.Connection {
    public class SQLServer {
        private string _connectionString = "MainConnection";
        public System.Data.SqlClient.SqlConnection Connection;
        public SQLServer() {
            Connection = new System.Data.SqlClient.SqlConnection(TM.Helper.TMAppContext.configuration.GetSection($"ConnectionStrings:{_connectionString}").Value);
            Connection.Open();
        }
        public SQLServer(string connectionString) {
            _connectionString = connectionString;
            Connection = new System.Data.SqlClient.SqlConnection(TM.Helper.TMAppContext.configuration.GetSection($"ConnectionStrings:{_connectionString}").Value);
            Connection.Open();
        }
        public void Close() {
            try {
                if (Connection != null && Connection.State == System.Data.ConnectionState.Open) {
                    Connection.Close();
                    Connection.Dispose();
                }
            } catch (Exception) { throw; }
        }
    }
}