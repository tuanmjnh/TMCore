namespace TM.Connection {
    public class OleDBF {
        private string _dataSource = "MainContext";
        //public OleDbConnection Connection;
        public static string GetConnectionString(string DataSource, string version = "VFPOLEDB") {
            if (version == "dBASE" || version == "1")
                return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataSource + ";Extended Properties=dBASE IV";
            else if (version == "MS" || version == "2")
                return @"Driver={Microsoft dBASE Driver (*.dbf)};DriverID=277;Dbq=" + DataSource + ";";
            else
                return "Provider=VFPOLEDB;Data Source=" + DataSource + ";Collating Sequence=machine;";
        }
        // public OleDBF(string DataSource, string version = "VFPOLEDB")
        // {
        //     _dataSource = DataSource;
        //     Connection = new OleDbConnection(GetConnectionString(_dataSource, version));
        //     Connection.Open();
        // }
        // public void Close()
        // {
        //     try
        //     {
        //         if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
        //             Connection.Close();
        //     }
        //     catch (Exception) { throw; }
        // }
    }
}