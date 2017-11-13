using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TXDLL.Tools
{
    /// <summary>
    /// MD5工具
    /// </summary>
    public class MD5Tools
    {
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string originalStr)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(originalStr)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <param name="XType">X或者x</param>
        /// <returns></returns>
        public static string MD5Encrypt32(string originalStr,string XType)
        {
            if (XType!="x" && XType!="X")
            {
                XType = "x";
            }
            string cl = originalStr;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString(XType);
            }
            return pwd;
        }
        /// <summary>   
        /// MD5加密
        /// </summary>
        /// <param name="originalStr">源字符串</param>
        /// <param name="sKey">密钥</param>
        /// <param name="XType">X或者x</param>
        /// <returns>string</returns>
        public static string MD5Encode(string originalStr, string sKey,string XType)
        {
            if (XType != "x" && XType != "X")
            {
                XType = "x";
            }
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(originalStr);
            des.Key = Encoding.ASCII.GetBytes(sKey);
            des.IV = Encoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            string format = "{0:" + XType + "2";
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat(format, b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// MD5解密
        /// </summary>
        /// <param name="resultStr">加密字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns>string</returns>
        public static string MD5Decode(string resultStr, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[resultStr.Length / 2];
            for (int x = 0; x < resultStr.Length / 2; x++)
            {
                int i = (Convert.ToInt32(resultStr.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = Encoding.ASCII.GetBytes(sKey);    
            des.IV = Encoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();

            return Encoding.Default.GetString(ms.ToArray());
        }
    }
}
