using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

/// <summary>
///sqlexec 的摘要说明
/// </summary>
public class Sqlexec
{
    static string sqlconnstr;
    static string reg;
    SqlDataAdapter sqldapt;
    DataSet ds;

    public Sqlexec()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public Sqlexec(string strcon)
    {
        sqlconnstr = strcon;
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public static string cutBadStr(string inputStr)
    {
        inputStr = inputStr.ToLower().Replace(",", "");
        inputStr = inputStr.ToLower().Replace("<", "");
        inputStr = inputStr.ToLower().Replace(">", "");
        inputStr = inputStr.ToLower().Replace("%", "");
        inputStr = inputStr.ToLower().Replace(".", "");
        inputStr = inputStr.ToLower().Replace(":", "");
        inputStr = inputStr.ToLower().Replace("#", "");
        inputStr = inputStr.ToLower().Replace("&", "");
        inputStr = inputStr.ToLower().Replace("$", "");
        inputStr = inputStr.ToLower().Replace("^", "");
        inputStr = inputStr.ToLower().Replace("*", "");
        inputStr = inputStr.ToLower().Replace("`", "");
        inputStr = inputStr.ToLower().Replace(" ", "");
        inputStr = inputStr.ToLower().Replace("~", "");
        inputStr = inputStr.ToLower().Replace("or", "");
        inputStr = inputStr.ToLower().Replace("and", "");
        inputStr = inputStr.ToLower().Replace("'", "");
        //以上的意思为前者被后者代替，比如and代替为空，<代替为：&glt;
        return inputStr;

    }
    /// <summary>
    /// 创建access数据库连接
    /// </summary>
    /// <returns></returns>
    public static OleDbConnection Creation(string ConnectionString)
    {
        return new OleDbConnection(ConnectionString);
    }
    ///<summary>
    /// 创建SQL数据库存连接
    /// <returns  ></returns>
    /// </summary>
    public static SqlConnection SqlCreation(string ConnectionString)
    {
        return new SqlConnection(ConnectionString);
    }
    ///<summary>
    /// Access数据库SQL语句执行；
    /// </summary>

    ///<summary>
    /// SQLserver数据库执行一条命令;
    /// </summary>
    public bool SqlExecuteSQL(string txtSql)
    {
        try
        {
            using (SqlConnection myconn = new SqlConnection(sqlconnstr))
            {


                myconn.Open();
                using (SqlTransaction trans = myconn.BeginTransaction())
                {
                    try
                    {

                        SqlCommand sqlcmd = new SqlCommand(txtSql, myconn);
                        sqlcmd.Transaction = trans;
                        int row = sqlcmd.ExecuteNonQuery();
                        if (row > 0)
                        {
                            trans.Commit();
                            return true;
                        }
                        else
                        {
                            trans.Rollback();
                            return false;
                        }

                    }
                    catch (Exception sqlerr)
                    {
                        trans.Rollback();

                        throw new Exception(sqlerr.Message, sqlerr);
                        
                    }
                    finally
                    {
                        myconn.Dispose();
                        myconn.Close();
                    }
                }
            }
        }
        catch (Exception sqlerr)
        {
           
            throw new Exception(sqlerr.Message, sqlerr);
        }
    }
    /// <summary>    
    /// 返回一个值    
    /// </summary>    
    /// <param name="strSql">sqlStr执行的SQL语句</param>    
    /// <returns>返回获取的值</returns>    

    /// <summary>    
    /// 说  明：  GetDataSet数据集，返回数据源的数据表    
    /// 返回值：  数据源的数据表    
    /// 参  数：  sqlStr执行的SQL语句，TableName 数据表名称    
    /// </summary>    

    /// 说明：　SqlGetDataSet数据集为SQL版数据集只能调用SQLSERVER数据；
    /// 返回值：数据表；
    /// </summary>
    public DataTable SqlGetDataSet(string StrTxtSql)
    {
        ds = new DataSet();
        using (SqlConnection myconn = new SqlConnection(sqlconnstr))
        {
            try
            {
                myconn.Open();
                SqlCommand sqlcmd = new SqlCommand(StrTxtSql, myconn);
                sqlcmd.CommandTimeout = 3600;
                sqldapt = new SqlDataAdapter(sqlcmd);
                sqldapt.Fill(ds);
                return ds.Tables[0];//返回数据集，前台调用时，直接不用再初始化;不用再New了。
            }
            catch (Exception er)
            {
                throw new Exception(er.Message, er);
            }
            finally
            {
                myconn.Dispose();
                sqldapt.Dispose();
                myconn.Close();
            }
        }

    }
}
///<summary>