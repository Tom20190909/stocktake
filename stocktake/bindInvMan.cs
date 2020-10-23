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

namespace stocktake
{
    public partial class bindInvMan : Form
    {
        [System.Runtime.InteropServices.DllImport("User32.dll", CharSet = CharSet.Auto)]

        
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public bindInvMan()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int startrow, endrow, barcol;
            if (!int.TryParse(textBox2.Text, out endrow))
            {
                MessageBox.Show("请输入正确的结束行数值");
                textBox2.Focus();
                return;
            }
            if (!int.TryParse(textBox1.Text, out startrow))
            {
                MessageBox.Show("请输入正确的开始行数值");
                textBox1.Focus();
                return;
            }
            if (!int.TryParse(textBox3.Text, out barcol))
            {
                MessageBox.Show("请输入正确的店铺名称列数值");
                textBox3.Focus();
                return;
            }
            if (!int.TryParse(textBox4.Text, out barcol))
            {
                MessageBox.Show("请输入正确的盘点人手机号列数值");
                textBox4.Focus();
                return;
            }
            if (endrow < 2)
            {
                MessageBox.Show("结束行不能小于2");
                textBox2.Focus();
                return;
            }
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (openFileDialog1.FileName != "")
            {

                Task.Run(() => readexcel());
            }
        }
        public void readexcel()
        {


            if (openFileDialog1.FileName != "")
            {
                this.Invoke(new Action(() =>
                {
                    this.lblprocessmsg.Text = "开始导入数据...";
                    this.button3.Enabled = false;

                }));
                Sqlexec sqlex = new Sqlexec(getelement("connstr"));
                object missing = Type.Missing;
                Microsoft.Office.Interop.Excel.Application ExcelRS;
                Microsoft.Office.Interop.Excel.Workbook RSbook;
                Microsoft.Office.Interop.Excel.Worksheet RSsheet;
                //  ExcelRS = null;
                //实例化ExcelRS对象
                ExcelRS = new Microsoft.Office.Interop.Excel.Application();
                //打开目标文件filePath
                RSbook = ExcelRS.Workbooks.Open(openFileDialog1.FileName, missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing, missing, missing);
                //设置第一个工作溥
                RSsheet = (Microsoft.Office.Interop.Excel.Worksheet)RSbook.Sheets.get_Item(1);
                //激活当前工作溥
                RSsheet.Activate();
                StringBuilder errmsg = new StringBuilder();
                // object missing = Type.Missing;
                errmsg.Append("错误信息:");
                try
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("U_clientid", typeof(System.String)));
                    dt.Columns.Add(new DataColumn("U_clientname", typeof(System.String)));
                    dt.Columns.Add(new DataColumn("U_userid", typeof(System.String)));
                    dt.Columns.Add(new DataColumn("U_name", typeof(System.String)));


                    int col = 0;
                    string U_clientid, U_clientName, U_userid;

                    #region 读取工作薄内容
                    for (int row = int.Parse(textBox1.Text); row < int.Parse(textBox2.Text) + 1; row++)
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.lblprocessmsg.Text = "读取第" + row.ToString() + "行记录中...";
                            this.button3.Enabled = false;

                        }));
                        U_clientName = ((Microsoft.Office.Interop.Excel.Range)RSsheet.Cells[row, int.Parse(textBox3.Text)]).Text;
                        U_userid = ((Microsoft.Office.Interop.Excel.Range)RSsheet.Cells[row, int.Parse(textBox4.Text)]).Text;
                        DataRow dr = dt.NewRow();
                        
                        dr[0] = getClientid(U_clientName);
                        dr[1] = U_clientName;
                        dr[2] = U_userid;
                        dr[3] = getUserName(U_userid);

                        dt.Rows.Add(dr);
                        this.Invoke(new Action(() =>
                                    {
                                        this.lblprocessmsg.Text = "读取" + U_clientName + "记录成功";
                                        this.button3.Enabled = false;

                                    }));
                        col++;


                        // ((Microsoft.Office.Interop.Excel.Range)RSsheet.Cells[row, 2]).Font.Color = ColorTranslator.ToOle(Color.Red);

                    }


                    // DataTable dt= oleGetDataSet("select BARCODE,SPDM,GG1DM,GG2DM,N_SL,SL,MARK FROM WPHYCD");
                    ExcelRS.Visible = false;
                    ExcelRS.DisplayAlerts = false;

                    RSbook.Save();
                    this.Invoke(new Action(() =>
                    {
                        dataGridView6.DataSource = dt;

                    }));
                  


                    MessageBox.Show("成功导入记录:" + col.ToString() + "条!");


                }
                catch (Exception er)
                {
                    MessageBox.Show(er.ToString());

                }
                finally
                {
                    ExcelRS.Quit();
                    IntPtr t = new IntPtr(ExcelRS.Hwnd);
                    int k = 0;
                    GetWindowThreadProcessId(t, out k);
                    System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);
                    p.Kill();
                    this.Invoke(new Action(() =>
                    {
                        this.lblprocessmsg.Text = "导入完成";

                        this.button3.Enabled = true;
                    }));

                }
            }
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
        private void bindInvMan_Load(object sender, EventArgs e)
        {

        }
        public string getUserName(string U_userid)
        {
            Sqlexec sqlex = new Sqlexec(getelement("sqlconnstr"));
            string sql = " select U_USENAME from U_MMS_TB_USER where U_USERID = '" + U_userid + "'";
            DataTable dt = sqlex.SqlGetDataSet(sql);
            if (dt.Rows.Count>0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
           
        }
        public bool Isbind(string userid,string U_Clientid)
        {
            Sqlexec sqlex = new Sqlexec(getelement("sqlconnstr"));
            string sql = " select 1 from U_MMS_TB_USER_CLIENT where U_USERID='"+ userid + "' and U_CLIENTID='"+ U_Clientid + "'";
            DataTable dt = sqlex.SqlGetDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public string getClientid(string U_clientName)
        {
            Sqlexec sqlex = new Sqlexec(getelement("sqlconnstr"));
            string sql = " select U_AddressCode from VWE_CRD1 where [Address]='" + U_clientName + "'";
            DataTable dt = sqlex.SqlGetDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int col = 0;
            Sqlexec sqlex = new Sqlexec(getelement("sqlconnstr"));
            for (int i=0;i<dataGridView6.Rows.Count;i++)
            {
                if (dataGridView6.Rows[i].Cells[0].Value.ToString() != "")
                {
                    if (!Isbind(dataGridView6.Rows[i].Cells[2].Value.ToString(), dataGridView6.Rows[i].Cells[0].Value.ToString()))
                    {
                        sqlex.SqlExecuteSQL("insert into U_MMS_TB_USER_CLIENT values('" + dataGridView6.Rows[i].Cells[2].Value.ToString() + "','" + dataGridView6.Rows[i].Cells[0].Value.ToString() + "')");
                        this.Invoke(new Action(() =>
                        {
                            dataGridView6.Rows[i].Cells[0].Style.BackColor = Color.Green;
                            richTextBox2.AppendText(dataGridView6.Rows[i].Cells[1].Value.ToString() + "--" + dataGridView6.Rows[i].Cells[0].Value.ToString() +  "绑定完成" + "\r\n");


                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            dataGridView6.Rows[i].Cells[0].Style.BackColor = Color.Red;
                            richTextBox2.AppendText(dataGridView6.Rows[i].Cells[1].Value.ToString()+"--"+dataGridView6.Rows[i].Cells[0].Value.ToString() + "已存在绑定记录,无须再绑定" + "\r\n");


                        }));
                    }
                   
                }
                else
                {
                    richTextBox2.AppendText(dataGridView6.Rows[i].Cells[1].Value.ToString() + "店铺代码为空，不能绑定" + "\r\n");
                }
            }
          

        }
    }
}
#endregion