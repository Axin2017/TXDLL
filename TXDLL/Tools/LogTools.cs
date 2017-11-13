using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TXDLL.Tools
{
    /// <summary>
    /// 日志操作工具
    /// </summary>
    public class LogTools
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <param name="logType">日志类型，将会把日志放到类型文件夹下</param>
        public static void WriteLog(string content, string logType)
        {
            WriteLog(content, logType, "", "");
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <param name="logType">日志类型，将会把日志放到类型文件夹下</param>
        /// <param name="customPath">自定义路径，如果为空，则默认当前程序的根目录下Log文件夹内</param>
        /// <param name="customName">自定义日志名，否则为“年-月-日.txt”</param>
        public static void WriteLog(string content, string logType, string customPath,string customName)
        {
            try
            {
                DateTime d = DateTime.Now;
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\";
                string logPath = filePath + logType + "\\";
                if (!string.IsNullOrEmpty(customPath))
                {
                    logPath = customPath+"\\";
                }
                if (!Directory.Exists(logPath))
                    Directory.CreateDirectory(logPath);
                string fileName = d.Year + "-" + d.Month + "-" + d.Day + ".txt";

                string time = d.ToString("yyyy-MM-dd HH：mm：ss");
                content = time + "-" + logType + "\r\n" + content + "\r\n";
                FileTools.WriteTextToFile(content, logPath + fileName, true);
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 写windows事件日志,注意：b/s程序不能直接使用
        /// </summary>
        /// <param name="logName"></param>
        /// <param name="SourceName"></param>
        /// <param name="LogText"></param>
        /// <param name="type"></param>
        public static void WriteEventLog(string logName, string SourceName, string LogText, EventLogEntryType type)
        {
            EventLog el = new EventLog();
            try
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    if (EventLog.Exists(logName))
                    {
                        el.Log = logName;
                    }
                    else
                    {
                        EventLog.CreateEventSource(SourceName, logName);
                    }
                }
                el.Source = SourceName;
                el.WriteEntry(LogText, type);
            }
            catch (Exception ex)
            {
                el.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
