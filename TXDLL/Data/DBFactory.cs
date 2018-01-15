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
        /// 注释数据库类型对应的操作类
        /// </summary>
        /// <param name="dbType">DataBaseType</param>
        /// <param name="classType">实现IBaseDBOprator的类</param>
        /// <param name="connectStr">连接字符串</param>
        /// <param name="setDefault">是否设置为默认</param>
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
        /// 获取注册过的并且设置为默认数据库操作类
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
        /// 根据注册的数据库类型获取相应的操作类
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
        /// 根据注册的数据库类型获取相应的操作类
        /// </summary>
        /// <param name="dbType">DataBaseType</param>
        /// <param name="connectStr">数据库连接字符串</param>
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
        /// <param name="connectStr">数据库连接字符串</param>
        /// <param name="setDefault">是否设置为默认数据库操作类</param>
        public static void RegisterDefaultOracleDb(string connectStr, bool setDefault)
        {
            RegisterDataBaseType(DataBaseType.Oracle, typeof(Imp.OracleDBOpretor), connectStr, setDefault);
        }
        #endregion
    }
}
