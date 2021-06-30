using stocktake.SoapUnitTOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;

namespace stocktake
{

    public partial class test : Form
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public WebReference.WebService ws = new global::stocktake.WebReference.WebService();
      
        public test()
        {
            InitializeComponent();
        }
        private string getelement(string element_name)
        {
            // string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\appconfig.xml";
            string path = System.Windows.Forms.Application.StartupPath + "\\config.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode xnode = xmlDoc.GetElementsByTagName(element_name)[0];
            return xnode.InnerText;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Sqlexec sqlex = new Sqlexec(getelement("sqlconnstr"));
            DataTable dt = sqlex.SqlGetDataSet("SELECT  T1.WddCode, T1.DocEntry, T2.OrderType,T2.OwnerID ,T2.Data     FROM T_CWDD T1    LEFT JOIN T_CDRF T2 ON T1.DocEntry = T2.Code   WHERE T1.Status = N'Y'   and T2.OrderType = '1146'  AND T1.WddCode = '22979'  ");
            DataSet ds = new DataSet();
            string Data =dt.Rows[0]["Data"].ToString();
			string docentry = GetDocEntry("U_CXCL");

			using (MemoryStream ms = new MemoryStream(new UTF8Encoding().GetBytes(Data)))
            {
                ds.ReadXml(ms);
				if(!ds.Tables[0].Columns.Contains("Comment"))
                {
					ds.Tables[0].Columns.Add(new DataColumn("Comment"));
				}
				if (!ds.Tables[0].Columns.Contains("DocStatus"))
				{
					ds.Tables[0].Columns.Add(new DataColumn("DocStatus"));
				}

			
				
				dataGridView1.DataSource = ds.Tables[0];
				foreach(DataRow dr in ds.Tables[0].Rows)
                {
					dr["DocEntry"] = docentry;

				}
				foreach (DataRow dr in ds.Tables[1].Rows)
				{
					dr["DocEntry"] = docentry;

				}
				foreach (DataRow dr in ds.Tables[2].Rows)
				{
					dr["DocEntry"] = docentry;

				}
				if(!SqlBulkCopyInsertMain(getelement("sqlconnstr"), ds.Tables[0].TableName, ds.Tables[0]))
                {
					MessageBox.Show("主表插入失败");
                }
				dataGridView2.DataSource = ds.Tables[1];
				if(!SqlBulkCopyInsert(getelement("sqlconnstr"), ds.Tables[1].TableName, ds.Tables[1]))
				{
					MessageBox.Show("明细表插入失败");
				}
				dataGridView3.DataSource = ds.Tables[2];
				if (!SqlBulkCopyInsert(getelement("sqlconnstr"), ds.Tables[2].TableName, ds.Tables[2]))
				{
					MessageBox.Show("明细表1插入失败");
				}



			}

        }
		
		public  bool  SqlBulkCopyInsert(string conStr, string strTableName, DataTable dtData)
		{
			try
			{
				using (SqlBulkCopy sqlRevdBulkCopy = new SqlBulkCopy(conStr))//引用SqlBulkCopy 
				{
					sqlRevdBulkCopy.DestinationTableName = strTableName;//数据库中对应的表名 
					sqlRevdBulkCopy.NotifyAfter = dtData.Rows.Count;//有几行数据 
					sqlRevdBulkCopy.WriteToServer(dtData);//数据导入数据库 
					sqlRevdBulkCopy.Close();//关闭连接 
					return true;
				}
			}
			catch (Exception ex)
			{
				//throw (ex);
				return false;
			}
		}

		public bool SqlBulkCopyInsertMain(string conStr, string strTableName, DataTable dtData)
		{
			try
			{
				using (SqlBulkCopy sqlRevdBulkCopy = new SqlBulkCopy(conStr))//引用SqlBulkCopy 
				{
					sqlRevdBulkCopy.DestinationTableName = strTableName;//数据库中对应的表名 
					sqlRevdBulkCopy.NotifyAfter = dtData.Rows.Count;//有几行数据 
					sqlRevdBulkCopy.ColumnMappings.Add("DocEntry", "DocEntry");
					sqlRevdBulkCopy.ColumnMappings.Add("DocStatus", "DocStatus");
					sqlRevdBulkCopy.ColumnMappings.Add("SDate", "SDate");
					sqlRevdBulkCopy.ColumnMappings.Add("EDate", "EDate");
					sqlRevdBulkCopy.ColumnMappings.Add("CXName", "CXName");
					sqlRevdBulkCopy.ColumnMappings.Add("ZPCL", "ZPCL");
					sqlRevdBulkCopy.ColumnMappings.Add("UserSign", "UserSign");
					sqlRevdBulkCopy.ColumnMappings.Add("Comment", "Comment");
					sqlRevdBulkCopy.ColumnMappings.Add("PlanType", "PlanType");
					sqlRevdBulkCopy.ColumnMappings.Add("TaxDate", "TaxDate");
					sqlRevdBulkCopy.WriteToServer(dtData);//数据导入数据库 
					sqlRevdBulkCopy.Close();//关闭连接 
					return true;
				}
			}
			catch (Exception ex)
			{
				//throw (ex);
				return false;
			}
		}




		private string GetDocEntry(string orderType)
		{
			string sql = $"select AutoKey  from T_CNNM where ObjCode='{orderType}'";
			using (SqlConnection conn = new SqlConnection(getelement("sqlconnstr")))
			{
				conn.Open();
				SqlCommand smd = new SqlCommand(sql, conn);
				return smd.ExecuteScalar().ToString();

			}
		}
		private int UpDocEntry(string orderType)
		{
			string sql = $"update T_CNNM set  AutoKey=AutoKey+1   where ObjCode='{orderType}'";
			using (SqlConnection conn = new SqlConnection(getelement("sqlconnstr")))
			{
				conn.Open();
				SqlCommand smd = new SqlCommand(sql, conn);
				return smd.ExecuteNonQuery();

			}
		}
		private void test_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(ConfigurationManager.AppSettings["Q_Interval"].ToString());
          
