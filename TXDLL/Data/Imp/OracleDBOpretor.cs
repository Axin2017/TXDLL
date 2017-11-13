using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Reflection;
using TXDLL.Data.Interface;
using TXDLL.Tools;

namespace TXDLL.Data.Imp
{
    class OracleDBOpretor : IBaseDBOprator
    {
        private OracleConnection _connection;
        private string _connectStr;
        private OracleConnection Connection
        {
            get
            {
                if (_connection==null)
                {
                    _connection = new OracleConnection(_connectStr);
                }
                return _connection;
            }
        }

        public OracleDBOpretor()
        {
            this._connectStr = AppConfigTools.GetConnectString(AppConfigTools.GetAppSettingString("DefaultConnStr"));
        }
        public OracleDBOpretor(string connectStr)
        {
            this._connectStr = connectStr;
        }

        public IDbConnection GetConnection()
        {
            return  GetConnection(_connectStr);
        }
        public IDbConnection GetConnection(string connectStr)
        {
            return new OracleConnection(connectStr);
        }

        public IDbTransaction GetTransaction()
        {
            IDbConnection conn = null;
            return GetTransaction(conn);
        }
        public IDbTransaction GetTransaction(IDbConnection conn)
        {
            if (conn==null)
            {
                conn = Connection;
            }
            CheckConnState(ref conn);
            IDbTransaction otrans = conn.BeginTransaction();
            return otrans;
        }

        public int ExcuteSql(string sql, IDbConnection conn, IDbTransaction trans)
        {
            CheckConnState(ref conn);
            OracleCommand oraCmd = new OracleCommand(sql, (OracleConnection)conn);
            if (trans != null)
            {
                oraCmd.Transaction = trans as OracleTransaction;
            }
            int result = oraCmd.ExecuteNonQuery();
            return result;
        }
        public int ExcuteSql(string sql, IDbConnection conn)
        {
            return ExcuteSql(sql, conn, null);
        }
        public int ExcuteSql(string sql, IDbTransaction trans)
        {
            return ExcuteSql(sql, null,  trans);
        }
        public int ExcuteSql(string sql)
        {
            return ExcuteSql(sql, null,null);
        }

