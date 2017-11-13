using System.Collections.Generic;
using System.Data;
using TXDLL.Data.Enums;

namespace TXDLL.Data.Interface
{
    public interface IBaseDBOprator
    {
        IDbConnection GetConnection();
        IDbConnection GetConnection(string connectStr);
        IDbTransaction GetTransaction();
        IDbTransaction GetTransaction(IDbConnection conn);
        DataSet SelectDsBySql(string sql);
        DataSet SelectDsBySql(string sql ,IDbConnection conn);
        int ExcuteSql(string sql);
        int ExcuteSql(string sql, IDbTransaction trans);
        int ExcuteSql(string sql, IDbConnection conn);
        int ExcuteSql(string sql, IDbConnection conn,  IDbTransaction trans);
        int Insert<T>(T t);
        int Insert<T>(T t, IDbTransaction trans);
        int Insert<T>(T t, IDbConnection conn);
        int Insert<T>(T t, IDbConnection conn, IDbTransaction trans);
        T GetEntity<T>(string condition) where T : new();
        T GetEntity<T>(string condition,IDbConnection conn) where T : new();
        List<T> GetEntityList<T>(string condition) where T : new();
        List<T> GetEntityList<T>(string condition,IDbConnection conn) where T : new();
        DataSet SelectDsByProcedure(string procedureName, IDbDataParameter[] parameters);
        DataSet SelectDsByProcedure(string procedureName, IDbConnection conn,IDbDataParameter[] parameters);
        void CloseConn();
        void CloseConn(ref IDbConnection conn);
    }
}
