using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Utility.Security.Extensions
{
#if !(NET20 || NET30 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)

    public static class BaiduSecurityExtensions
    {
        /// <summary>
        /// 字符串unicode转换
        /// <para>
        /// 不支持 netstandard 1.0 - 1.2
        /// </para>
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string Unicode2String(this string source)
        {
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, x => Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)).ToString());
#else
             return string.Empty;
#endif
        }
        public static string GetG(this string t, string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < t.Length; i++)
            {
                string c = Convert.ToString((int)t[i] ^ s[i], 16);
                sb.Append(c.Length > 1 ? c : "0" + c);
            }
            return sb.ToString();
        }
        public static byte[] HexStringToByteArray(this string s)
        {
            s = s.Replace("_", "0").Replace(" ", "0");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                try
                {
                    string x = s.Substring(i, 2);
                    byte b = Convert.ToByte(x, 16);
                    buffer[i / 2] = b;
                }
                catch (Exception e)
                {
                    throw new Exception("16进制转换异常", e);
                }
            return buffer;
        }
        public static string ByteArrayToHexString(this byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                //16进制数字
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
                //16进制数字之间以空格隔开
                //sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            }
            return sb.ToString().ToUpper();
        }

        public static string TenParseTenSix(this string str)
        {
            StringBuilder sbAppend = new StringBuilder();
            foreach (var item in str)
            {
                sbAppend.Append(Convert.ToString((int)item, 16));
            }
            return sbAppend.ToString();
        }
        // private const string AES_IV = "00000000000000000";//16位  
        public static string GetAES_IV()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 128; i++)
            {
                sb.Append("0");
            }
            return sb.ToString();
        }

        /// <summary>  AES加密算法</summary>  
        public static string EncryptByAES(this string input, string key)
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                byte[] keyBytes = new byte[32];
                aesAlg.BlockSize = 128;
                aesAlg.FeedbackSize = 128;
                Array.Copy(HexStringToByteArray(key), keyBytes, 32);
                aesAlg.Key = keyBytes;
                byte[] buffer = new byte[16];
                Array.Copy(HexStringToByteArray(GetAES_IV()), buffer, 16);
                aesAlg.IV = buffer;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.Zeros;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                byte[] bs = Encoding.UTF8.GetBytes(input);
                return encryptor.TransformFinalBlock(bs, 0, bs.Length).ByteArrayToHexString();
            }
        }
        /// <summary>AES解密 </summary>
        public static string DecryptByAES(this string input, string key)
        {
            byte[] inputBytes = input.HexStringToByteArray();
            byte[] buffer = new byte[inputBytes.Length];
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 32));
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.BlockSize = 128;
                aesAlg.FeedbackSize = 128;
                aesAlg.Key = keyBytes;
                byte[] buff = new byte[16];
                Array.Copy(HexStringToByteArray(GetAES_IV()), buff, 16);
                aesAlg.IV = buff;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.Zeros;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream(inputBytes))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read))
                    {
                        csEncrypt.Read(buffer, 0, buffer.Length);
                        return Encoding.UTF8.GetString(buffer).Replace("\0", "");
                    }
                }
            }
        }
    }
#endif
}
