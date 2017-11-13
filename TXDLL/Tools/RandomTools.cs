using System;

namespace TXDLL.Tools
{
    /// <summary>
    /// 随机数或者随机字符串工具
    /// </summary>
    public class RandomTools
    {
        /// <summary>
        /// 获得随机的特定长度的asc码 0-127字符串 
        /// </summary>
        /// <param name="count">字符串长度</param>
        /// <returns>string</returns>
        public static string GetRandomWord(int count)
        {
            string randomWords = string.Empty;
            Random r = new Random();
            for (int i = 1; i <= count; i++)
            {
                randomWords += (char)r.Next(127);
            }
            return randomWords;
        }

        /// <summary>
        /// 获得32位随机码
        /// </summary>
        /// <returns></returns>
        public static string GetUnid()
        {
            string unid = Guid.NewGuid().ToString();
            return unid;
        }
    }
}
