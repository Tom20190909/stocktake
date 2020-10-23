using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace stocktake
{
    class oraexec
    {
        #region 变量
        /// <summary>  
        /// 数据库连接对象  
        /// </summary> 
        private OracleConnection _con = null;
       //  public static readonly string _constr = "Data Source=172.28.18.98/wms;User ID=wmsuser;Password=sys2016";
        public string _constr;
        #endregion 
        public oraexec(string conn)
        {
            _constr = conn;
        }
        #region 静态实例对象
        //public static readonly PublicMethod Instance = new PublicMethod();
        #endregion

        #region 初始化常用变量


        #endregion  

        #region 属性

        /// <summary>  
        /// 获取或设置数据库连接对象  
        /// </summary>  
        public OracleConnection Con
        {
            get
            {

                if (_con == null)
                {
                    _con = new OracleConnection();
                }
                if (_con.ConnectionString == null || _con.ConnectionString.Equals(string.Empty))
                {
                    _con.ConnectionString = _constr;
                }
                return _con;
            }
            set
            {
                _con = value;
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 执行不查询的数据库操作
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string strSql)
        {
            int result = 0;
            try
            {
                using (OracleCommand cmd = new OracleCommand(strSql, Con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;

                        Con.Open();
                        result = cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        result = -1;
                    }
                }
            }
            finally
            {
                if (Con.State != ConnectionState.Closed)
                {
                    Con.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataTable GetDt(string strSql)
        {
            DataTable result = new DataTable();
            try
            {
                using (OracleCommand cmd = new OracleCommand(strSql, Con))
                {

                    try
                    {
                        OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                        adapter.Fill(result);
                    }
                    catch (Exception ex)
                    {
                        result = null;
                    }
                }
            }
            finally
            {
                if (Con.State != ConnectionState.Closed)
                {
                    Con.Close();
                }
            }
            return result;
        }


        #region ExecuteOrcSqlStr
        /// <summary>
        /// ExecuteOrcSqlStr
        /// </summary>
        /// <param name="arrSql"></param>
        /// <returns></returns>
        public bool ExecuteOrcSqlStr(StringBuilder arrSql)
        {
            bool result = false;

            string[] arrSql_strs;
            if (arrSql.ToString().EndsWith("`") == true)
            {
                arrSql_strs = arrSql.ToString().Substring(0, arrSql.ToString().Length - 1).Split('`');
            }
            else
            {
                arrSql_strs = arrSql.ToString().Split('`');
            }
            //XX

            Con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd = Con.CreateCommand();

            OracleTransaction trans;
            trans = Con.BeginTransaction();


            //(ITransaction)ContextUtil.Transaction;

            /**原来启动锁的位置**/

            cmd.Transaction = trans; //20160706

            try
            {
                for (int i = 0; i < arrSql_strs.Length; i++)
                {
                    if (arrSql_strs[i].ToString().Trim() != "")
                    {
                        cmd.CommandText = arrSql_strs[i].ToString();
                        cmd.ExecuteNonQuery();
                    }
                }
                trans.Commit();
                result = true;
            }
            catch (Exception err)
            {
                string strErr = err.ToString();
                trans.Rollback();
            }
            finally
            {
                trans.Dispose();
                if (Con.State != ConnectionState.Closed)
                {
                    Con.Close();
                }
            }
            return result;
        }
        #endregion

        #endregion
    }
}
