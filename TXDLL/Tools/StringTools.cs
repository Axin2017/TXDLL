using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXDLL.Tools
{
    public class StringTools
    {
        /// <summary>
        /// 在一个字符串截取两个关键词之间的字符串
        /// </summary>
        /// <param name="originalStr">源字符串</param>
        /// <param name="startStr">截取开始的字符串</param>
        /// <param name="endStr">截取结束的字符串</param>
        /// <returns>List<string> default=null</returns>
        public static List<string> CutStrBetweenTwoWordsOut(string originalStr,string startKey,string endKey)
        {
            List<string> stringList = new List<string>();
            while (originalStr.Contains(startKey) && originalStr.Contains(endKey) && originalStr.IndexOf(endKey)> originalStr.IndexOf(startKey))
            {
                int startIndex = originalStr.IndexOf(startKey);
                int endIndex = originalStr.IndexOf(endKey);
                stringList.Add(originalStr.Substring(startIndex+startKey.Length,endIndex-startIndex-startKey.Length));
                originalStr = originalStr.Substring(endIndex+endKey.Length);
            }
            return stringList;
        }
    }
}
