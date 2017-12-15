using System;
using System.Collections.Generic;
using TXDLL.Data.Enums;
using TXDLL.Data.Interface;

namespace TXDLL.Data
{
    internal class DbTypeRegInfo
    {
        public string ConnectStr { get; set; }
        public DataBaseType DBType { get; set; }
        public Type DBInstanceType { get; set; }
        public DbTypeRegInfo(DataBaseType DBType, Type DBInstanceType, string ConnectStr)
        {
            this.DBType = DBType;
            this.DBInstanceType = DBInstanceType;
            this.ConnectStr = ConnectStr;
        }
    }
    public class DBFactory
    {

        private static Dictionary<DataBaseType, DbTypeRegInfo> DataBaseTypeRegDic = new Dictionary<DataBaseType, DbTypeRegInfo>();

        private static DbTypeRegInfo CurrentDataBase { get; set; }

        /// <summary>
        /// 注册相应的数据库类型
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="classType"></param>
        public static void RegisterDataBaseType(DataBaseType dbType, Type classType, string connectStr, bool setDefault)
        {
            DbTypeRegInfo dbInfo = new DbTypeRegInfo(dbType, classType, connectStr);
            if (classType is IBaseDBOprator)
            {
                DataBaseTypeRegDic.Add(dbType, dbInfo);
                if (setDefault)
                {
                    CurrentDataBase = dbInfo;
                }
            }
            else
            {
                throw new TypeLoadException("The param 'classType' must be extended from IBaseDBOprator");
            }
        }
        /// <summary>
        /// 默认oracle
        /// </summary>
        /// <param name="connectStr"></param>
        /// <returns></returns>
        public static IBaseDBOprator GetDBOprator()
        {
            return GetDBOprator(CurrentDataBase.DBType, CurrentDataBase.ConnectStr);
        }

        /// <summary>
        /// 选择数据库类型
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static IBaseDBOprator GetDBOprator(DataBaseType dbType)
        {

            return GetDBOprator(dbType, DataBaseTypeRegDic[dbType].ConnectStr);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectStr"></param>
        /// <returns></returns>
        public static IBaseDBOprator GetDBOprator(DataBaseType dbType, string connectStr)
        {
            IBaseDBOprator db = null;
            if (DataBaseTypeRegDic[dbType] != null)
            {
                db = Activator.CreateInstance(DataBaseTypeRegDic[dbType].DBInstanceType, new object[] { connectStr }) as IBaseDBOprator;
            }
            return db;
        }
    }
}
