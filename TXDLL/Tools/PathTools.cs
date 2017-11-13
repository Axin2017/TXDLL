using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXDLL.Tools
{
    public class PathTools
    {
        /// <summary>
        /// 应用程序基目录
        /// </summary>
        public static string AppRootPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
    }
}
