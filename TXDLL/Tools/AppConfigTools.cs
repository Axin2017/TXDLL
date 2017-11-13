using System;
using System.Configuration;

namespace TXDLL.Tools
{
    /// <summary>
    /// 程序配置文件操作工具
    /// </summary>
    public class AppConfigTools
    {
        /// <summary>
        /// 从AppConfig中获取值,不存在则返回""
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettingString(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.HasFile)
            {
                return string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]) ? "" : ConfigurationManager.AppSettings[key];
            }
            return string.Empty;
        }

        /// <summary>
        /// 更改AppSetting中的值，如果没有就新插入
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool UpdateOrInsertAppSettings(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (!config.HasFile)
            {
                throw new ArgumentException("程序配置文件缺失！");
            }
            KeyValueConfigurationElement _key = config.AppSettings.Settings[key];
            if (_key == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }
            config.Save(ConfigurationSaveMode.Modified);
            return true;
        }

        public static string GetConnectString(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.HasFile)
            {
                if (ConfigurationManager.ConnectionStrings[key]!=null)
                {
                    return ConfigurationManager.ConnectionStrings[key].ConnectionString;
                }
            }
            return string.Empty;
        }
    }

}
