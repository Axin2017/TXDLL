using System.IO;

namespace TXDLL.Tools
{
    /// <summary>
    /// 文件操作工具
    /// </summary>
    public class FileTools
    {
        /// <summary>
        /// 以特定的编码读取文本
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ReadTextFromFile(string filepath, System.Text.Encoding encoding)
        {
            string str1 = "";
            using (StreamReader sr = new StreamReader(filepath, encoding))
            {
                str1 = sr.ReadToEnd();
            }
            string businessData = File.ReadAllText(filepath);
            return str1;
        }

        /// <summary>
        /// 写字符串进文件
        /// </summary>
        /// <param name="text">要写进去的内容</param>
        /// <param name="filePath">文件相对地址</param>
        /// <param name="isAppend">是否追加，不是的话就直接替换</param>
        public static void WriteTextToFile(string text, string filePath, bool isAppend)
        {

            FileStream fs = null;
            if (isAppend)
            {
                fs = new FileStream(filePath, FileMode.Append);
            }
            else
            {
                fs = new FileStream(filePath, FileMode.Create);
            }
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes(text);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }
    }
}
