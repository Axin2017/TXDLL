using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace TXDLL.Tools
{
    /// <summary>
    /// json转换工具
    /// </summary>
    public class JsonTools
    {

        public static string DataTableToJson(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            if (dt!=null && dt.Rows.Count > 0)
            {
                sb.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0) sb.Append(",");
                    sb.Append(DataRowToJson(dt.Rows[i]));
                }
                sb.Append("]");
            }
            else
            {
                sb.Append("[]");
            }
            return sb.ToString();
        }

        public static string DataSetToJson(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                sb.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i > 0) sb.Append(",");
                    sb.Append(DataRowToJson(ds.Tables[0].Rows[i]));
                }
                sb.Append("]");
            }
            else
            {
                sb.Append("[]");
            }
            return sb.ToString();
        }

        public static string DataRowToJson(DataRow dr)
        {
            StringBuilder sb = new StringBuilder();
            if (dr != null && dr.Table.Columns.Count > 0)
            {
                sb.Append("{");
                foreach (DataColumn col in dr.Table.Columns)
                {
                    if (col.Ordinal > 0) sb.Append(",");
                    sb.Append("\"");
                    sb.Append(col.ColumnName);
                    sb.Append("\"");
                    if (col.DataType.Equals(typeof(int)) || col.DataType.Equals(typeof(decimal)))
                    {
                        sb.Append(":");
                        sb.Append(dr[col]);
                        if (dr[col] == null || dr[col] == DBNull.Value)
                        {
                            sb.Append("\"\"");//双引号
                        }
                    }
                    else if (col.DataType.Equals(typeof(bool)))
                    {
                        sb.Append(":");
                        sb.Append(dr[col].ToString().ToLower());
                    }
                    else
                    {
                        sb.Append(":\"");
                        sb.Append(dr[col].ToString());
                        sb.Append("\"");
                    }
                }
                sb.Append("}");
            }
            return sb.ToString();
        }
        
        /// <summary>
        /// object转换为json
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjectToJson(Object t)
        {
            return new JavaScriptSerializer().Serialize(t);
        }
        /// <summary>
        /// json转换为object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonStrToObject<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            T sn = serializer.Deserialize<T>(json);
            return sn;
        }
        /// <summary>
        /// jon转换为List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<T> JSonStringToList<T>(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<T> objs = serializer.Deserialize<List<T>>(json);
            return objs;
        }
    }
}
