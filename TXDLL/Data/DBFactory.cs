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
    /// <summary>
    /// 数据库工厂类
    /// </summary>
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
            if (classType.GetInterface("IBaseDBOprator")!=null)
            {
                if (!DataBaseTypeRegDic.ContainsKey(dbType))
                {
                    DataBaseTypeRegDic.Add(dbType, dbInfo);
                }else
                {
                    DataBaseTypeRegDic[dbType] = dbInfo;
                }
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
        /// get default DBOprator
        /// </summary>
        /// <returns></returns>
        public static IBaseDBOprator GetDBOprator()
        {
            if (CurrentDataBase==null)
            {
                throw new Exception("There is no default DBOprator,please regist first");
            }
            return GetDBOprator(CurrentDataBase.DBType, CurrentDataBase.ConnectStr);
        }

        /// <summary>
        /// get  DBOprator by DataBaseType
        /// </summary>
        /// <param name="dbType">DataBaseType</param>
        /// <returns></returns>
        public static IBaseDBOprator GetDBOprator(DataBaseType dbType)
        {
            if (!DataBaseTypeRegDic.ContainsKey(dbType))
            {
                throw new Exception(string.Format("There is no DataBaseType '{0}' finded,please regist first", dbType.ToString()));
            }
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

        #region 默认注册的数据库操作类
        /// <summary>
        /// 注册默认提供的oracle操作类
        /// </summary>
        /// <param name="connectStr"></param>
        /// <param name="setDefault"></param>
        public static void RegisterDefaultOracleDb(string connectStr, bool setDefault)
        {
            RegisterDataBaseType(DataBaseType.Oracle, typeof(Imp.OracleDBOpretor), connectStr, setDefault);
        }
        #endregion
    }
}
