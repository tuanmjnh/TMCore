using System;
using System.Collections.Generic;
using System.Text;
namespace TMCore.Connection
{
    public class OleExcel
    {
    //     private string _dataSource = "MainContext";
    //     public System.Data.Odbc.OleDbConnection Connection;
    //     public static string GetConnectionString(string DataSource)
    //     {
    //         var props = new System.Collections.Generic.Dictionary<string, string>();
    //         var ext = System.IO.Path.GetExtension(DataSource).ToLower();
    //         if (ext == ".xlsx")
    //         {
    //             // XLSX - Excel 2007, 2010, 2012, 2013
    //             props["Provider"] = "Microsoft.ACE.OLEDB.12.0";
    //             props["Extended Properties"] = "Excel 12.0 XML";
    //             props["Data Source"] = DataSource;
    //             //"Mode=ReadWrite;Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1;\""
    //         }
    //         else
    //         {
    //             // XLS - Excel 2003 and Older
    //             props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
    //             props["Extended Properties"] = "Excel 8.0";
    //             props["Data Source"] = DataSource;
    //         }
    //         StringBuilder sb = new StringBuilder();
    //         foreach (var prop in props)
    //         {
    //             sb.Append(prop.Key);
    //             sb.Append('=');
    //             sb.Append(prop.Value);
    //             sb.Append(';');
    //         }
    //         return sb.ToString();
    //     }
    //     public OleExcel(string DataSource)
    //     {
    //         _dataSource = DataSource;
    //         Connection = new OleDbConnection(GetConnectionString(_dataSource));
    //         Connection.Open();
    //     }
    //     public void Close()
    //     {
    //         try
    //         {
    //             if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
    //                 Connection.Close();
    //         }
    //         catch (Exception) { throw; }
    //     }
    }
}