using System.Collections.Generic;
using System.Data;
using TXDLL.Data.Enums;

namespace TXDLL.Data.Interface
{
    /// <summary>
    /// 数据库操作接口
    /// </summary>
    public interface IBaseDBOprator
    {
        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();
        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <param name="connectStr">连接字符串</param>
        /// <returns></returns>
        IDbConnection GetConnection(string connectStr);
        /// <summary>
        /// 获取事务
        /// </summary>
        /// <returns></returns>
        IDbTransaction GetTransaction();
        /// <summary>
        /// 获取特定连接的事务
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        IDbTransaction GetTransaction(IDbConnection conn);
        /// <summary>
        /// 查询sql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>DataSet</returns>
        DataSet SelectDsBySql(string sql);
        /// <summary>
        /// 查询sql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接对象</param>
        /// <returns>DataSet</returns>
        DataSet SelectDsBySql(string sql ,IDbConnection conn);
        /// <summary>
        /// 查询某个字段
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>当前字段的object对象或者null</returns>
        object SelectObjBySql(string sql);
        /// <summary>
        /// 查询某个字段
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接对象</param>
        /// <returns>当前字段的object对象或者null</returns>
        object SelectObjBySql(string sql, IDbConnection conn);
        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>受影响的行数</returns>
        int ExcuteSql(string sql);
        /// <summary>
        /// 执行sql，含事务
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="trans">事务对象</param>
        /// <returns>受影响的行数</returns>
        int ExcuteSql(string sql, IDbTransaction trans);
        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接对象</param>
        /// <returns>受影响的行数</returns>
        int ExcuteSql(string sql, IDbConnection conn);
        /// <summary>
        /// 执行sql，含事务
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="trans">事务对象</param>
        /// <returns>受影响的行数</returns>
        int ExcuteSql(string sql, IDbConnection conn,  IDbTransaction trans);
        /// <summary>
        /// 插入实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns>受影响的行数</returns>
        int Insert<T>(T t);
        /// <summary>
        /// 插入实体类，包含事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="trans">事务对象</param>
        /// <returns>受影响的行数</returns>
        int Insert<T>(T t, IDbTransaction trans);
        /// <summary>
        /// 插入实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="conn">数据库连接对象</param>
        /// <returns>受影响的行数</returns>
        int Insert<T>(T t, IDbConnection conn);
        /// <summary>
        /// 插入实体类，含事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="trans">事务对象</param>
        /// <returns>受影响的行数</returns>
        int Insert<T>(T t, IDbConnection conn, IDbTransaction trans);
        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">sql语句条件部分</param>
        /// <returns>实体类的实例</returns>
        T GetEntity<T>(string condition) where T : new();
        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">sql语句条件部分</param>
        /// <param name="conn">数据库连接对象</param>
        /// <returns>实体类的实例</returns>
        T GetEntity<T>(string condition,IDbConnection conn) where T : new();
        /// <summary>
        /// 获取实体类的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">sql语句条件部分</param>
        /// <returns>实体类的实例集合</returns>
        List<T> GetEntityList<T>(string condition) where T : new();
        /// <summary>
        /// 获取实体类的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">sql语句条件部分</param>
        /// <param name="conn">数据库连接对象</param>
        /// <returns>实体类的实例集合</returns>
        List<T> GetEntityList<T>(string condition,IDbConnection conn) where T : new();
        /// <summary>
        /// 使用存储过程查询返回ds
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>DataSet</returns>
        DataSet SelectDsByProcedure(string procedureName, IDbDataParameter[] parameters);
        /// <summary>
        /// 使用存储过程查询返回ds
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>DataSet</returns>
        DataSet SelectDsByProcedure(string procedureName, IDbConnection conn,IDbDataParameter[] parameters);
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        void CloseConn();
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        void CloseConn(ref IDbConnection conn);
    }
}
