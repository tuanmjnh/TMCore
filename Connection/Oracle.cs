using System;
using System.Collections.Generic;
using System.Text;
using Oracle.ManagedDataAccess.Client;
namespace TM.Connection {
    public class Oracle {
        public OracleConnection Connection;
        public Oracle(string ConnectionString = "HNIVNPTBACKAN1") {
            Connection = new OracleConnection(TM.Helper.TMAppContext.configuration.GetSection($"ConnectionStrings:{ConnectionString}").Value);
            Connection.Open();
        }
        public void Close() {
            try {
                if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
            } catch (Exception) { throw; }
        }
    }
}