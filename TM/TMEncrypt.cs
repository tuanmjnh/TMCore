using System;

namespace TM.Encrypt
{
    public class MD5
    {
        public static string CryptoMD5(string password)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                // Send a sample text to hash.
                var hashedBytes = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                var sBuilder = new System.Text.StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < hashedBytes.Length; i++)
                    sBuilder.Append(hashedBytes[i].ToString("x2"));

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
        public static string CryptoMD5TM(string password)
        {
            string TM = "&trade;";
            return CryptoMD5(password + TM);
        }
        public static string TMEncode(string str)
        {
            string[] arr = new string[] { "x8c", "q2z", "f2x", "67h", "hf9", "4aj", "llb", "ea2", "wr6", "bvz" };
            string[] chars = new string[] { "(", ")", ".", "," };
            string val = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].ToString() == "0") val += arr[0];
                if (str[i].ToString() == "1") val += arr[1];
                if (str[i].ToString() == "2") val += arr[2];
                if (str[i].ToString() == "3") val += arr[3];
                if (str[i].ToString() == "4") val += arr[4];
                if (str[i].ToString() == "5") val += arr[5];
                if (str[i].ToString() == "6") val += arr[6];
                if (str[i].ToString() == "7") val += arr[7];
                if (str[i].ToString() == "8") val += arr[8];
                if (str[i].ToString() == "9") val += arr[9];
            }
            return val;
        }
        public static string TMDecode(string str)
        {
            string[] arr = new string[] { "x8c", "q2z", "f2x", "67h", "hf9", "4aj", "llb", "ea2", "wr6", "bvz" };
            string val = "";
            for (int i = 0; i < str.Length - 2; i += 3)
                for (int j = 0; j < arr.Length; j++)
                    if (str[i].ToString() + str[i + 1].ToString() + str[i + 2].ToString() == arr[j]) val += j;
            return val;
        }
    }
}