        public T GetEntity<T>(string condition) where T : new()
        {
            CheckConnState();
            T t = new T();
            Type type = t.GetType();
            string tableName = type.Name;
            PropertyInfo[] pis = type.GetProperties();
            string selectStr = "select * from " + tableName + " where " + condition;
            DataSet ds = SelectDsBySql(selectStr,Connection);
            if (DataTools.DsIsNotNull(ds))
            {
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    if (ds.Tables[0].Rows[0][col.ColumnName] != DBNull.Value && type.GetProperty(col.ColumnName) != null)
                    {
                        type.GetProperty(col.ColumnName).SetValue(t, Convert.ChangeType(ds.Tables[0].Rows[0][col.ColumnName], type.GetProperty(col.ColumnName).PropertyType), null);
                    }
                }
            }
            else
            {
                t = default(T);
            }
            return t;
        }
        public T GetEntity<T>(string condition,IDbConnection conn ) where T : new()
        {
            CheckConnState(ref conn);
            T t = new T();
            Type type = t.GetType();
            string tableName = type.Name;
            PropertyInfo[] pis = type.GetProperties();
            string selectStr = "select * from " + tableName + " where " + condition;
            DataSet ds = SelectDsBySql(selectStr, conn);
            if (DataTools.DsIsNotNull(ds))
            {
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    if (ds.Tables[0].Rows[0][col.ColumnName] != DBNull.Value && type.GetProperty(col.ColumnName) != null)
                    {
                        type.GetProperty(col.ColumnName).SetValue(t, Convert.ChangeType(ds.Tables[0].Rows[0][col.ColumnName], type.GetProperty(col.ColumnName).PropertyType), null);
                    }
                }
            }
            else
            {
                t = default(T);
            }
            return t;
        }
        public List<T> GetEntityList<T>(string condition) where T : new()
        {
            CheckConnState();
            List<T> list = null;
            Type type = typeof(T);
            string tableName = type.Name;
            PropertyInfo[] pis = type.GetProperties();
            string selectStr = "select * from " + tableName + " where " + condition;
            DataSet ds = SelectDsBySql(selectStr,Connection);
            if (DataTools.DsIsNotNull(ds))
            {
                list = new List<T>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    T tt = new T();
                    foreach (DataColumn col in ds.Tables[0].Columns)
                    {
                        if (row[col] != DBNull.Value && type.GetProperty(col.ColumnName) != null)
                        {
                            type.GetProperty(col.ColumnName).SetValue(tt, Convert.ChangeType(row[col], type.GetProperty(col.ColumnName).PropertyType), null);
                        }
                    }
                    list.Add(tt);
                }
            }
            return list;
        }
        public List<T> GetEntityList<T>(string condition,IDbConnection conn ) where T : new()
        {
            CheckConnState(ref conn);
            List<T> list = null;
            Type type = typeof(T);
            string tableName = type.Name;
            PropertyInfo[] pis = type.GetProperties();
            string selectStr = "select * from " + tableName + " where " + condition;
            DataSet ds = SelectDsBySql(selectStr, conn);
            if (DataTools.DsIsNotNull(ds))
            {
                list = new List<T>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    T tt = new T();
                    foreach (DataColumn col in ds.Tables[0].Columns)
                    {
                        if (row[col] != DBNull.Value && type.GetProperty(col.ColumnName) != null)
                        {
                            type.GetProperty(col.ColumnName).SetValue(tt, Convert.ChangeType(row[col], type.GetProperty(col.ColumnName).PropertyType), null);
                        }
                    }
                    list.Add(tt);
                }
            }
            return list;
        }

        public int Insert<T>(T t, IDbTransaction trans)
        {
            return Insert(t, null, trans);
        }
        public int Insert<T>(T t, IDbConnection conn)
        {
            return Insert(t, conn, null);
        }
        public int Insert<T>(T t)
        {
            return Insert<T>(t, null,null);
        }
        public int Insert<T>(T t, IDbConnection conn, IDbTransaction trans)
        {
            CheckConnState(ref conn);
            Type type = t.GetType();
            string tableName = type.Name;
            PropertyInfo[] pis = type.GetProperties();
            string insertStr = "insert into " + tableName.ToUpper();
            string filedStr = "(";
            string valueStr = "(";
            for (int i = 0; i < pis.Length; i++)
            {
                if (pis[i].GetValue(t, null) != null)
                {
                    if (i != 0)
                    {
                        filedStr += ",";
                        valueStr += ",";
                    }
                    filedStr += pis[i].Name;
                    if (pis[i].PropertyType.ToString() == "System.Int32")
                    {
                        valueStr += pis[i].GetValue(t, null);
                    }
                    else if (pis[i].PropertyType.ToString() == "System.DateTime")
                    {
                        valueStr += "to_date('" + pis[i].GetValue(t, null) + "','YYYY-MM-DD  hh24:mi:ss')";
                    }
                    else
                    {
                        valueStr += "'" + pis[i].GetValue(t, null) + "'";
                    }
                }
            }
            filedStr += ")";
            valueStr += ")";
            insertStr += filedStr;
            insertStr += " values" + valueStr;
            OracleCommand oraCmd = new OracleCommand(insertStr, conn as OracleConnection);
            if (trans != null)
            {
                oraCmd.Transaction = trans as OracleTransaction;
            }
            int result = oraCmd.ExecuteNonQuery();
            return result;
        }

        public DataSet SelectDsBySql(string sql)
        {
            return SelectDsBySql(sql, null);
        }
        public DataSet SelectDsBySql(string sql, IDbConnection conn)
        {
            CheckConnState(ref conn);
            OracleCommand cmd = new OracleCommand(sql, (OracleConnection)conn);
            OracleDataAdapter orda = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            orda.Fill(ds);
            conn.Close();
            return ds;
        }

        private void CheckConnState()
        {
            IDbConnection conn = null;
            CheckConnState(ref conn);
        }
        private void CheckConnState(ref IDbConnection conn)
        {
            if (conn == null)
            {
                conn = Connection;
            }
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        public DataSet SelectDsByProcedure(string procedureName, IDbDataParameter[] parameters)
        {
            DataSet ds = new DataSet();
            OracleDataAdapter sqlDA = new OracleDataAdapter();
            OracleCommand command = new OracleCommand(procedureName, Connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (OracleParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            sqlDA.SelectCommand = command;
            sqlDA.Fill(ds);
            Connection.Close();
            return ds;
        }
        public DataSet SelectDsByProcedure(string procedureName, IDbConnection conn,  IDbDataParameter[] parameters)
        {
            DataSet ds = new DataSet();
            OracleDataAdapter sqlDA = new OracleDataAdapter();
            OracleCommand command = new OracleCommand(procedureName, (OracleConnection)conn);
            command.CommandType = CommandType.StoredProcedure;
            foreach (OracleParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            sqlDA.SelectCommand = command;
            sqlDA.Fill(ds);
            Connection.Close();
            return ds;
        }

        public void CloseConn()
        {
            IDbConnection conn = null;
            CloseConn(ref conn);
        }
        public void CloseConn(ref IDbConnection conn)
        {
            if (conn==null)
            {
                conn = Connection;
            }
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
    }
}
