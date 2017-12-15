using System.Data;

namespace TXDLL.Tools
{
    public class DataTools
    {
        /// <summary>
        /// 判断ds是否不为空
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool DsIsNotNull(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}
