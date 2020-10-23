using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using stocktake.BLL;
using stocktake.DAL;
using System.Data.Common;
using System.Data.OleDb;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Data.OracleClient;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;
using System.Data.SqlClient;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Windows.Forms.VisualStyles;
using NPOI.SS.UserModel;

namespace stocktake
{
    public partial class rpt : Form
    {
        
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        string connstr = "User Id=dswmsuser;Password=sys2016;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.28.18.98)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=newwms)))";
        string StrConn = "Data Source=172.28.18.97;Initial Catalog=JHH20180430;Persist Security Info=True;User ID=sa;Password=4cemgehI";
        //  static long row = 2;
        string filePath;
        public rpt()
        {
            InitializeComponent();
        }

        private void rpt_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出文件，使用文件流。该方法使用的数据源为DataTable,导出的Excel文件没有具体的样式。
        /// </summary>
        /// <param name="dt"></param>
        public  void ExportToExcel(System.Data.DataTable dt, string path)
        {
            KillSpecialExcel();
            string result = string.Empty;
            try
            {
                // 实例化流对象，以特定的编码向流中写入字符。
                StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));

                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    // 添加列名称
                    sb.Append(dt.Columns[k].ColumnName.ToString() + " ");
                }
                sb.Append(Environment.NewLine);
                // 添加行数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        // 根据列数追加行数据
                        sb.Append(row[j].ToString() + " ");
                    }
                    sb.Append(Environment.NewLine);
                }
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
                sw.Dispose();

                // 导出成功后打开
                //System.Diagnostics.Process.Start(path);
            }
            catch (Exception)
            {
                result = "请保存或关闭可能已打开的Excel文件";
            }
            finally
            {
                dt.Dispose();
            }
            this.Invoke(new Action(() =>
            {
                richTextBox4.AppendText(DateTime.Now.ToString() + "--保存记录到excel完成... ");

            }));
        }
        /// <summary>
        /// 结束进程
        /// </summary>
        private static void KillSpecialExcel()
        {
            foreach (System.Diagnostics.Process theProc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
            {
                if (!theProc.HasExited)
                {
                    bool b = theProc.CloseMainWindow();
                    if (b == false)
                    {
                        theProc.Kill();
                    }
                    theProc.Close();
                }
            }
        }

        private string getelement(string element_name)
        {

            string path = System.Windows.Forms.Application.StartupPath + "\\config.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode xnode = xmlDoc.GetElementsByTagName(element_name)[0];
            return xnode.InnerText;
        }
        public void ExportToExcel(DataTable dataTable, string fileName, bool isOpen = false)
        {
            var lines = new List<string>();
            string[] columnNames = dataTable.Columns
                                            .Cast<DataColumn>()
                                            .Select(column => column.ColumnName)
                                            .ToArray();
            var header = string.Join(",", columnNames);
            lines.Add(header);
            var valueLines = dataTable.AsEnumerable()
                            .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);
            File.WriteAllLines(fileName, lines);
            if (isOpen)
            Process.Start(fileName);
            this.Invoke(new Action(() =>
            {
                richTextBox4.AppendText(DateTime.Now.ToString() + "--保存记录到excel完成... ");

            }));

        }
        public void readexcel(DataTable dt)
        {
          //  DataTable dataTable = new DataTable();

          
            string fileName = System.Windows.Forms.Application.StartupPath + "\\exportcopy" + DateTime.Now.ToString().Replace('/', '-').Replace("-", "").Replace(":", "").Replace(" ", "") + ".csv";
            //  Stopwatch watch = new Stopwatch();
            //   watch.Start();
            //    ExportToExcel(dt, fileName);
            //   watch.Stop();
            //  ("导出完毕,用时:" + watch.Elapsed).Dump();
            for(int c=0;c<dt.Columns.Count;c++)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox4.AppendText(dt.Columns[c].ColumnName+" ");
                   

                }));
            }

            ExportToExcel(dt, fileName);



        }
        public void readexcel1(DataTable dt)
        {
            string path = System.Windows.Forms.Application.StartupPath + "\\export.xlsx";

            filePath = System.Windows.Forms.Application.StartupPath + "\\exportcopy"+ DateTime.Now.ToString().Replace('/', '-').Replace("-", "").Replace(":", "").Replace(" ", "") + ".xlsx";
            if (!File.Exists(path))
            {
                MessageBox.Show("模板文件不存在！");
                return;
            }

            File.Copy(path, filePath, true);
            object missing = Type.Missing;
            Microsoft.Office.Interop.Excel.Application ExcelRS;
            Microsoft.Office.Interop.Excel.Workbook RSbook;
            Microsoft.Office.Interop.Excel.Worksheet RSsheet;
            ExcelRS = new Microsoft.Office.Interop.Excel.Application();
            //打开目标文件filePath
            RSbook = ExcelRS.Workbooks.Open(filePath, missing, missing, missing, missing, missing,
                missing, missing, missing, missing, missing, missing, missing, missing, missing);
            RSsheet = (Microsoft.Office.Interop.Excel.Worksheet)RSbook.Sheets.get_Item(1);
            try
            {
                //  ExcelRS = null;
                //实例化ExcelRS对象
                //   MessageBox.Show("开始保存");
                RSsheet.Activate();

                //设置第一个工作溥


             //   int row = 2;
                for (int r = 0; r < 10; r++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {

                        RSsheet.Cells[r+2, c+1] = dt.Rows[r][c].ToString();

                    }
                 //   row++;
                    this.Invoke(new Action(() =>
                    {
                        richTextBox4.AppendText(DateTime.Now.ToString() + "--保存第"+(r+1).ToString()+"记录中... ");
                      
                    }));
                }
                richTextBox4.AppendText(DateTime.Now.ToString() + "--保存记录完成... ");


            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
            finally
            {
                ExcelRS.DisplayAlerts = false;
                RSbook.Save();
                ExcelRS.Visible = false;
   
            }
        }
        public void BitchInsert(DataTable dt,string TtableName)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(StrConn))
                {
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);
                    bulkCopy.DestinationTableName = TtableName;
                    bulkCopy.BatchSize = dt.Rows.Count;
                    conn.Open();

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        bulkCopy.WriteToServer(dt);

                    }
                    this.Invoke(new Action(() =>
                    {

                        richTextBox4.AppendText(DateTime.Now.ToString() + "--插入完成... ");



                    }));
                }
            }
            catch (Exception er)
            {
                this.Invoke(new Action(() =>
                {

                    richTextBox4.AppendText(DateTime.Now.ToString() + "--插入失败:"+er.ToString()+" ");



                }));
            }
        }
        public  void BulkToDB(DataTable dt, string targetTable)
        {
            this.Invoke(new Action(() =>
            {
                richTextBox4.AppendText(DateTime.Now.ToString() + "--开始插入查询结果... ");

            }));
      
         
        Oracle.DataAccess.Client.OracleConnection conn = new Oracle.DataAccess.Client.OracleConnection(connstr);
            //System.Data.OracleClient.OracleConnection conn = new OracleConnection(conStr);
            if (conn.State != ConnectionState.Open)
            { conn.Open(); }
            OracleBulkCopy bulkCopy = new OracleBulkCopy(conn, OracleBulkCopyOptions.Default);
            bulkCopy.BatchSize = dt.Rows.Count;
            bulkCopy.BulkCopyTimeout = 260;
            bulkCopy.DestinationTableName = targetTable;

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                // conn.Open();
                if (dt != null && dt.Rows.Count != 0)
                {
                    bulkCopy.WriteToServer(dt);

                }
                else
                {
                    this.Invoke(new Action(() =>
                    {

                        richTextBox4.AppendText("没有可以插入的记录 ");
                       


                    }));
                }
                this.Invoke(new Action(() =>
                {
                   
                        richTextBox4.AppendText("插入记录数:" + dt.Rows.Count.ToString() + " ");
                       // richTextBox3.AppendText(DateTime.Now.ToString() + "--查询报表中... ");
                        richTextBox4.Focus();
                        richTextBox4.AppendText(DateTime.Now.ToString() + "--插入完成... ");
                        richTextBox4.Focus();
                   
                   
                }));
                Task.Run(() => ExpFromorcl());
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox4.AppendText(DateTime.Now.ToString() + "--插入报错"+ex.ToString()+" ");
                    richTextBox4.Focus();

                }));
               
            }
            finally
            {
                conn.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();
            }

        }

        public void getrpt()
        {
            this.Invoke(new Action(() =>
            {
                richTextBox3.AppendText(DateTime.Now.ToString() + "开始从服务器获取数据... ");
                richTextBox3.Focus();
              

            }));
            var suc = new SoapUnitTOM.SoapUnitClient();
          //  string connstr = getelement("connstr");
         //   richTextBox1.AppendText("oracle连接字符串：" + connstr + " ");
            try
            {
                oraexec ora = new oraexec(connstr);
                ora.ExecuteNonQuery("truncate table  JXC_FOR_SAP");
                //   DataTable dt = ora.GetDt("select count(1) from jxc_for_sap");
                //    richTextBox1.AppendText("select count(1) from jxc_for_sap的结果:" + dt.Rows.Count.ToString());
                DataTable dtrpt=new DataTable();
                DataTable dt = suc.GetData(1, "DXKH",null);
                this.Invoke(new Action(() =>
                {
                    richTextBox3.AppendText(DateTime.Now.ToString()+"--开始查询报表... ");
                    richTextBox3.Focus();

                }));
             for (int i=0;i< dt.Rows.Count; i++)
                 // for (int i = 0; i < 10; i++)
                    {
                    this.Invoke(new Action(() =>
                    {
                        richTextBox3.AppendText(DateTime.Now.ToString() + "--"+(i+1).ToString()+"查询店铺(" + dt.Rows[i][1].ToString() + ")报表中... ");
                       // richTextBox3.AppendText(DateTime.Now.ToString() + "--查询报表中... ");
                        richTextBox3.Focus();
                    }));
                    string[] args = {  Std.Value.ToString("yyyy-MM-dd"), Etd.Value.ToString("yyyy-MM-dd"), dt.Rows[i][0].ToString(), Std.Value.ToString("yyyy-MM") };
                                        
                    dtrpt = suc.GetData(1, "Single_Shop", args);
                    //   DataTable dtrptcopy = dtrpt.Copy();
                      Task.Run(() => BulkToDB(dtrpt, "JXC_FOR_SAP"));
                  //  Task.Run(() => readexcel(dtrpt));

                }
              

                this.Invoke(new Action(() =>
                {
                  
                    richTextBox3.AppendText(DateTime.Now.ToString() + "--查寻结束 ");
                  
                }));
            }
            catch (Exception er)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox3.AppendText(er.ToString()+" ");
                 
                }));
            }
        }
        public void getrptone()
        {
            this.Invoke(new Action(() =>
            {
                richTextBox3.AppendText(DateTime.Now.ToString() + "开始从服务器获取数据... \r\n");
                richTextBox3.Focus();
                this.button2.Enabled = false;
                this.button5.Enabled = false;
                this.button6.Enabled = false;
                this.button7.Enabled = false;
                this.button8.Enabled = false;
            }));
         
            try
            {
                oraexec ora = new oraexec(connstr);
                ora.ExecuteNonQuery("truncate table  test");
             
                DataTable dtrpt = new DataTable();
               
                this.Invoke(new Action(() =>
                {
                    richTextBox3.AppendText(DateTime.Now.ToString() + "--提取数据中...\r\n ");
                    richTextBox3.Focus();

                }));
             
                Sqlexec sqlexc = new Sqlexec(StrConn);
                string text = @"DECLARE @S_Date DATETIME
DECLARE @E_Date DATETIME
DECLARE @DOCMONTH VARCHAR(10)
SET @S_Date = CAST('{0}' AS DATETIME)
SET @E_Date = CAST('{1}' AS DATETIME)
SET @DOCMONTH='{2}'

SELECT 
        T0.ItemCode
    --  , T1.ItemName
     ,  T3.BinCode
      , T4.CardCode
      , T4.CardName
     -- , T1.U_CodeBard
   	
      ,SUM(T0.InvPLInQty)-SUM(T0.InvPLOutQty) QC_QTY
   into     #TEMP_00
FROM    [dbo].[MY_B1_OinmWithBinTransfer] T0
        INNER  JOIN [dbo].[VWE_OITM] T1 ON T0.[ItemCode] = T1.[ItemCode]
        LEFT OUTER  JOIN [dbo].[OBTL] T2 ON T0.[InvPLMessageID] = T2.[MessageID]
        INNER JOIN OBIN T3 ON T2.BinAbs = T3.AbsEntry
        LEFT JOIN VWE_OCRD T4 ON T4.U_BinCode = T3.BinCode
		LEFT JOIN (select U_Code,U_Name from U_CPJC) t5 on t1.u_xmbrand=t5.u_code
		LEFT JOIN (SELECT CARDCODE,U_SALEAREACODE,U_SALETEAMCODE FROM VWE_CRD1 GROUP BY CARDCODE,U_SALEAREACODE,U_SALETEAMCODE) TG ON T4.CARDCODE=TG.CARDCODE
WHERE  
        T0.Warehouse = 'zdmd'
        AND ( T4.U_CONSIGN = 'Y'
              OR T4.U_CONSIGN IS NULL
            )
        AND CONVERT(CHAR(10), T0.DocDate, 111) < CONVERT(CHAR(10), @S_Date, 111)
        group by
      
        T0.ItemCode
      , T1.ItemName
      , T3.BinCode
      , T4.CardCode
      , t4.CardName
      , T1.U_CodeBard
    



SELECT 
       T0.ItemCode
  
      , T3.BinCode
      , T4.CardCode
      , T4.CardName
   
   
	  ,SUM(T0.InvPLInQty) InvPLInQty
      ,SUM(T0.InvPLOutQty) InvPLOutQty 
    
INTO    #TEMP_01
FROM    [dbo].[MY_B1_OinmWithBinTransfer] T0
        INNER  JOIN [dbo].[VWE_OITM] T1 ON T0.[ItemCode] = T1.[ItemCode]
        LEFT OUTER  JOIN [dbo].[OBTL] T2 ON T0.[InvPLMessageID] = T2.[MessageID]
        INNER JOIN OBIN T3 ON T2.BinAbs = T3.AbsEntry
        LEFT JOIN VWE_OCRD T4 ON T4.U_BinCode = T3.BinCode
		LEFT JOIN (select U_Code,U_Name from U_CPJC) t5 on t1.u_xmbrand=t5.u_code
		LEFT JOIN (SELECT CARDCODE,U_SALEAREACODE,U_SALETEAMCODE FROM VWE_CRD1 GROUP BY CARDCODE,U_SALEAREACODE,U_SALETEAMCODE) TG ON T4.CARDCODE=TG.CARDCODE
WHERE 
        T0.Warehouse = 'zdmd'
        AND ( T4.U_CONSIGN = 'Y'
              OR T4.U_CONSIGN IS NULL
            )
        AND CONVERT(CHAR(10), T0.DocDate, 111) >= CONVERT(CHAR(10), @S_Date, 111)
        AND CONVERT(CHAR(10), T0.DocDate, 111) <= CONVERT(CHAR(10), @E_Date, 111)
       
        group by
      
        T0.ItemCode
      , T1.ItemName
      , T3.BinCode
      , T4.CardCode
      , t4.CardName
      , T1.U_CodeBard
  
  SELECT 
       T0.ItemCode
  
      , T3.BinCode
      , T4.CardCode
      , T4.CardName
   
       ,AVG(t0.price) AS AVGPRICE
	   , SUM(InvPLInQty - InvPLOutQty) AS QM_QTY
      , SUM(TRANSVALUE) AS STOCK
    
INTO    #TEMP_011
FROM    [dbo].[MY_B1_OinmWithBinTransfer] T0
        INNER  JOIN [dbo].[VWE_OITM] T1 ON T0.[ItemCode] = T1.[ItemCode]
        LEFT OUTER  JOIN [dbo].[OBTL] T2 ON T0.[InvPLMessageID] = T2.[MessageID]
        INNER JOIN OBIN T3 ON T2.BinAbs = T3.AbsEntry
        LEFT JOIN VWE_OCRD T4 ON T4.U_BinCode = T3.BinCode
		LEFT JOIN (select U_Code,U_Name from U_CPJC) t5 on t1.u_xmbrand=t5.u_code
		LEFT JOIN (SELECT CARDCODE,U_SALEAREACODE,U_SALETEAMCODE FROM VWE_CRD1 GROUP BY CARDCODE,U_SALEAREACODE,U_SALETEAMCODE) TG ON T4.CARDCODE=TG.CARDCODE
WHERE 
        T0.Warehouse = 'zdmd'
        AND ( T4.U_CONSIGN = 'Y'
              OR T4.U_CONSIGN IS NULL
            )
     
        AND CONVERT(CHAR(10), T0.DocDate, 111) <= CONVERT(CHAR(10), @E_Date, 111)
       
        group by
      
        T0.ItemCode
      , T1.ItemName
      , T3.BinCode
      , T4.CardCode
      , t4.CardName
      , T1.U_CodeBard
  
	select @DOCMONTH DOCMONTH ,a.BinCode,
	a.CardCode as CardCode,
	a.CardName as CardName,
	a.ItemCode as  ItemCode,
	
	b.QC_QTY QC_Qty,c.InvPLInQty AS In_QTY,c.InvPLOutQty as Out_Qty ,
	a.QM_QTY ,a.STOCK,a.AVGPRICE 
	INTO #TEMP_02
	from #TEMP_011 a 	left outer join #TEMP_00 b 	on a.BinCode=b.BinCode and a.CardCode=b.CardCode and a.ItemCode=b.ItemCode 
	left outer join #TEMP_01 c 	on a.BinCode=c.BinCode and a.CardCode=c.CardCode and a.ItemCode=c.ItemCode 
		


        SELECT
       @DOCMONTH DOCMONTH
       , T0.CardCode
       ,T0.CARDNAME
       ,T1.ItemCode
       ,T1.STOCKPRICE
       , T1.Quantity as saleqty
       , T1.GTotal as saletotal
       into #temp_03
FROM    OINV T0
        LEFT JOIN vwe_INV1 T1 ON T0.DOCENTRY = T1.DOCENTRY
        LEFT JOIN vwe_ocrd t2 ON t0.cardcode = t2.cardcode        
		LEFT JOIN VWE_OITM T3 ON T1.ItemCode=T3.ItemCode
		LEFT JOIN (SELECT U_CODE,U_NAME FROM U_CPJC) T4 ON T3.U_XMBrand=T4.U_CODE               
WHERE   
    
                      
            
        CONVERT(CHAR(10), T0.DocDate, 111) >= CONVERT(CHAR(10), @S_Date, 111)
        AND CONVERT(CHAR(10), T0.DocDate, 111) <= CONVERT(CHAR(10), @E_Date, 111)
        AND T0.DOCTYPE <> 'S'
        AND t2.u_consign = 'Y'

		union all
		SELECT  @DOCMONTH DOCMONTH,
		       
		 T0.CardCode 
		,T0.CARDNAME
        ,0 as STOCKPRICE
		,T1.ItemCode ,
		-1*T1.Quantity as saleqty,
		-1*t1.quantity * priceafvat AS saletotal

		FROM    ORIN T0
				LEFT JOIN vwe_rin1 T1 ON T0.DOCENTRY = T1.DOCENTRY
			   LEFT JOIN vwe_ocrd t2 ON t0.cardcode = t2.cardcode     
		    
		WHERE   
   
         CONVERT(CHAR(10), T0.DocDate, 111) >= CONVERT(CHAR(10), @S_Date, 111)
        AND CONVERT(CHAR(10), T0.DocDate, 111) <= CONVERT(CHAR(10), @E_Date, 111)
        AND T0.DOCTYPE <> 'S'
        AND t2.u_consign = 'Y' 
        
     select DOCMONTH,CardCode,CardName,ItemCode,SUM(saleqty) SALEQTY,sum(saletotal) saletotal into #temp_05  from  #temp_03     group by DOCMONTH,CardCode,CardName,ItemCode 
  
	 select isnull(a.DOCMONTH ,b.DOCMONTH) DOCMONTH,isnull(a.BinCode,'') bincode,isnull(a.CardCode,b.CardCode) CardCode,isnull(a.CardName,b.CardName) cardename,
	 
	 isnull(a.ItemCode,b.ItemCode ) itemcode,isnull(a.QC_QTY,0) QC_QTY,isnull(a.In_Qty,0) IN_QTY,isnull(a.Out_Qty,0) OUT_QTY,isnull(a.QM_Qty,0) QM_QTY ,
	 ISNULL(b.saleqty,0) SALEQTY,ISNULL(b.saletotal,0) SALETOTAL ,a.STOCK,a.AVGPRICE 
	 into #temp_04
	 from #temp_02 a 
		  
	 full join 	#temp_05   b  on a.CardCode=b.CardCode and a.ItemCode=b.ItemCode and a.DOCMONTH=b.DOCMONTH
	 
	 
	
	 select
	  T4.DQJL 大区经理
	 ,T4.YWZG 业务主管
	 , T0.CardCode  店铺代码
	 ,t0.cardename  店铺名称
	 ,t0.QC_QTY  期初数量
	 ,t0.IN_QTY  入库数量
	 ,t0.OUT_QTY  出库数量
	 ,t0.QM_QTY   期末数量
	 ,t0.QM_QTY*T1.U_retailprice   期末库存金额
	  ,t0.STOCK      库存成本金额
	 ,t0.SALEQTY 销售上报数量
	 ,t0.SALETOTAL 销售上报金额
	 ,t0.itemcode 货品编号
	 ,t1.ItemName 货品名称
	 ,T5.u_name 品牌
	 ,T1.U_CodeBard 条码
	 ,T1.U_retailprice 零售价格
	 ,T0.AvgPrice 库存成本
	 ,T1.U_ModleCode
	  
	  
	 --  , TG.U_SALEAREACODE
	--   , TG.U_SALETEAMCODE 
	  
	  FROM   #temp_04 T0 
	 
	    LEFT OUTER JOIN [dbo].[VWE_OITM] T1 ON T0.[ItemCode] = T1.[ItemCode]
	 --   LEFT OUTER JOIN (select ItemCode, AvgPrice from [dbo].[VWE_OITW] where whscode='zdmd') T2 ON T0.[ItemCode] = T2.[ItemCode]
        LEFT OUTER JOIN U_MDRY1 T4 ON T4.CardCode = T0.CardCode
		LEFT OUTER JOIN (select U_Code,U_Name from U_CPJC) T5 on t1.u_xmbrand=t5.u_code
		LEFT OUTER JOIN (SELECT CARDCODE,U_SALEAREACODE,U_SALETEAMCODE FROM VWE_CRD1 GROUP BY CARDCODE,U_SALEAREACODE,U_SALETEAMCODE) TG ON T4.CARDCODE=TG.CARDCODE
	    --	LEFT OUTER JOIN 
		--(
		--	select U_code ,b.UserName from U_QPFB a
		--	left join T_cuse b
		--	on a.U_ManagerID = b.UserID
		--) T6 ON T6.U_CODE=TG.U_SALEAREACODE
		--LEFT OUTER JOIN 
		--(
		--	select U_code ,b.UserName from U_QPFB a
		--	left join T_cuse b
		--	on a.U_ManagerID = b.UserID
		--) T7 ON T7.U_CODE=TG.U_SALETEAMCODE 
	  ORDER BY 店铺代码  
	-- left outer join 
	-- )  c  group by DOCMONTH,cardcode,cardename  order by cardcode
	 

	 drop table #TEMP_00
	 drop table #TEMP_01 
	 drop table #TEMP_02
	 drop table #TEMP_03 
	 drop table #TEMP_04
     drop table #TEMP_05
     drop table #TEMP_011


 ";
                string newsql = text.Replace("{0}", Std.Value.ToString("yyyy/MM/dd")).Replace("{1}", Etd.Value.ToString("yyyy/MM/dd")).Replace("{2}", Std.Value.ToString("yyyy-MM"));
              
                dtrpt = sqlexc.SqlGetDataSet(newsql);
                Task.Run(() => BulkToDB(dtrpt, "test"));

                



            }
            catch (Exception er)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox3.AppendText(er.ToString() + " ");
                    this.button2.Enabled = true;
                    this.button5.Enabled = true;
                    this.button6.Enabled = true;
                    this.button7.Enabled = true;
                    this.button8.Enabled = true;
                }));
            }
        }
        public void getrpt_forshop()
        {
            this.Invoke(new Action(() =>
            {
                richTextBox3.AppendText(DateTime.Now.ToString() + "开始从服务器获取数据... \r\n");
                richTextBox3.Focus();
             

            }));
            var suc = new SoapUnitTOM.SoapUnitClient();
            //  string connstr = getelement("connstr");
            //   richTextBox1.AppendText("oracle连接字符串：" + connstr + " ");
            try
            {
                oraexec ora = new oraexec(connstr);
                ora.ExecuteNonQuery("delete from   JXC_FOR_SAP where CARDCODE='"+textBox1.Text+"'");
                //   DataTable dt = ora.GetDt("select count(1) from jxc_for_sap");
                //    richTextBox1.AppendText("select count(1) from jxc_for_sap的结果:" + dt.Rows.Count.ToString());
                DataTable dtrpt = new DataTable();
             //   DataTable dt = suc.GetData(1, "DXKH", null);
                this.Invoke(new Action(() =>
                {
                    richTextBox3.AppendText(DateTime.Now.ToString() + "--开始查询报表... \r\n");
                    richTextBox3.Focus();

                }));
              //  for (int i = 0; i < dt.Rows.Count; i++)
                // for (int i = 0; i < 10; i++)
              //  {
                    this.Invoke(new Action(() =>
                    {
                        richTextBox3.AppendText(DateTime.Now.ToString() + "--" + "查询店铺(" + textBox1.Text  + ")报表中...\r\n ");
                        // richTextBox3.AppendText(DateTime.Now.ToString() + "--查询报表中... ");
                        richTextBox3.Focus();
                    }));
                    string[] args = { Std.Value.ToString("yyyy-MM-dd"), Etd.Value.ToString("yyyy-MM-dd"), textBox1.Text , Std.Value.ToString("yyyy-MM") };

                    dtrpt = suc.GetData(1, "Single_Shop", args);
                    //   DataTable dtrptcopy = dtrpt.Copy();
                    Task.Run(() => BulkToDB(dtrpt, "JXC_FOR_SAP"));
                    //  Task.Run(() => readexcel(dtrpt));

               // }


                this.Invoke(new Action(() =>
                {

                    richTextBox3.AppendText(DateTime.Now.ToString() + "--查寻结束 \r\n");
                 
                }));
            }
            catch (Exception er)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox3.AppendText(er.ToString() + " ");
                  
                }));
            }
        }
        private string GetDate(int No)
        {
           // string sql = " select Convert(varchar(20), DATEADD(D, -1, (DATEADD(m, 1, CAST(DocMonth + '-01' AS DATETIME)))), 23) from U_PDZLU where DocEntry = " + No;
            string sql = " select DocMonth from U_PDZLU where DocEntry = " + No;
            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();
                SqlCommand smd = new SqlCommand(sql, conn);
                return smd.ExecuteScalar().ToString();

            }
        }
        private string CheckNo(int No)
        {
            string sql = " select count(1) from U_PDZLU where DocEntry = " + No;
            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();
                SqlCommand smd = new SqlCommand(sql, conn);
                return smd.ExecuteScalar().ToString();

            }
        }
        public void getrptforsql()
        {
            try
            {
                string docmonth = GetDate(int.Parse(textBox2.Text));
                using (SqlConnection conn = new SqlConnection(StrConn))
                {
                    conn.Open();
                    SqlCommand smd = new SqlCommand("delete from  InvData_Shop where  U_DocMonth='"+docmonth+"' ", conn);
                    smd.ExecuteNonQuery();

                    this.Invoke(new Action(() =>
                    {
                        richTextBox4.AppendText(DateTime.Now.ToString() + "清除原有数据完成...\r\n ");
                    }));
                }
            }
            catch (Exception er)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox4.AppendText(DateTime.Now.ToString() + "--清除原有数据失败:" + er.Message + " \r\n");

                }));
            }
            this.Invoke(new Action(() =>
            {
                richTextBox3.AppendText(DateTime.Now.ToString() + "开始从服务器获取数据... \r\n");
                richTextBox3.Focus();
               

            }));
            var suc = new SoapUnitTOM.SoapUnitClient();
            try
            {
                List<string> parlist = new List<string>();
                parlist.Add(textBox2.Text);
                DataTable dtrpt = new DataTable();
                DataTable dt = suc.GetData(1, "GetInvShop", parlist.ToArray());
                
                this.Invoke(new Action(() =>
                {
                    richTextBox3.AppendText(DateTime.Now.ToString() + "--开始查询报表... \r\n");
                    richTextBox3.Focus();

                }));
                for (int i = 0; i <dt.Rows.Count; i++)
                {
                    this.Invoke(new Action(() =>
                    {
                        richTextBox3.AppendText(DateTime.Now.ToString() + "--" + (i + 1).ToString() + "查询店铺(" + dt.Rows[i][1].ToString() + ")报表中... \r\n");
                        // richTextBox3.AppendText(DateTime.Now.ToString() + "--查询报表中... ");
                        richTextBox3.Focus();
                    }));
                    string[] args = { GetDate(int.Parse(textBox2.Text)),textBox2.Text, dt.Rows[i][0].ToString() };

                    dtrpt = suc.GetData(1, "GetInvData", args);
                    //   DataTable dtrptcopy = dtrpt.Copy();
                    Task.Run(() => BitchInsert(dtrpt, "InvData_Shop"));
                    //  Task.Run(() => readexcel(dtrpt));

                }


                this.Invoke(new Action(() =>
                {

                    richTextBox3.AppendText(DateTime.Now.ToString() + "--查寻结束 \r\n");
                  
                }));
            }
            catch (Exception er)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox3.AppendText(er.ToString() + " ");
                  

                }));
            }
        }
        public void getrptforsql_shop(string shopcode)
        {
            try
            {
                string docmonth = GetDate(int.Parse(textBox2.Text));
                using (SqlConnection conn = new SqlConnection(StrConn))
                {
                    conn.Open();
                    SqlCommand smd = new SqlCommand("delete from  InvData_Shop where  U_DocMonth='" + docmonth + "' and  U_AddressCode='"+shopcode+"'", conn);
                    smd.ExecuteNonQuery();

                    this.Invoke(new Action(() =>
                    {
                        richTextBox4.AppendText(DateTime.Now.ToString() + "清除原有数据完成... ");
                    }));
                }
            }
            catch (Exception er)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox4.AppendText(DateTime.Now.ToString() + "--清除原有数据失败:" + er.Message + " ");

                }));
            }
            this.Invoke(new Action(() =>
            {
                richTextBox3.AppendText(DateTime.Now.ToString() + "开始从服务器获取数据... ");
                richTextBox3.Focus();
              

            }));
            var suc = new SoapUnitTOM.SoapUnitClient();
            try
            {
               
                    this.Invoke(new Action(() =>
                    {
                        richTextBox3.AppendText(DateTime.Now.ToString() + "--"  + "查询店铺(" + shopcode + ")报表中... ");
                        // richTextBox3.AppendText(DateTime.Now.ToString() + "--查询报表中... ");
                        richTextBox3.Focus();
                    }));
                    string[] args = { GetDate(int.Parse(textBox2.Text)), textBox2.Text, shopcode };

                   DataTable  dtrpt = suc.GetData(1, "GetInvData", args);
                    //   DataTable dtrptcopy = dtrpt.Copy();
                    Task.Run(() => BitchInsert(dtrpt, "InvData_Shop"));
                    //  Task.Run(() => readexcel(dtrpt));

               


                this.Invoke(new Action(() =>
                {

                    richTextBox3.AppendText(DateTime.Now.ToString() + "--查寻结束 ");
                   
                }));
            }
            catch (Exception er)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox3.AppendText(er.ToString() + " ");
                  

                }));
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(StrConn))
            //    {

            //        conn.Open();
            //        SqlCommand cmd= conn.CreateCommand();
            //        cmd.CommandText = "truncate table  Single_Shop";
            //        int i=cmd.ExecuteNonQuery();
            //        if (i>-1)
            //        {
            //            this.Invoke(new Action(() =>
            //            {

            //                richTextBox3.AppendText(DateTime.Now.ToString() + "--清除原有数据完成... ");



            //            }));
            //        }
            //    }
            //}
            //catch (Exception er)
            //{
            //    this.Invoke(new Action(() =>
            //    {

            //        richTextBox3.AppendText(DateTime.Now.ToString() + "--清除原有数据失败:" + er.ToString() + " ");



            //    }));


            //}
          
            Task.Run(() => getrpt());
            
        }
        private void getsaledetail()
        {
            this.Invoke(new Action(() =>
            {
                richTextBox3.AppendText(DateTime.Now.ToString() + "开始从服务器获取数据... ");
                richTextBox3.Focus();
               

            }));
            var suc = new SoapUnitTOM.SoapUnitClient();
            
            //  string connstr = getelement("connstr");
            //   richTextBox1.AppendText("oracle连接字符串：" + connstr + " ");
            try
            {
                oraexec ora = new oraexec(connstr);

               int k= ora.ExecuteNonQuery("truncate table JXC_FOR_SALEDETAIL");
               
                this.Invoke(new Action(() =>
                {
                    richTextBox4.AppendText("清除已有记录结果:" + k.ToString() + " ");
                    // richTextBox3.AppendText(DateTime.Now.ToString() + "--查询报表中... ");
                    richTextBox4.Focus();
                }));
                //   DataTable dt = ora.GetDt("select count(1) from jxc_for_sap");
                //    richTextBox1.AppendText("select count(1) from jxc_for_sap的结果:" + dt.Rows.Count.ToString());
                DataTable dtrpt = new DataTable();

                this.Invoke(new Action(() =>
                {
                    richTextBox3.AppendText(DateTime.Now.ToString() + "--" + "查询店铺销售上报报表中... ");
                    // richTextBox3.AppendText(DateTime.Now.ToString() + "--查询报表中... ");
                    richTextBox3.Focus();
                }));
                string[] args = { Std.Value.ToString("yyyy-MM") };

                dtrpt = suc.GetData(1, "Get_saleDetail", args);
                //  for (int i=0;i<dtrpt.Rows.Count;i++)
                //for (int i = 0; i < 2; i++)
                //{
                //    string sql = "insert into JXC_FOR_SALEDETAIL values ('" + dtrpt.Rows[i][0].ToString() + "','" + dtrpt.Rows[i][1].ToString() + "','" + dtrpt.Rows[i][2].ToString() + "','" + dtrpt.Rows[i][3].ToString() + "','" + dtrpt.Rows[i][4].ToString() + "'," + double.Parse(dtrpt.Rows[i][5].ToString()) + ");";
                //    //this.Invoke(new Action(() =>
                //    //{
                //    //  //  richTextBox4.AppendText(sql+" ");
                //    //  //  richTextBox4.AppendText(sql + " ");
                //    //   // richTextBox4.Focus();
                //    //}));


                //    int j = ora.ExecuteNonQuery(sql);
                //    if (j != -1)
                //   {
                //        this.Invoke(new Action(() =>
                //        {
                //            richTextBox4.AppendText("插入第" + (i + 1).ToString() + "记录,执行结果:" + j.ToString() + " ");
                //                  richTextBox4.AppendText(sql+" ");
                //                richTextBox4.Focus();
                //        }));
                //   }
                //}
                  DataTable dtrptcopy = dtrpt.Clone();
                  DataRow[] dr = dtrpt.Select();
                    for (int i = 0; i < 100; i++)
                    {
                     dtrptcopy.ImportRow((DataRow)dr[i]);
                    }
                Task.Run(() => BulkToDB(dtrptcopy, "JXC_FOR_SALEDETAIL"));
                //  Task.Run(() => readexcel(dtrpt));
                this.Invoke(new Action(() =>
                {
                    richTextBox4.AppendText("插入记录数:"+ dtrpt.Rows.Count.ToString() + " ");
                    // richTextBox3.AppendText(DateTime.Now.ToString() + "--查询报表中... ");
                    richTextBox3.Focus();
                }));



                this.Invoke(new Action(() =>
                {

                    richTextBox4.AppendText(DateTime.Now.ToString() + "--运行结束 ");
                   
                }));
                
            }
            catch (Exception er)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox3.AppendText(er.ToString() + " ");
                  

                }));
            }
            finally
            {

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
           // Task.Run(() => getsaledetail());

        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox3.Text=Std.Value.ToString("yyyy-MM");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                oraexec ora = new oraexec(connstr);
                int i = ora.ExecuteNonQuery("truncate table  jxc_for_sap");
                richTextBox3.AppendText(DateTime.Now.ToString() + "--清除数据结果:" + i.ToString() + " ");
                DataTable dt = new DataTable();
                dt = ora.GetDt("select * from t_mom_outinwmsresultorder where  state=2 ");
                if (dt.Rows.Count > 0)
                {
                    richTextBox3.AppendText(DateTime.Now.ToString() + "--查询到" + dt.Rows.Count.ToString() + "条记录 ");
                    richTextBox3.Focus();
                }

            }
            catch (Exception er)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox3.AppendText(er.ToString() + " ");
                  

                }));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Task.Run(() => getrpt_forshop());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int No;
            if (!int.TryParse(textBox2.Text,out No))
            {
                MessageBox.Show("非有效单号!");
                return;
            }
            if (CheckNo(No)=="0")
            {
                MessageBox.Show("非有效单号!");
                return;
            }
            //  richTextBox3.Text = GetDate(32);
            Task.Run(() => getrptforsql());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Task.Run(() => getrptforsql_shop(textBox3.Text));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Task.Run(() => getrptone()); 
        }

        private void rpt_Load_1(object sender, EventArgs e)
        {

        }
       
       
        private void ExpFromorcl()
        {
            this.Invoke(new Action(() =>
            {

                this.button2.Enabled = false;
                this.button5.Enabled = false;
                this.button6.Enabled = false;
                this.button7.Enabled = false;
                this.button8.Enabled = false;
            }));
            this.Invoke(new Action(() =>
            {
                richTextBox4.AppendText(DateTime.Now.ToString() + "--开始导出查询结果... " + "\r\n");

            }));
            if (!Directory.Exists(Application.StartupPath + "\\Excel\\"))
            {
                // Create the directory it does not exist.
                Directory.CreateDirectory(Application.StartupPath + "\\Excel\\");
            }
            DataTable dtrpt = new DataTable();
            oraexec ora = new oraexec(connstr);
            dtrpt = ora.GetDt("select * from test");

            string iyear = Std.Value.Year.ToString();
            string imonth = Std.Value.Month.ToString();
            string filepath = Application.StartupPath + "\\Excel\\" +iyear +"-"+imonth +"-"+ DateTime.Now.ToString().Replace('/', '-').Replace("-", "").Replace(":", "").Replace(" ", "") + ".csv";
            KillSpecialExcel();
            var lines = new List<string>();
            string[] columnNames = dtrpt.Columns
                                            .Cast<DataColumn>()
                                            .Select(column => column.ColumnName)
                                            .ToArray();
            var header = string.Join(",", columnNames);
            lines.Add(header);
            var valueLines = dtrpt.AsEnumerable()
                            .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);
            File.WriteAllLines(filepath, lines, Encoding.GetEncoding("gb2312"));
           
            Process.Start(filepath);

            //string result = string.Empty;
            //try
            //{
            //    // 实例化流对象，以特定的编码向流中写入字符。
            //    StreamWriter sw = new StreamWriter(filepath, false, Encoding.GetEncoding("gb2312"));

            //    StringBuilder sb = new StringBuilder();
            //    for (int k = 0; k < dtrpt.Columns.Count; k++)
            //    {
            //        // 添加列名称
            //        sb.Append(dtrpt.Columns[k].ColumnName.ToString() + "\t");
            //    }
            //    sb.Append(Environment.NewLine);
            //    // 添加行数据
            //    for (int i = 0; i < dtrpt.Rows.Count; i++)
            //    {
            //        DataRow row = dtrpt.Rows[i];
            //        for (int j = 0; j < dtrpt.Columns.Count; j++)
            //        {
            //            // 根据列数追加行数据
            //            sb.Append(row[j].ToString() + "\t");
            //        }
            //        sb.Append(Environment.NewLine);
            //    }
            //    sw.Write(sb.ToString());
            //    sw.Flush();
            //    sw.Close();
            //    sw.Dispose();

            //    // 导出成功后打开
            //    System.Diagnostics.Process.Start(filepath);

                //XSSFWorkbook excelBook = new XSSFWorkbook(); //创建工作簿Excel

                //NPOI.SS.UserModel.ISheet sheet1 = excelBook.CreateSheet("进销存");//为工作簿创建工作表并命名
                ////定义样式
                //NPOI.XSSF.UserModel.XSSFCellStyle cellStyle = (NPOI.XSSF.UserModel.XSSFCellStyle)excelBook.CreateCellStyle(); //定义一个单元格样式
                //                                                                      // 设置单元格背景色
                //                                                                      //  cellStyle.FillForegroundColor = HSSFColor.White.Index;
                //                                                                      //   cellStyle.FillPattern = FillPattern.SolidForeground;

                ////设置文字对齐方式
                //cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left; //水平居中

                ////设置字体格式开始
                //XSSFFont firstFont = (XSSFFont)excelBook.CreateFont();

                ////字体属性
                //firstFont.FontName = "幼圆";
                //firstFont.FontHeightInPoints = (short)11; //设置字体大小
                //cellStyle.SetFont(firstFont);  //设置cellStyle 样式的字体




                ////编写工作表
                //NPOI.SS.UserModel.IRow row0 = sheet1.CreateRow(0);//创建表头

                //row0.CreateCell(0).SetCellValue("大区经理");
                //row0.CreateCell(1).SetCellValue("业务主管");
                //row0.CreateCell(2).SetCellValue("店铺代码");
                //row0.CreateCell(3).SetCellValue("店铺名称");
                //row0.CreateCell(4).SetCellValue("期初数量");
                //row0.CreateCell(5).SetCellValue("入库数量");
                //row0.CreateCell(6).SetCellValue("出库数量");

                //row0.CreateCell(7).SetCellValue("期末数量");
                //row0.CreateCell(8).SetCellValue("期末库存金额");
                //row0.CreateCell(9).SetCellValue("库存成本金额");
                //row0.CreateCell(10).SetCellValue("销售上报数量");
                //row0.CreateCell(11).SetCellValue("销售上报金额");
                //row0.CreateCell(12).SetCellValue("货品编号");
                //row0.CreateCell(13).SetCellValue("货品名称");
                //row0.CreateCell(14).SetCellValue("品牌");
                //row0.CreateCell(15).SetCellValue("条码");
                //row0.CreateCell(16).SetCellValue("零售价格");
                //row0.CreateCell(17).SetCellValue("库存成本");
                //row0.CreateCell(18).SetCellValue("二类品线");
                //int i = 1;
                //dtrpt.AsEnumerable().ToList().ForEach(r =>
                //{
                //    this.Invoke(new Action(() =>
                //    {
                //        richTextBox4.AppendText(DateTime.Now.ToString() + "导出" + dtrpt.Rows.Count.ToString() + "/" + i.ToString() + "条件记录中... " + "\r\n");

                //    }));
                //    NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i);
                //    rowTemp.CreateCell(0).SetCellValue(r[0].ToString());
                //    rowTemp.CreateCell(1).SetCellValue(r[1].ToString());
                //    rowTemp.CreateCell(2).SetCellValue(r[2].ToString());
                //    rowTemp.CreateCell(3).SetCellValue(r[3].ToString());
                //    rowTemp.CreateCell(4).SetCellValue(r[4].ToString());
                //    rowTemp.CreateCell(5).SetCellValue(r[5].ToString());
                //    rowTemp.CreateCell(6).SetCellValue(r[6].ToString());
                //    rowTemp.CreateCell(7).SetCellValue(r[7].ToString());
                //    rowTemp.CreateCell(8).SetCellValue(r[8].ToString());
                //    rowTemp.CreateCell(9).SetCellValue(r[9].ToString());
                //    rowTemp.CreateCell(10).SetCellValue(r[10].ToString());
                //    rowTemp.CreateCell(11).SetCellValue(r[11].ToString());
                //    rowTemp.CreateCell(12).SetCellValue(r[12].ToString());
                //    rowTemp.CreateCell(13).SetCellValue(r[13].ToString());
                //    rowTemp.CreateCell(14).SetCellValue(r[14].ToString());
                //    rowTemp.CreateCell(15).SetCellValue(r[15].ToString());
                //    rowTemp.CreateCell(16).SetCellValue(r[16].ToString());
                //    rowTemp.CreateCell(17).SetCellValue(r[17].ToString());
                //    rowTemp.CreateCell(18).SetCellValue(r[18].ToString());

                //    i++;
                //}
                //);
                //sheet1.SetColumnWidth(0, 15 * 256);
                //sheet1.SetColumnWidth(1, 15 * 256);
                //sheet1.SetColumnWidth(2, 15 * 256);
                //sheet1.SetColumnWidth(3, 15 * 256);
                //sheet1.SetColumnWidth(4, 15 * 256);
                //sheet1.SetColumnWidth(5, 15 * 256);
                //sheet1.SetColumnWidth(6, 15 * 256);
                //sheet1.SetColumnWidth(7, 15 * 256);
                //sheet1.SetColumnWidth(8, 15 * 256);
                //sheet1.SetColumnWidth(9, 15 * 256);
                //sheet1.SetColumnWidth(10, 15 * 256);
                //sheet1.SetColumnWidth(11, 15 * 256);
                //sheet1.SetColumnWidth(12, 15 * 256);
                //sheet1.SetColumnWidth(13, 50 * 256);
                //sheet1.SetColumnWidth(14, 15 * 256);
                //sheet1.SetColumnWidth(15, 15 * 256);
                //sheet1.SetColumnWidth(16, 15 * 256);
                //sheet1.SetColumnWidth(17, 15 * 256);
                //sheet1.SetColumnWidth(18, 15 * 256);



                //using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
                //{
                //    excelBook.Write(fs);
                //}
                //object missing = Type.Missing;
                //Microsoft.Office.Interop.Excel.Application ExcelRS;
                //Microsoft.Office.Interop.Excel.Workbook RSbook;
                //Microsoft.Office.Interop.Excel.Worksheet RSsheet;
                //ExcelRS = new Microsoft.Office.Interop.Excel.Application();
                ////打开目标文件filePath
                //RSbook = ExcelRS.Workbooks.Open(filepath, missing, missing, missing, missing, missing,
                //    missing, missing, missing, missing, missing, missing, missing, missing, missing);
                //RSsheet = (Microsoft.Office.Interop.Excel.Worksheet)RSbook.Sheets.get_Item(1);

                //ExcelRS.DisplayAlerts = false;

                //ExcelRS.Visible = true;

                this.Invoke(new Action(() =>
            {
                //  richTextBox3.AppendText(newsql + " ");
                richTextBox4.AppendText(DateTime.Now.ToString() + "--提取结束 \r\n ");

                this.button5.Enabled = true;
                this.button6.Enabled = true;
                this.button7.Enabled = true;
                this.button8.Enabled = true;
                this.button2.Enabled = true;
            }));
        }
        public void gen_excel_cmd_fillWholeRowStyle(IRow r, ICellStyle cellStyle)
        {
            List<ICell> clist = r.Cells;

            foreach (var cItem in clist)
            {
                cItem.CellStyle = cellStyle;
            }
            r.RowStyle = cellStyle;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Task.Run(() => ExpFromorcl());


        }
    }
}
