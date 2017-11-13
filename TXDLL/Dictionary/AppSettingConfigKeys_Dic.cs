using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXDLL.Dictionary
{
    /// <summary>
    /// 在appsetting里面可以配置的项的key
    /// </summary>
    public class AppSettingConfigKeys_Dic
    {
        /// <summary>
        /// 数据库类型 数字 Oracle=1,SqlServer=2,MySQL=3,Access=4
        /// </summary>
        public const string DatabaseType = "DatabaseType";
        /// <summary>
        /// 默认的数据库连接字符串
        /// </summary>
        public const string DefaultConnStr = "DefaultConnStr";
        /// <summary>
        /// 连接超时时间。单位毫秒
        /// </summary>
        public const string TimeOut = "TimeOut";
    }
}