            // var str = ConfigurationManager.AppSettings;
            //for(int i=0;i<str.Count;i++)
            //{
            //    richTextBox1.AppendText(str[i]);
            //}

        }
        // HccDIServer.Services.Order
        //internal DocInfo OrderSave(int UserSign, int OrderType, DataSet ds, int DraftOrder, FormMode frmMode = FormMode.fm_ADD_MODE)
        //{
        //	string Guid = Util.GetGuid().ToString();
        //		Order.log.Info(string.Concat(new object[]
        //		{
        //	"OrderSave  UserSign:",
        //	UserSign,
        //	" OrderType:",
        //	OrderType,
        //	" 开始提交",
        //	Guid
        //		}));
        //	DocInfo di = new DocInfo();
        //	IOrderObject ioo = ProxyInterface.OrderObjectCatch();
        //	IUdo udo = ProxyInterface.UdoInterfaceCache();
        //	try
        //	{
        //		OrderType DocType = (OrderType)Enum.Parse(typeof(OrderType), OrderType.ToString());
        //		UdoInfo _UdoInfo = UnitHelper.GetUdoInfo(OrderType);
        //		string udoName = string.Empty;
        //		string masterTable = string.Empty;
        //		string detailTable = string.Empty;
        //		string freightTable = string.Empty;
        //		string keyField = string.Empty;
        //		int newDocEntry = -1;
        //		string sbSql = string.Empty;
        //		string tmpdetailTable = string.Empty;
        //		if (DocType == OrderType.NONE)
        //		{
        //			throw new FaultException("未知单据类型,操作取消!");
        //		}
        //		OrderType tmpOrder = TransOrderType.TansByOtType(DocType);
        //		bool isOrderObject;
        //		bool isSaleOrder;
        //		if (tmpOrder <= OrderType.Supplier)
        //		{
        //			if (tmpOrder <= OrderType.GoodsIssue)
        //			{
        //				switch (tmpOrder)
        //				{
        //					case OrderType.SaleOrder:
        //						Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //						udoName = _UdoInfo.UdoID;
        //						isOrderObject = true;
        //						isSaleOrder = true;
        //						sbSql = string.Empty;
        //						if (string.IsNullOrEmpty(masterTable))
        //						{
        //							masterTable = _UdoInfo.MasterTableName;
        //						}
        //						if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //						{
        //							detailTable = _UdoInfo.DetailTables[0];
        //						}
        //						keyField = _UdoInfo.MasterKeyField;
        //						goto IL_D19;
        //					case OrderType.PurchaseOrder:
        //						Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //						udoName = _UdoInfo.UdoID;
        //						isOrderObject = true;
        //						isSaleOrder = false;
        //						sbSql = string.Empty;
        //						if (string.IsNullOrEmpty(masterTable))
        //						{
        //							masterTable = _UdoInfo.MasterTableName;
        //						}
        //						if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //						{
        //							detailTable = _UdoInfo.DetailTables[0];
        //						}
        //						keyField = _UdoInfo.MasterKeyField;
        //						goto IL_D19;
        //					case OrderType.ApproveTemplet:
        //					case OrderType.Activity:
        //					case OrderType.Quotations:
        //						break;
        //					case OrderType.SalesOpp:
        //						Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //						udoName = _UdoInfo.UdoID;
        //						isOrderObject = false;
        //						isSaleOrder = false;
        //						sbSql = string.Empty;
        //						if (string.IsNullOrEmpty(masterTable))
        //						{
        //							masterTable = _UdoInfo.MasterTableName;
        //						}
        //						if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //						{
        //							detailTable = _UdoInfo.DetailTables[0];
        //						}
        //						keyField = _UdoInfo.MasterKeyField;
        //						goto IL_D19;
        //					case OrderType.Delivery:
        //						Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //						udoName = _UdoInfo.UdoID;
        //						isOrderObject = true;
        //						isSaleOrder = true;
        //						sbSql = string.Empty;
        //						if (string.IsNullOrEmpty(masterTable))
        //						{
        //							masterTable = _UdoInfo.MasterTableName;
        //						}
        //						if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //						{
        //							detailTable = _UdoInfo.DetailTables[0];
        //						}
        //						keyField = _UdoInfo.MasterKeyField;
        //						goto IL_D19;
        //					case OrderType.Returns:
        //						Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //						udoName = _UdoInfo.UdoID;
        //						isOrderObject = true;
        //						isSaleOrder = true;
        //						sbSql = string.Empty;
        //						if (string.IsNullOrEmpty(masterTable))
        //						{
        //							masterTable = _UdoInfo.MasterTableName;
        //						}
        //						if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //						{
        //							detailTable = _UdoInfo.DetailTables[0];
        //						}
        //						keyField = _UdoInfo.MasterKeyField;
        //						goto IL_D19;
        //					default:
        //						switch (tmpOrder)
        //						{
        //							case OrderType.GoodsRpt:
        //								Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //								udoName = _UdoInfo.UdoID;
        //								isOrderObject = true;
        //								isSaleOrder = false;
        //								sbSql = string.Empty;
        //								if (string.IsNullOrEmpty(masterTable))
        //								{
        //									masterTable = _UdoInfo.MasterTableName;
        //								}
        //								if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //								{
        //									detailTable = _UdoInfo.DetailTables[0];
        //								}
        //								keyField = _UdoInfo.MasterKeyField;
        //								goto IL_D19;
        //							case OrderType.GoodsRtn:
        //								Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //								udoName = _UdoInfo.UdoID;
        //								isOrderObject = true;
        //								isSaleOrder = false;
        //								sbSql = string.Empty;
        //								if (string.IsNullOrEmpty(masterTable))
        //								{
        //									masterTable = _UdoInfo.MasterTableName;
        //								}
        //								if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //								{
        //									detailTable = _UdoInfo.DetailTables[0];
        //								}
        //								keyField = _UdoInfo.MasterKeyField;
        //								goto IL_D19;
        //							case OrderType.WhsTrans:
        //								Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //								udoName = _UdoInfo.UdoID;
        //								isOrderObject = true;
        //								isSaleOrder = false;
        //								sbSql = string.Empty;
        //								if (string.IsNullOrEmpty(masterTable))
        //								{
        //									masterTable = _UdoInfo.MasterTableName;
        //								}
        //								if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //								{
        //									detailTable = _UdoInfo.DetailTables[0];
        //								}
        //								keyField = _UdoInfo.MasterKeyField;
        //								goto IL_D19;
        //							case OrderType.Invoices:
        //								Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //								udoName = _UdoInfo.UdoID;
        //								isOrderObject = true;
        //								isSaleOrder = false;
        //								sbSql = string.Empty;
        //								if (string.IsNullOrEmpty(masterTable))
        //								{
        //									masterTable = _UdoInfo.MasterTableName;
        //								}
        //								if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //								{
        //									detailTable = _UdoInfo.DetailTables[0];
        //								}
        //								keyField = _UdoInfo.MasterKeyField;
        //								goto IL_D19;
        //							case OrderType.CreditInvc:
        //								Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //								udoName = _UdoInfo.UdoID;
        //								isOrderObject = true;
        //								isSaleOrder = true;
        //								sbSql = string.Empty;
        //								if (string.IsNullOrEmpty(masterTable))
        //								{
        //									masterTable = _UdoInfo.MasterTableName;
        //								}
        //								if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //								{
        //									detailTable = _UdoInfo.DetailTables[0];
        //								}
        //								keyField = _UdoInfo.MasterKeyField;
        //								goto IL_D19;
        //							case OrderType.APInvoices:
        //								Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //								udoName = _UdoInfo.UdoID;
        //								isOrderObject = true;
        //								isSaleOrder = false;
        //								sbSql = string.Empty;
        //								if (string.IsNullOrEmpty(masterTable))
        //								{
        //									masterTable = _UdoInfo.MasterTableName;
        //								}
        //								if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //								{
        //									detailTable = _UdoInfo.DetailTables[0];
        //								}
        //								keyField = _UdoInfo.MasterKeyField;
        //								goto IL_D19;
        //							case OrderType.InComPayment:
        //								Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //								udoName = _UdoInfo.UdoID;
        //								isOrderObject = true;
        //								isSaleOrder = false;
        //								sbSql = string.Empty;
        //								if (string.IsNullOrEmpty(masterTable))
        //								{
        //									masterTable = _UdoInfo.MasterTableName;
        //								}
        //								if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //								{
        //									detailTable = _UdoInfo.DetailTables[0];
        //								}
        //								keyField = _UdoInfo.MasterKeyField;
        //								goto IL_D19;
        //							case OrderType.BPartners:
        //								Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //								udoName = _UdoInfo.UdoID;
        //								isOrderObject = false;
        //								isSaleOrder = false;
        //								sbSql = string.Empty;
        //								if (string.IsNullOrEmpty(masterTable))
        //								{
        //									masterTable = _UdoInfo.MasterTableName;
        //								}
        //								if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //								{
        //									detailTable = _UdoInfo.DetailTables[0];
        //								}
        //								keyField = _UdoInfo.MasterKeyField;
        //								goto IL_D19;
        //							case OrderType.GoodsReceipt:
        //								Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //								udoName = _UdoInfo.UdoID;
        //								isOrderObject = true;
        //								isSaleOrder = false;
        //								sbSql = string.Empty;
        //								if (string.IsNullOrEmpty(masterTable))
        //								{
        //									masterTable = _UdoInfo.MasterTableName;
        //								}
        //								if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //								{
        //									detailTable = _UdoInfo.DetailTables[0];
        //								}
        //								keyField = _UdoInfo.MasterKeyField;
        //								goto IL_D19;
        //							case OrderType.GoodsIssue:
        //								Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //								udoName = _UdoInfo.UdoID;
        //								isOrderObject = true;
        //								isSaleOrder = false;
        //								sbSql = string.Empty;
        //								if (string.IsNullOrEmpty(masterTable))
        //								{
        //									masterTable = _UdoInfo.MasterTableName;
        //								}
        //								if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //								{
        //									detailTable = _UdoInfo.DetailTables[0];
        //								}
        //								keyField = _UdoInfo.MasterKeyField;
        //								goto IL_D19;
        //						}
        //						break;
        //				}
        //			}
        //			else
        //			{
        //				if (tmpOrder == OrderType.Items)
        //				{
        //					Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //					udoName = _UdoInfo.UdoID;
        //					isOrderObject = false;
        //					isSaleOrder = false;
        //					sbSql = string.Empty;
        //					if (string.IsNullOrEmpty(masterTable))
        //					{
        //						masterTable = _UdoInfo.MasterTableName;
        //					}
        //					if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //					{
        //						detailTable = _UdoInfo.DetailTables[0];
        //					}
        //					keyField = _UdoInfo.MasterKeyField;
        //					goto IL_D19;
        //				}
        //				if (tmpOrder == OrderType.UpDateRate)
        //				{
        //					Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //					udoName = _UdoInfo.UdoID;
        //					isOrderObject = false;
        //					isSaleOrder = false;
        //					sbSql = string.Empty;
        //					if (string.IsNullOrEmpty(masterTable))
        //					{
        //						masterTable = _UdoInfo.MasterTableName;
        //					}
        //					keyField = _UdoInfo.MasterKeyField;
        //					goto IL_D19;
        //				}
        //				if (tmpOrder == OrderType.Supplier)
        //				{
        //					Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //					udoName = _UdoInfo.UdoID;
        //					isOrderObject = false;
        //					isSaleOrder = false;
        //					sbSql = string.Empty;
        //					if (string.IsNullOrEmpty(masterTable))
        //					{
        //						masterTable = _UdoInfo.MasterTableName;
        //					}
        //					if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //					{
        //						detailTable = _UdoInfo.DetailTables[0];
        //					}
        //					keyField = _UdoInfo.MasterKeyField;
        //					goto IL_D19;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			if (tmpOrder <= OrderType.WorkConfirm)
        //			{
        //				if (tmpOrder == OrderType.WorkReport)
        //				{
        //					Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //					udoName = _UdoInfo.UdoID;
        //					isOrderObject = false;
        //					isSaleOrder = false;
        //					sbSql = string.Empty;
        //					if (string.IsNullOrEmpty(masterTable))
        //					{
        //						masterTable = _UdoInfo.MasterTableName;
        //					}
        //					if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //					{
        //						detailTable = _UdoInfo.DetailTables[0];
        //					}
        //					keyField = _UdoInfo.MasterKeyField;
        //					goto IL_D19;
        //				}
        //				if (tmpOrder == OrderType.WorkConfirm)
        //				{
        //					Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //					udoName = _UdoInfo.UdoID;
        //					isOrderObject = false;
        //					isSaleOrder = false;
        //					sbSql = string.Empty;
        //					if (string.IsNullOrEmpty(masterTable))
        //					{
        //						masterTable = _UdoInfo.MasterTableName;
        //					}
        //					if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //					{
        //						detailTable = _UdoInfo.DetailTables[0];
        //					}
        //					keyField = _UdoInfo.MasterKeyField;
        //					goto IL_D19;
        //				}
        //			}
        //			else
        //			{
        //				if (tmpOrder == OrderType.SrvceDPayAR)
        //				{
        //					Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //					udoName = _UdoInfo.UdoID;
        //					isOrderObject = true;
        //					isSaleOrder = false;
        //					sbSql = string.Empty;
        //					if (string.IsNullOrEmpty(masterTable))
        //					{
        //						masterTable = _UdoInfo.MasterTableName;
        //					}
        //					if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //					{
        //						detailTable = _UdoInfo.DetailTables[0];
        //					}
        //					keyField = _UdoInfo.MasterKeyField;
        //					goto IL_D19;
        //				}
        //				switch (tmpOrder)
        //				{
        //					case OrderType.ApplyReturn:
        //						Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //						udoName = _UdoInfo.UdoID;
        //						isOrderObject = false;
        //						isSaleOrder = false;
        //						sbSql = string.Empty;
        //						if (string.IsNullOrEmpty(masterTable))
        //						{
        //							masterTable = _UdoInfo.MasterTableName;
        //						}
        //						if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //						{
        //							detailTable = _UdoInfo.DetailTables[0];
        //						}
        //						keyField = _UdoInfo.MasterKeyField;
        //						goto IL_D19;
        //					case OrderType.ApplyOtherIn:
        //						Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //						udoName = _UdoInfo.UdoID;
        //						isOrderObject = false;
        //						isSaleOrder = false;
        //						sbSql = string.Empty;
        //						if (string.IsNullOrEmpty(masterTable))
        //						{
        //							masterTable = _UdoInfo.MasterTableName;
        //						}
        //						if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //						{
        //							detailTable = _UdoInfo.DetailTables[0];
        //						}
        //						keyField = _UdoInfo.MasterKeyField;
        //						goto IL_D19;
        //					case OrderType.ApplyOtherOut:
        //						Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //						udoName = _UdoInfo.UdoID;
        //						isOrderObject = false;
        //						isSaleOrder = false;
        //						sbSql = string.Empty;
        //						if (string.IsNullOrEmpty(masterTable))
        //						{
        //							masterTable = _UdoInfo.MasterTableName;
        //						}
        //						if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //						{
        //							detailTable = _UdoInfo.DetailTables[0];
        //						}
        //						keyField = _UdoInfo.MasterKeyField;
        //						goto IL_D19;
        //					default:
        //						switch (tmpOrder)
        //						{
        //							case OrderType.HCC_LoanBill:
        //							case OrderType.HCC_FeeExpenseBill:
        //							case OrderType.HCC_RePayBill:
        //								Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //								udoName = _UdoInfo.UdoID;
        //								isOrderObject = false;
        //								isSaleOrder = false;
        //								sbSql = string.Empty;
        //								if (string.IsNullOrEmpty(masterTable))
        //								{
        //									masterTable = _UdoInfo.MasterTableName;
        //								}
        //								if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //								{
        //									detailTable = _UdoInfo.DetailTables[0];
        //								}
        //								keyField = _UdoInfo.MasterKeyField;
        //								goto IL_D19;
        //						}
        //						break;
        //				}
        //			}
        //		}
        //		if (OrderType >= 1000 && OrderType < 2000)
        //		{
        //			Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //			udoName = _UdoInfo.UdoID;
        //			isOrderObject = false;
        //			isSaleOrder = false;
        //			sbSql = string.Empty;
        //			if (string.IsNullOrEmpty(masterTable))
        //			{
        //				masterTable = _UdoInfo.MasterTableName;
        //			}
        //			if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //			{
        //				detailTable = _UdoInfo.DetailTables[0];
        //			}
        //			keyField = _UdoInfo.MasterKeyField;
        //		}
        //		else
        //		{
        //			Orders.OrderTableName(tmpOrder, ref masterTable, ref detailTable, ref freightTable);
        //			udoName = _UdoInfo.UdoID;
        //			isOrderObject = true;
        //			isSaleOrder = false;
        //			sbSql = string.Empty;
        //			if (string.IsNullOrEmpty(masterTable))
        //			{
        //				masterTable = _UdoInfo.MasterTableName;
        //			}
        //			if (string.IsNullOrEmpty(detailTable) && _UdoInfo.DetailTables.Count > 0)
        //			{
        //				detailTable = _UdoInfo.DetailTables[0];
        //			}
        //			keyField = _UdoInfo.MasterKeyField;
        //		}
        //	IL_D19:
        //		if (string.IsNullOrEmpty(udoName) && string.IsNullOrEmpty(keyField))
        //		{
        //			udoName = _UdoInfo.UdoID;
        //			keyField = _UdoInfo.MasterKeyField;
        //			masterTable = _UdoInfo.MasterTableName;
        //		}
        //		DataSet dsUdo = ds;
        //		if (frmMode == FormMode.fm_ADD_MODE)
        //		{
        //			if (TempletCache.UdoTableStruct.ContainsKey(_UdoInfo.UdoID))
        //			{
        //				dsUdo = TempletCache.UdoTableStruct[_UdoInfo.UdoID].Clone();
        //			}
        //			else
        //			{
        //				tmpdetailTable = detailTable;
        //				if (string.IsNullOrEmpty(detailTable))
        //				{
        //					tmpdetailTable = masterTable;
        //				}
        //				if (!string.IsNullOrEmpty(udoName) && !string.IsNullOrEmpty(keyField))
        //				{
        //					dsUdo = ioo.GetOrderEmptyDataSet(UserSign, udoName, DocType, isOrderObject, masterTable, tmpdetailTable, freightTable, keyField, isSaleOrder);
        //				}
        //				else
        //				{
        //					dsUdo = ProxyInterface.UdoInterfaceCache().GetEmptyDataSet(_UdoInfo.UdoID, _UdoInfo.MasterKeyField);
        //				}
        //				if (DocType == OrderType.InComPayment && dsUdo.Tables.Contains("RCT2") && !dsUdo.Tables["RCT2"].Columns.Contains("DocCur"))
        //				{
        //					dsUdo.Tables["RCT2"].Columns.Add("DocCur");
        //				}
        //				TempletCache.UdoTableStruct.Add(_UdoInfo.UdoID, dsUdo.Clone());
        //			}
        //			this.CopyDataSet(ds, dsUdo, _UdoInfo.MasterTableName, _UdoInfo.DetailTables.ToArray());
        //			UnitHelper.SetLineID(dsUdo, _UdoInfo, _UdoInfo.DetailIdentityField);
        //			if (frmMode == FormMode.fm_ADD_MODE)
        //			{
        //				dsUdo.AcceptChanges();
        //				IEnumerator enumerator = dsUdo.Tables.GetEnumerator();
        //				try
        //				{
        //					while (enumerator.MoveNext())
        //					{
        //						IEnumerator enumerator2 = ((DataTable)enumerator.Current).Rows.GetEnumerator();
        //						try
        //						{
        //							while (enumerator2.MoveNext())
        //							{
        //								((DataRow)enumerator2.Current).SetAdded();
        //							}
        //						}
        //						finally
        //						{
        //							IDisposable disposable = enumerator2 as IDisposable;
        //							if (disposable != null)
        //							{
        //								disposable.Dispose();
        //							}
        //						}
        //					}
        //				}
        //				finally
        //				{
        //					IDisposable disposable = enumerator as IDisposable;
        //					if (disposable != null)
        //					{
        //						disposable.Dispose();
        //					}
        //				}
        //			}
        //			if (tmpOrder != OrderType.SaleOrder)
        //			{
        //				if (tmpOrder != OrderType.InSStcTrans)
        //				{
        //					switch (tmpOrder)
        //					{
        //						case OrderType.HCC_LoanBill:
        //							{
        //								Hashtable hst = new Hashtable();
        //								DataTable dtDetail = dsUdo.Tables["T_LOAN1"];
        //								if (dtDetail == null || dtDetail.Rows.Count == 0)
        //								{
        //									DocInfo result = null;
        //									return result;
        //								}
        //								StringBuilder sbSql2 = new StringBuilder();
        //								if (frmMode == FormMode.fm_ADD_MODE)
        //								{
        //									foreach (DataRow dr in dtDetail.Rows)
        //									{
        //										OrderType ot = OrderType.NONE;
        //										string BaseEntry = dr["BaseEntry"].ToString();
        //										string BaseLine = dr["BaseLine"].ToString();
        //										if (dr["BaseType"].ToString() == "901" && !string.IsNullOrEmpty(BaseEntry) && !string.IsNullOrEmpty(BaseLine))
        //										{
        //											ot = OrderType.HCC_LoanApplyBill;
        //										}
        //										if (ot == OrderType.HCC_LoanApplyBill)
        //										{
        //											if (!hst.Contains(dr["BaseEntry"].ToString()))
        //											{
        //												hst.Add(dr["BaseEntry"].ToString(), dr["BaseEntry"].ToString());
        //											}
        //											if (sbSql2.Length > 0)
        //											{
        //												sbSql2.Append("    ");
        //											}
        //											sbSql2.Append(" UPDATE T_LOAP1 SET OpenSum = ISNULL(OpenSum,0) - ").Append(dr["LoanSum"].ToString()).Append(" WHERE DocEntry = ").Append(dr["BaseEntry"].ToString()).Append(" AND LineNum = ").Append(dr["BaseLine"].ToString());
        //										}
        //									}
        //									if (sbSql2.Length > 0)
        //									{
        //										foreach (string sDocEntry in hst.Keys)
        //										{
        //											sbSql2.Append("    UPDATE T_LOAP1 SET OpenSum = 0,LineStatus='C' WHERE DocEntry = ").Append(sDocEntry).Append(" AND ISNULL(OpenSum,0) <= 0 ");
        //											sbSql2.Append("    UPDATE T_LOAP SET DocStatus = 'C' WHERE DocEntry = ").Append(sDocEntry).Append(" AND (SELECT COUNT(DocEntry) FROM T_LOAP1 WHERE DocEntry = ").Append(sDocEntry).Append(" AND ISNULL(OpenSum,0) > 0) = 0");
        //										}
        //									}
        //									if (sbSql2.Length > 0)
        //									{
        //										sbSql = sbSql2.ToString();
        //									}
        //								}
        //								break;
        //							}
        //						case OrderType.HCC_FeeExpenseBill:
        //							{
        //								StringBuilder sbSql2 = new StringBuilder();
        //								DataTable dtMaster = dsUdo.Tables["T_NFEE"];
        //								DataTable dtDetail = dsUdo.Tables["T_NFEE1"];
        //								DataTable dtDetail2 = dsUdo.Tables["T_NFEE2"];
        //								OrderType otLoan = OrderType.NONE;
        //								if (dtDetail2.Rows.Count > 0)
        //								{
        //									otLoan = OrderType.HCC_LoanBill;
        //								}
        //								if (frmMode == FormMode.fm_ADD_MODE)
        //								{
        //									if (otLoan == OrderType.HCC_LoanBill)
        //									{
        //										foreach (DataRow dr2 in dtDetail.Rows)
        //										{
        //											if (sbSql2.Length > 0)
        //											{
        //												sbSql2.Append("    ");
        //											}
        //											string arg_1065_0 = dr2["LoanEntry"].ToString();
        //											string LoanLine = dr2["LoanLine"].ToString();
        //											if (!string.IsNullOrEmpty(arg_1065_0) && !string.IsNullOrEmpty(LoanLine))
        //											{
        //												sbSql2.Append(" UPDATE T_LOAN1 SET VertSum =ISNULL(VertSum,0)+ ").Append(dr2["VertSum"].ToString()).Append(" WHERE DocEntry = ").Append(dr2["LoanEntry"].ToString()).Append(" AND LineNum = ").Append(dr2["LoanLine"].ToString());
        //											}
        //										}
        //										string empID = dtMaster.Rows[0]["EmpID"].ToString();
        //										foreach (DataRow dr3 in dtDetail2.Rows)
        //										{
        //											string VertSum = dr3["VertSum"].ToString();
        //											if (string.IsNullOrEmpty(VertSum))
        //											{
        //												VertSum = "0";
        //											}
        //											sbSql2.Append("    UPDATE T_LOAN SET VertToDay=ISNULL(VertToDay,0)+" + VertSum + " WHERE DocEntry = ").Append(dr3["LoanEntry"].ToString());
        //											sbSql2.Append("  UPDATE T_OHEM SET U_Banance=ISNULL(U_Banance,0)-" + VertSum + " WHERE empID=" + empID);
        //										}
        //										dtDetail.Rows[0]["DocEntry"].ToString();
        //									}
        //									if (dtDetail == null || dtDetail.Rows.Count == 0)
        //									{
        //										Hashtable hst2 = new Hashtable();
        //										foreach (DataRow dr4 in dtDetail.Rows)
        //										{
        //											OrderType otFee = OrderType.NONE;
        //											string arg_125E_0 = dr4["BaseType"].ToString();
        //											string BaseEntry2 = dr4["BaseEntry"].ToString();
        //											string BaseLine2 = dr4["BaseLine"].ToString();
        //											if (TransOrderType.TansByOtTypeByString(arg_125E_0) == "HCC_FeeApplyBill" && !string.IsNullOrEmpty(BaseEntry2) && !string.IsNullOrEmpty(BaseLine2))
        //											{
        //												otFee = OrderType.HCC_FeeApplyBill;
        //											}
        //											if (otFee == OrderType.HCC_FeeApplyBill)
        //											{
        //												if (!hst2.Contains(dr4["BaseEntry"].ToString()))
        //												{
        //													hst2.Add(dr4["BaseEntry"].ToString(), dr4["BaseEntry"].ToString());
        //												}
        //												if (sbSql2.Length > 0)
        //												{
        //													sbSql2.Append("    ");
        //												}
        //												sbSql2.Append("UPDATE T_OFAP1 SET OpenSum =ISNULL(OpenSum,0)- ").Append(dr4["FeeSum"].ToString()).Append(" WHERE DocEntry = ").Append(dr4["BaseEntry"].ToString()).Append(" AND LineNum = ").Append(dr4["BaseLine"].ToString());
        //											}
        //										}
        //										if (sbSql2.Length > 0)
        //										{
        //											foreach (string sDocEntry2 in hst2.Keys)
        //											{
        //												sbSql2.Append("    UPDATE T_OFAP1 SET OpenSum = 0,LineStatus='C'  WHERE DocEntry = ").Append(sDocEntry2).Append(" AND ISNULL(OpenSum,0)<=0 ");
        //												sbSql2.Append("    UPDATE T_OFAP SET DocStatus = 'C' WHERE DocEntry = ").Append(sDocEntry2).Append(" AND (SELECT COUNT(DocEntry) FROM T_OFAP1 WHERE DocEntry = ").Append(sDocEntry2).Append(" AND ISNULL(OpenSum,0) >0) =0 ");
        //											}
        //										}
        //									}
        //								}
        //								if (sbSql2.Length > 0)
        //								{
        //									sbSql = sbSql2.ToString();
        //								}
        //								break;
        //							}
        //						case OrderType.HCC_RePayBill:
        //							{
        //								Hashtable hst3 = new Hashtable();
        //								DataTable dtDetail = dsUdo.Tables["T_REPY1"];
        //								if (dtDetail == null || dtDetail.Rows.Count == 0)
        //								{
        //									DocInfo result = null;
        //									return result;
        //								}
        //								StringBuilder sbSql2 = new StringBuilder();
        //								if (frmMode == FormMode.fm_ADD_MODE)
        //								{
        //									foreach (DataRow dr5 in dtDetail.Rows)
        //									{
        //										OrderType ot2 = OrderType.NONE;
        //										string BaseEntry3 = dr5["BaseEntry"].ToString();
        //										string BaseLine3 = dr5["BaseLine"].ToString();
        //										if (dr5["BaseType"].ToString() == "905" && !string.IsNullOrEmpty(BaseEntry3) && !string.IsNullOrEmpty(BaseLine3))
        //										{
        //											ot2 = OrderType.HCC_RePayApplyBill;
        //										}
        //										if (ot2 == OrderType.HCC_RePayApplyBill)
        //										{
        //											if (!hst3.Contains(dr5["BaseEntry"].ToString()))
        //											{
        //												hst3.Add(dr5["BaseEntry"].ToString(), dr5["BaseEntry"].ToString());
        //											}
        //											if (sbSql2.Length > 0)
        //											{
        //												sbSql2.Append("    ");
        //											}
        //											sbSql2.Append("  UPDATE T_REAY1 SET OpenSum = ISNULL(OpenSum,0) - ").Append(dr5["PaySum"].ToString()).Append(" WHERE DocEntry = ").Append(dr5["BaseEntry"].ToString()).Append(" AND LineNum = ").Append(dr5["BaseLine"].ToString());
        //											sbSql2.Append("  UPDATE T_REAY1 SET OpenSum = 0,LineStatus='C' WHERE DocEntry = ").Append(dr5["BaseEntry"].ToString()).Append(" AND LineNum = ").Append(dr5["BaseLine"].ToString()).Append(" AND ISNULL(OpenSum,0) <= 0  ");
        //										}
        //									}
        //									if (sbSql2.Length > 0)
        //									{
        //										foreach (string sDocEntry3 in hst3.Keys)
        //										{
        //											sbSql2.Append("    UPDATE T_REAY SET DocStatus = 'C' WHERE DocEntry = ").Append(sDocEntry3).Append(" AND (SELECT COUNT(DocEntry) FROM T_REAY1 WHERE DocEntry = ").Append(sDocEntry3).Append(" AND ISNULL(OpenSum,0) > 0) = 0");
        //										}
        //									}
        //								}
        //								if (sbSql2.Length > 0)
        //								{
        //									sbSql = sbSql2.ToString();
        //								}
        //								break;
        //							}
        //					}
        //				}
        //				else
        //				{
        //					if (!dsUdo.Tables.Contains("OSRN"))
        //					{
        //						dsUdo.Tables.Add("OSRN");
        //					}
        //					if (!dsUdo.Tables.Contains("OBTN"))
        //					{
        //						dsUdo.Tables.Add("OBTN");
        //					}
        //				}
        //			}
        //			else
        //			{
        //				this.BeforeDataBinding(dsUdo, masterTable, detailTable, freightTable);
        //			}
        //		}
        //		if (string.IsNullOrEmpty(detailTable))
        //		{
        //		}
        //		string[] SqlExcute = null;
        //		if (sbSql != null && sbSql.Length > 0)
        //		{
        //			SqlExcute = new string[]
        //			{
        //		sbSql.ToString()
        //			};
        //		}
        //		if (_UdoInfo.IsSbo)
        //		{
        //			string U_SyncID = string.Empty;
        //			if (dsUdo.Tables[masterTable].Columns.Contains("U_SyncID"))
        //			{
        //				U_SyncID = Guid.NewGuid().ToString();
        //				dsUdo.Tables[masterTable].Rows[0]["U_SyncID"] = U_SyncID;
        //			}
        //			if (dsUdo.Tables[masterTable].Columns.Contains("U_StationID"))
        //			{
        //				dsUdo.Tables[masterTable].Rows[0]["U_StationID"] = "2";
        //			}
        //			if (DocType == OrderType.UpDateRate)
        //			{
        //				foreach (DataRow expr_1A2D in dsUdo.Tables[0].Rows)
        //				{
        //					string Currency = expr_1A2D["Currency"].ToString();
        //					string RateDate = expr_1A2D["RateDate"].ToString();
        //					string Rate = expr_1A2D["Rate"].ToString();
        //					string ShopCurrency = expr_1A2D["ShopCurrency"].ToString();
        //					QueryAnalysis.ExecuteNonQuery(string.Format("IF EXISTS (SELECT 1 FROM T_ORTT WHERE Currency =N'{0}' AND  RateDate ='{1}')\r\n                                    BEGIN\r\n                                        UPDATE T_ORTT SET Rate='{3}', ShopCurrency='{2}'  WHERE Currency =N'{0}' AND  RateDate ='{1}'\r\n                                    END \r\n                                ELSE\r\n                                    BEGIN\r\n                                        INSERT T_ORTT (RateDate,Currency,ShopCurrency,Rate,UpdateTime) VALUES  ('{1}','{0}','{2}','{3}',GETDATE())\t\t\r\n                                    END", new object[]
        //					{
        //				Currency,
        //				RateDate,
        //				ShopCurrency,
        //				Rate
        //					}));
        //				}
        //				if (DTOInterface.SboServer.SBOSave(null, dsUdo, DocType, -1, null) == null)
        //				{
        //					di.Result = true;
        //					di.ResultDesc = " 保存成功";
        //					di.OrderType = (int)DocType;
        //					di.DocEntry = newDocEntry.ToString();
        //					if (!string.IsNullOrEmpty(U_SyncID))
        //					{
        //						DataTable dt = QueryAnalysis.ExecuteQuery(string.Format("SELECT TOP 1 {0} FROM {1} NOLOCK WHERE U_SyncID='{2}'", keyField, masterTable, U_SyncID)).Tables[0];
        //						if (dt != null && dt.Rows.Count == 1)
        //						{
        //							di.DocEntry = dt.Rows[0][0].ToString();
        //						}
        //					}
        //				}
        //				else
        //				{
        //					di.Result = false;
        //					di.ResultDesc = _UdoInfo.FormCaption + " 保存失败";
        //				}
        //			}
        //			else
        //			{
        //				if (DTOInterface.SboServer.SBOSave(null, dsUdo, DocType, DraftOrder, SqlExcute) == null)
        //				{
        //					di.Result = true;
        //					di.ResultDesc = " 保存成功";
        //					di.OrderType = (int)DocType;
        //					di.DocEntry = newDocEntry.ToString();
        //					if (!string.IsNullOrEmpty(U_SyncID))
        //					{
        //						DataTable dt2 = QueryAnalysis.ExecuteQuery(string.Format("SELECT TOP 1 {0} FROM {1} NOLOCK WHERE U_SyncID='{2}'", keyField, masterTable, U_SyncID)).Tables[0];
        //						if (dt2 != null && dt2.Rows.Count == 1)
        //						{
        //							di.DocEntry = dt2.Rows[0][0].ToString();
        //						}
        //					}
        //				}
        //				else
        //				{
        //					di.Result = false;
        //					di.ResultDesc = _UdoInfo.FormCaption + " 保存失败";
        //				}
        //			}
        //		}
        //		else
        //		{
        //			DataSet dsTmp = dsUdo.Clone();
        //			foreach (DataTable dt3 in dsUdo.Tables)
        //			{
        //				dsTmp.Tables[dt3.TableName].Merge(dt3);
        //			}
        //			foreach (DataTable dtTable in dsUdo.Tables)
        //			{
        //				List<string> dict_expression = new List<string>();
        //				foreach (DataColumn dcColumn in dtTable.Columns)
        //				{
        //					string expression = dcColumn.Expression;
        //					if (!string.IsNullOrEmpty(expression))
        //					{
        //						if (expression.IndexOf("Child", StringComparison.OrdinalIgnoreCase) >= 0)
        //						{
        //							dcColumn.Expression = "";
        //						}
        //						else
        //						{
        //							if (expression.IndexOf("Parent", StringComparison.OrdinalIgnoreCase) >= 0)
        //							{
        //								dcColumn.Expression = "";
        //							}
        //							else
        //							{
        //								if (!string.IsNullOrEmpty(expression) && expression.Length > 0)
        //								{
        //									dict_expression.Add(dcColumn.ColumnName);
        //									dcColumn.Expression = null;
        //									dcColumn.ReadOnly = false;
        //								}
        //							}
        //						}
        //					}
        //				}
        //				if (dict_expression.Count > 0)
        //				{
        //					int i = 0;
        //					foreach (DataRow row in dtTable.Rows)
        //					{
        //						if (row.RowState == DataRowState.Modified)
        //						{
        //							foreach (DataColumn col in dtTable.Columns)
        //							{
        //								row[col.ColumnName] = dsTmp.Tables[dtTable.TableName].Rows[i][col.ColumnName, DataRowVersion.Original];
        //							}
        //							row.AcceptChanges();
        //							foreach (DataColumn col2 in dtTable.Columns)
        //							{
        //								row[col2.ColumnName] = dsTmp.Tables[dtTable.TableName].Rows[i][col2.ColumnName];
        //							}
        //						}
        //						i++;
        //					}
        //				}
        //			}
        //			if (!udo.Save(UserSign, udoName, dsUdo, keyField, (OrderType)OrderType, DraftOrder, ref newDocEntry, SqlExcute))
        //			{
        //				di.Result = false;
        //				di.ResultDesc = _UdoInfo.FormCaption + " 保存失败";
        //				di.OrderType = OrderType;
        //			}
        //			else
        //			{
        //				di.Result = true;
        //				di.ResultDesc = " 保存成功";
        //				di.OrderType = OrderType;
        //				di.DocEntry = newDocEntry.ToString();
        //			}
        //		}
        //		Order.log.Info(string.Concat(new string[]
        //		{
        //	di.OrderType.ToString(),
        //	di.Result.ToString(),
        //	" ",
        //	di.ResultDesc,
        //	" ",
        //	di.DocEntry
        //		}));
        //	}
        //	catch (Exception ex)
        //	{
        //		di.OrderType = OrderType;
        //		di.DocEntry = "-1";
        //		di.Result = false;
        //		di.ResultDesc = ex.Message;
        //		Order.log.Error(string.Concat(new string[]
        //		{
        //	"错误:",
        //	di.OrderType.ToString(),
        //	di.Result.ToString(),
        //	" ",
        //	di.ResultDesc,
        //	" ",
        //	di.DocEntry
        //		}));
        //	}
        //	finally
        //	{
        //		Order.log.Info("提交结束:" + Guid);
        //	}
        //	return di;
        //}

    }
}
   
    

