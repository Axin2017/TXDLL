using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXDLL.Tools
{
    /// <summary>
    /// 与系统有关的一些工具方法
    /// </summary>
    public class SystemTools
    {
        /// <summary>
        /// 设置程序开机自动运行
        /// </summary>
        /// <param name="keyName">程序标识</param>
        /// <param name="filePath">程序路径</param>
        /// <returns></returns>
        public static bool SetProgramAutoRun(string keyName, string filePath)
        {
            try
            {
                RegistryKey Local = Registry.LocalMachine;
                RegistryKey runKey = Local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\");
                runKey.SetValue(keyName, filePath);
                Local.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 取消程序开机自动运行
        /// </summary>
        /// <param name="keyName">程序标识</param>
        /// <returns></returns>
        public static bool CancelProgramAutoRun(string keyName)
        {
            try
            {
                RegistryKey Local = Registry.LocalMachine;
                RegistryKey runKey = Local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\");
                runKey.DeleteValue(keyName);
                Local.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
