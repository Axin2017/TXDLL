using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXDLL.Data.Enums;
using TXDLL.Data.Imp;
using TXDLL.Data.Interface;
using TXDLL.Tools;

namespace TXDLL.Data
{
    public class DBFactory
    {
        /// <summary>
        /// 默认oracle
        /// </summary>
        /// <param name="connectStr"></param>
        /// <returns></returns>
        public static IBaseDBOprator GetDBOprator()
        {
            string databaseType = AppConfigTools.GetAppSettingString("DatabaseType");
            int tp = 1;//默认oracle
            if (!string.IsNullOrEmpty(databaseType))
            {
                int.TryParse(databaseType, out tp);
            }
            return GetDBOprator((DataBaseType)tp);
        }

        public static IBaseDBOprator GetDBOprator(string connectStr)
        {
            string databaseType = AppConfigTools.GetAppSettingString("DatabaseType");
            int tp = 1;//默认oracle
            if (string.IsNullOrEmpty(databaseType))
            {
                int.TryParse(databaseType, out tp);
            }
            return GetDBOprator((DataBaseType)tp,connectStr);
        }
        /// <summary>
        /// 选择数据库类型
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static IBaseDBOprator GetDBOprator(DataBaseType dbType)
        {
            IBaseDBOprator db = null;
            switch(dbType)
            {
                case DataBaseType.Oracle:
                    db = new OracleDBOpretor();
                    break;
                case DataBaseType.SqlServer:
                    break;
                case DataBaseType.MySQL:
                    break;
                case DataBaseType.Access:
                    break;
                default:
                    db = new OracleDBOpretor();
                    break;
            }
            return db;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectStr"></param>
        /// <returns></returns>
        public static IBaseDBOprator GetDBOprator(DataBaseType dbType,string connectStr)
        {
            IBaseDBOprator db = null;
            switch (dbType)
            {
                case DataBaseType.Oracle:
                    db = new OracleDBOpretor(connectStr);
                    break;
                case DataBaseType.SqlServer:
                    break;
                case DataBaseType.MySQL:
                    break;
                case DataBaseType.Access:
                    break;
                default:
                    db = new OracleDBOpretor(connectStr);
                    break;
            }
            return db;
        }
    }
}
