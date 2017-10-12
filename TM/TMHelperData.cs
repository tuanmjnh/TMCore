using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TM.Helper
{
    //public static class Data
    //{
    //    public static DataTable ToDataTable2<T>(this List<T> iList)
    //    {
    //        var dataTable = new DataTable();
    //        System.ComponentModel.PropertyDescriptorCollection propertyDescriptorCollection =
    //            System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));
    //        for (int i = 0; i < propertyDescriptorCollection.Count; i++)
    //        {
    //            System.ComponentModel.PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
    //            Type type = propertyDescriptor.PropertyType;

    //            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
    //                type = Nullable.GetUnderlyingType(type);
    //            dataTable.Columns.Add(propertyDescriptor.Name, type);
    //        }
    //        object[] values = new object[propertyDescriptorCollection.Count];
    //        foreach (T iListItem in iList)
    //        {
    //            for (int i = 0; i < values.Length; i++)
    //            {
    //                values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
    //            }
    //            dataTable.Rows.Add(values);
    //        }
    //        return dataTable;
    //    }
    //    public static DataTable LinqToDataTable<T>(this IEnumerable<T> varlist)
    //    {
    //        var dtReturn = new DataTable();

    //        // column names
    //        System.Reflection.PropertyInfo[] oProps = null;

    //        if (varlist == null) return dtReturn;

    //        foreach (T rec in varlist)
    //        {
    //            // Use reflection to get property names, to create table, Only first time, others will follow
    //            if (oProps == null)
    //            {
    //                oProps = ((System.Type)rec.GetType()).GetProperties();
    //                foreach (System.Reflection.PropertyInfo pi in oProps)
    //                {
    //                    System.Type colType = pi.PropertyType;

    //                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(System.Nullable<>)))
    //                        colType = colType.GetGenericArguments()[0];

    //                    dtReturn.Columns.Add(new System.Data.DataColumn(pi.Name, colType));
    //                }
    //            }
    //            DataRow dr = dtReturn.NewRow();

    //            foreach (System.Reflection.PropertyInfo pi in oProps)
    //                dr[pi.Name] = pi.GetValue(rec, null) == null ? System.DBNull.Value : pi.GetValue(rec, null);

    //            dtReturn.Rows.Add(dr);
    //        }
    //        return dtReturn;
    //    }
    //    public static DataTable ToDataTable<T>(this IList<T> data)
    //    {
    //        var props = System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));
    //        DataTable table = new DataTable();
    //        for (int i = 0; i < props.Count; i++)
    //        {
    //            var prop = props[i];
    //            table.Columns.Add(prop.Name, prop.PropertyType);
    //        }
    //        object[] values = new object[props.Count];
    //        foreach (T item in data)
    //        {
    //            for (int i = 0; i < values.Length; i++)
    //            {
    //                values[i] = props[i].GetValue(item);
    //            }
    //            table.Rows.Add(values);
    //        }
    //        return table;
    //    }
    //    public static DataTable ToDataTable<T>(this List<T> items)
    //    {
    //        DataTable dataTable = new DataTable(typeof(T).Name);

    //        //Get all the properties
    //        var Props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
    //        foreach (var prop in Props)
    //        {
    //            //Defining type of data column gives proper data table 
    //            var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
    //            //Setting column names as Property names
    //            dataTable.Columns.Add(prop.Name, type);
    //        }
    //        foreach (T item in items)
    //        {
    //            var values = new object[Props.Length];
    //            for (int i = 0; i < Props.Length; i++)
    //            {
    //                //inserting property values to datatable rows
    //                values[i] = Props[i].GetValue(item, null);
    //            }
    //            dataTable.Rows.Add(values);
    //        }
    //        //put a breakpoint here and check datatable
    //        return dataTable;
    //    }
    //    public static DataTable DynamicToDataTable<T>(this List<T> items)
    //    {
    //        var a = typeof(T);
    //        DataTable dataTable = new DataTable(typeof(T).Name);

    //        //Get all the properties
    //        System.Reflection.PropertyInfo[] Props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
    //        foreach (System.Reflection.PropertyInfo prop in Props)
    //        {
    //            //Defining type of data column gives proper data table 
    //            var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
    //            //Setting column names as Property names
    //            dataTable.Columns.Add(prop.Name, type);
    //        }
    //        foreach (T item in items)
    //        {
    //            var values = new object[Props.Length];
    //            for (int i = 0; i < Props.Length; i++)
    //            {
    //                //inserting property values to datatable rows
    //                values[i] = Props[i].GetValue(item, null);
    //            }
    //            dataTable.Rows.Add(values);
    //        }
    //        //put a breakpoint here and check datatable
    //        return dataTable;
    //    }
    //    public static List<T> ToList<T>(this IDataReader dr)
    //    {
    //        List<T> list = new List<T>();
    //        T obj = default(T);
    //        while (dr.Read())
    //        {
    //            obj = Activator.CreateInstance<T>();
    //            foreach (System.Reflection.PropertyInfo prop in obj.GetType().GetProperties())
    //                if (!object.Equals(dr[prop.Name], DBNull.Value))
    //                    prop.SetValue(obj, dr[prop.Name], null);
    //            list.Add(obj);
    //        }
    //        return list;
    //    }
    //    public static List<T> ToList<T>(this DataTable table) where T : class, new()
    //    {
    //        try
    //        {
    //            List<T> list = new List<T>();
    //            foreach (var row in table.AsEnumerable())
    //            {
    //                T obj = new T();
    //                foreach (var prop in obj.GetType().GetProperties())
    //                {
    //                    try
    //                    {
    //                        System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
    //                        propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
    //                    }
    //                    catch { continue; }
    //                }
    //                list.Add(obj);
    //            }
    //            return list;
    //        }
    //        catch { return null; }
    //    }
    //    public static List<T> ToList2<T>(this DataTable dt)
    //    {
    //        const System.Reflection.BindingFlags flags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;
    //        var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
    //        var objectProperties = typeof(T).GetProperties(flags);
    //        var targetList = dt.AsEnumerable().Select(dataRow =>
    //        {
    //            var instanceOfT = Activator.CreateInstance<T>();
    //            foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
    //                properties.SetValue(instanceOfT, dataRow[properties.Name], null);
    //            return instanceOfT;
    //        }).ToList();
    //        return targetList;
    //    }
    //}
    public static class Dictionary
    {
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> dicToAdd)
        {
            dicToAdd.ForEach(x => dic.Add(x.Key, x.Value));
        }
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        public static void ForEachOrBreak<T>(this IEnumerable<T> source, Func<T, bool> func)
        {
            foreach (var item in source)
            {
                bool result = func(item);
                if (result) break;
            }
        }
    }
    public static class Extra
    {
        public static Dictionary<int, string> day()
        {
            var rs = new Dictionary<int, string>();
            for (int i = 1; i < 32; i++)
                rs.Add(i, i.ToString());
            return rs;
        }
        public static Dictionary<int, string> day(string select)
        {
            var rs = day();
            rs.Add(0, select);
            return rs;
        }
        public static Dictionary<int, string> month()
        {
            var rs = new Dictionary<int, string>();
            for (int i = 1; i < 13; i++)
                rs.Add(i, i.ToString());
            return rs;
        }
        public static Dictionary<int, string> month(string select)
        {
            var rs = month();
            rs.Add(0, select);
            return rs;
        }
        public static Dictionary<int, string> year()
        {
            var rs = new Dictionary<int, string>();
            for (int i = 1900; i < DateTime.Now.Year; i++)
                rs.Add(i, i.ToString());
            return rs;
        }
        public static Dictionary<int, string> year(string select)
        {
            var rs = year();
            rs.Add(0, select);
            return rs;
        }
        public static string Random(int from, int to)
        {
            Random rd = new Random();
            int i = rd.Next(from, to);
            return TM.Encrypt.MD5.CryptoMD5(i.ToString() +
                DateTime.Now.Day.ToString() +
                DateTime.Now.Month.ToString() +
                DateTime.Now.Year.ToString() +
                DateTime.Now.Hour.ToString() +
                DateTime.Now.Minute.ToString() +
                DateTime.Now.Second.ToString() +
                DateTime.Now.Millisecond.ToString());
        }
        public static string Random()
        {
            return Random(6, 9);
        }
    }
}
