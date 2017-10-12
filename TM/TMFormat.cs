using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Format
{
    public class Location
    {
        public enum location
        {
            viVN = 0x042A,
            enUS = 0x0409,
            frFR = 0x040C,
            jaJP = 0x0411,
            svSE = 0x041D,
            ruRU = 0x0419,
            daDK = 0x0406
        }
        public const string vi = "vi-VN";
        public const string en = "en-US";
        public const string fr = "fr-FR";
        public const string jp = "ja-JP";
        public const string sv = "sv-SE";
        public const string ru = "ru-RU";
        public const string da = "da-DK";
    }
    public static class Formating
    {
        public static System.Globalization.NumberFormatInfo FormatNumber(string location = Location.vi)
        {
            return new System.Globalization.CultureInfo(location).NumberFormat;
        }
        public static System.Globalization.CultureInfo CultureInfo(string location = Location.vi)
        {
            switch (location)
            {
                case "vi": location = Location.vi; break;
                case "en": location = Location.en; break;
                case "fr": location = Location.fr; break;
                case "jp": location = Location.jp; break;
                case "sv": location = Location.sv; break;
                case "ru": location = Location.ru; break;
                case "da": location = Location.da; break;
                default: location = Location.vi; break;
            }
            var ci = new System.Globalization.CultureInfo(location);
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = ci;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = ci;
            //System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            return ci;
        }
        public static string Currency(this decimal d, string currency)
        {
            return d.Equals(Decimal.Truncate(d)) ? d.ToString("0 " + currency) : d.ToString("0.00 " + currency);
        }
        public static string CurrencyVN(this decimal d)
        {
            return Currency(d, "₫");
        }
        public static string CurrencyFR(this decimal d)
        {
            return Currency(d, "€");
        }
        public static System.Globalization.NumberFormatInfo NumberFormat(string CultureInfoCode)
        {
            var myNumberFormatInfo = new System.Globalization.CultureInfo(CultureInfoCode).NumberFormat;
            return myNumberFormatInfo;
        }
        public static System.Globalization.NumberFormatInfo NumberFormatEN()
        {
            return NumberFormat("en-US");
        }
        public static System.Globalization.NumberFormatInfo NumberFormatVN()
        {
            return NumberFormat("vi-VN");
        }
        public static string ToCurrency(this decimal d, System.Globalization.NumberFormatInfo NumberFormat, int digits)
        {
            return d.ToString("C" + digits.ToString(), NumberFormat);
        }
        public static string ToCurrency(this decimal d, System.Globalization.NumberFormatInfo NumberFormat)
        {
            return d.ToString("C", NumberFormat);
        }
        public static string ToCurrencyVN(this decimal d, int digits)
        {
            return ToCurrency(d, NumberFormatVN(), digits);
        }
        public static string ToCurrencyVN(this decimal d)
        {
            return ToCurrency(d, NumberFormatVN());
        }
        public static string ToCurrencyEN(this decimal d, int digits)
        {
            return ToCurrency(d, NumberFormatEN(), digits);
        }
        public static string ToCurrencyEN(this decimal d)
        {
            return ToCurrency(d, NumberFormatEN());
        }
        public static DateTime DateTimeNow(string CultureInfo)
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(CultureInfo);
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo(CultureInfo);
            return DateTime.Now;
        }
        public static DateTime DateTimeNow()
        {
            return DateTimeNow("vi-VN");
        }
        public static DateTime DateTimeParseExactVNToVN(this string s)
        {
            return DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", CultureInfo());
        }
        public static DateTime DateTimeParseExactVNToVNHours(this string s)
        {
            return DateTime.ParseExact(s, "dd/MM/yyyy hh:mm:ss tt", CultureInfo());
        }
        public static DateTime DateParseExactVNToVN(this string s)
        {
            return DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo());
        }
        public static DateTime DateTimeParseExactVNToEN(this string s)
        {
            return DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", CultureInfo(Location.en));
        }
        public static DateTime DateTimeParseExactVNToENHours(this string s)
        {
            return DateTime.ParseExact(s, "dd/MM/yyyy hh:mm:ss tt", CultureInfo(Location.en));
        }
        public static DateTime DateParseExactVNToEN(this string s)
        {
            return DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo(Location.en));
        }
        public static DateTime DateTimeParseExactENToVN(this string s)
        {
            return DateTime.ParseExact(s, "MM/dd/yyyy HH:mm:ss", CultureInfo());
        }
        public static DateTime DateTimeParseExactENToVNHours(this string s)
        {
            return DateTime.ParseExact(s, "MM/dd/yyyy hh:mm:ss tt", CultureInfo());
        }
        public static DateTime DateParseExactENToVN(this string s)
        {
            return DateTime.ParseExact(s, "MM/dd/yyyy", CultureInfo());
        }
        public static DateTime StringToShortDatetime(this string date, char split)
        {
            try
            {
                var tmp = date.Trim().Split(split);
                return new DateTime(int.Parse(tmp[2]), int.Parse(tmp[1]), int.Parse(tmp[0]));
            }
            catch (Exception) { throw; }
            //var ci = new System.Globalization.CultureInfo("en-US");
            //ci.DateTimeFormat.SetAllDateTimePatterns(new string[] { "dd/MM/yyyy" }, 'd');
            //System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
        }
        public static DateTime StringToShortDatetime(this string date)
        {
            return StringToShortDatetime(date, '/');
        }
        public static DateTime StringToDatetime(this string date, char split)
        {
            return StringToShortDatetime(date, split).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
        }
        public static DateTime StringToDatetime(this string date)
        {
            return StringToDatetime(date, '/');
        }
        public static DateTime StartOfDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }
        public static DateTime EndOfDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }
    }
}

