using stocktake.BLL;
using stocktake.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace stocktake
{
    public partial class scanbar : Form
    {
        BarCodeInfoBll barBll = new BarCodeInfoBll();
        TakeCodeBll takeBll = new TakeCodeBll();
        public int rec;
        public scanbar()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == 13)
            {
                var itemlist = barBll.GetList().Where(b => b.CodeBard.Equals(txtCodeBard.Text)).AsQueryable();
                int Num = itemlist.ToList().Count;
                if (Num > 0)
                {
                   // txtItemName.Enabled = false;
                  //  txtBrandName.Enabled = false;

                    if (Num == 1)
                    {
                        txtBrandCode.Text = itemlist.SingleOrDefault().BrandCode;
                        txtBrandName.Text = itemlist.SingleOrDefault().BrandName;
                        txtItemCode.Text = itemlist.SingleOrDefault().ItemCode;
                        txtItemName.Text = itemlist.SingleOrDefault().ItemName;
                    }
                    else
                    {
                        loaditem(itemlist.ToList());
                        groupBox6.Visible = true;
                        dataGridView6.Focus();
                    }
                }
                else
                {
                    var takelist = takeBll.GetList().Where(b => b.CodeBard.Equals(txtCodeBard.Text)).AsQueryable();
                    Num = takelist.ToList().Count;
                    if (Num > 0)
                    {
                       
                        if (Num == 1)
                        {
                            txtBrandCode.Text = takelist.SingleOrDefault().BrandCode;
                            txtBrandName.Text = takelist.SingleOrDefault().BrandName;
                            txtItemCode.Text = takelist.SingleOrDefault().ItemCode;
                            txtItemName.Text = takelist.SingleOrDefault().ItemName;
                        }
                        else
                        {
                         var  blist=takeBll.GetList().Where(b => b.CodeBard.Equals(txtCodeBard.Text)).GroupBy(a => new { a.ItemCode, a.ItemName, a.BrandCode, a.BrandName }).Select(c=>c.Key).ToList().Select(c=>new BarCodeInfo {
                                ItemCode = c.ItemCode,
                                ItemName = c.ItemName,
                                BrandCode = c.BrandCode,
                                BrandName = c.BrandName
                               
                            }).ToList();
                            loaditem(blist);
                            groupBox6.Visible = true;
                            dataGridView6.Focus();
                        }
                    }
                    txtItemName.Enabled = true;
                    txtBrandName.Enabled = true;
                }
                label5.Text = checkexitsbar(txtCodeBard.Text);
                txtCodeBard.SelectAll();
                txtTV1.Focus();
            }
            
        }
        public string checkexitsbar(string barcode)
        {
            var result = takeBll.GetList().Where(b => b.CodeBard.Equals(txtCodeBard.Text));
            if (result.ToList().Count > 0)
            {
                return "该条码已有"+ result.ToList().Count.ToString()+"条盘点记录";
            }
            else
            {
                return "";
            }
        }
        public void loaditem(List<BarCodeInfo> list)
        {
            dataGridView6.DataSource = list;
        } 
        private void scanbar_Load(object sender, EventArgs e)
        {
            lstadd(takeBll.GetList().OrderBy(u=>u.ID).ToList());
        }
        public void lstadd(List<TakeCode> list)
        {
            // MessageBox.Show("ok");
            rec = 0;
            listView1.Clear();
            listView1.View = View.Details;

            listView1.Columns.Add("条码", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("货品编号", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("货品名称", 420, HorizontalAlignment.Left);
            listView1.Columns.Add("品牌", 90, HorizontalAlignment.Left);
            listView1.Columns.Add("品牌代码", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("货位", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("效期1~2年", 90, HorizontalAlignment.Left);
            listView1.Columns.Add("效期2年以上", 90, HorizontalAlignment.Left);
            listView1.Columns.Add("合计", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("ID", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("时间", 170, HorizontalAlignment.Left);
            listView1.CheckBoxes = true;

         
            try
            {
                foreach (var obj in list.OrderBy(u=>u.ID))
                {
                   TakeCode takeinfo = (TakeCode)obj;
                    rec += takeinfo.TV3;
                listView1.Items.Add(new ListViewItem(new string[] {
                    takeinfo.CodeBard,
                    takeinfo.ItemCode,
                    takeinfo.ItemName,
                    takeinfo.BrandName,
                    takeinfo.BrandCode,
                    takeinfo.TakeArea,
                    takeinfo.TV1.ToString(),
                    takeinfo.TV2.ToString(),
                    takeinfo.TV3.ToString(),
                    takeinfo.ID.ToString(),
                    takeinfo.TakeTime.ToString()
                }));
                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

            }

         
            listView1.GridLines = true;
            if (listView1.Items.Count > 1)
            {
                listView1.EnsureVisible(listView1.Items.Count - 1);
            }
            for (int i = 0; i < listView1.Items.Count; i++)
            {
               
                 //   listView1.Items[0].BackColor = Color.LightSlateGray;
               
                if (i % 2 == 0)
                {
                    for (int j = 0; j < listView1.Items[i].SubItems.Count; j++)
                    {
                        listView1.Items[i].SubItems[j].BackColor = Color.LightSlateGray;
                    }
                }
                else
                {
                    for (int j = 0; j < listView1.Items[i].SubItems.Count; j++)
                    {
                        listView1.Items[i].SubItems[j].BackColor = Color.WhiteSmoke;
                    }
                }
                listView1.Items[i].UseItemStyleForSubItems = true;
            }

            label14.Text = "合计数量:" + rec.ToString();
        }

        private void dataGridView6_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtItemCode.Text = dataGridView6.CurrentRow.Cells["ItemCode"].Value==null?"": dataGridView6.CurrentRow.Cells["ItemCode"].Value.ToString();
                txtItemName.Text = dataGridView6.CurrentRow.Cells["ItemName"].Value.ToString();
                txtBrandName.Text = dataGridView6.CurrentRow.Cells["BrandName"].Value==null?"": dataGridView6.CurrentRow.Cells["BrandName"].Value.ToString();
                txtBrandCode.Text = dataGridView6.CurrentRow.Cells["BrandCode"].Value==null ?"": dataGridView6.CurrentRow.Cells["BrandCode"].Value.ToString();
                groupBox6.Visible = false;
            }
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tv1 = 0,tv2=0;
            if (txtTV1.Text.Trim().Length>0)
            {
                if (!int.TryParse(txtTV1.Text.Trim(),out tv1))
                {
                    MessageBox.Show("非有效数值!");
                    txtTV1.Focus();
                    return;
                }
                if (tv1<0)
                {
                    MessageBox.Show("非有效数值!");
                    txtTV1.Focus();
                    return;
                }
            }
            if (txtTV2.Text.Trim().Length > 0)
            {
                if (!int.TryParse(txtTV2.Text.Trim(), out tv2))
                {
                    MessageBox.Show("非有效数值!");
                    txtTV2.Focus();
                    return;
                }
                if (tv2 < 0)
                {
                    MessageBox.Show("非有效数值!");
                    txtTV2.Focus();
                    return;
                }
            }
            if (tv1+tv2==0)
            {
                MessageBox.Show("无盘点数数据!");
                txtTV2.Focus();
                return;
            }
            TakeCode takecode = new TakeCode
            {
                ItemCode = txtItemCode.Text,
                ItemName = txtItemName.Text,
                CodeBard = txtCodeBard.Text,
                BrandCode = txtBrandCode.Text,
                BrandName = txtBrandName.Text,
                TV1 = tv1,
                TV2 = tv2,
                TV3 = tv1 + tv2,
                TakeArea = txtTakeArea.Text,
                TakeTime = DateTime.Now

            };
            if (takeBll.Add(takecode))
            {
                listView1.Items.Add(new ListViewItem(new string[] {
                    takecode.CodeBard,
                    takecode.ItemCode,
                    takecode.ItemName,
                    takecode.BrandName,
                    takecode.BrandCode,
                    takecode.TakeArea,
                    takecode.TV1.ToString(),
                    takecode.TV2.ToString(),
                    takecode.TV3.ToString(),
                    takecode.ID.ToString(),
                    takecode.TakeTime.ToString()
                }));
                rec = rec + takecode.TV3;
            
                MessageBox.Show("添加成功!");
                cleartxt();
                listView1.GridLines = true;
                if (listView1.Items.Count > 1)
                {
                    listView1.EnsureVisible(listView1.Items.Count - 1);
                }
                for (int i = 0; i < listView1.Items.Count; i++)
                {

                    //   listView1.Items[0].BackColor = Color.LightSlateGray;

                    if (i % 2 == 0)
                    {
                        for (int j = 0; j < listView1.Items[i].SubItems.Count; j++)
                        {
                            listView1.Items[i].SubItems[j].BackColor = Color.LightSlateGray;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < listView1.Items[i].SubItems.Count; j++)
                        {
                            listView1.Items[i].SubItems[j].BackColor = Color.WhiteSmoke;
                        }
                    }
                    listView1.Items[i].UseItemStyleForSubItems = true;
                }
                label14.Text = "合计数量:"+rec.ToString();
            }
            else
            {
                MessageBox.Show("添加失败!");
            }
            txtCodeBard.Focus();
            txtCodeBard.SelectAll();
            label5.Text = "";
        }
        public void cleartxt()
        {
            foreach (Control ctl in this.Controls)
            {
                //获取并判断控件类型或控件名称 
                if (ctl.GetType().Name.Equals("TextBox"))
                {
                    if (!(ctl.Name.Equals("txtTakeArea") || ctl.Name.Equals("txtItemName")|| ctl.Name.Equals("txtBrandName")))
                    {
                        ctl.Text = "";
                    }
                }
                    
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            if (listView1.CheckedItems.Count == 0)
            {
                MessageBox.Show("您尚未选择要删除的记录！", "提醒您：", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {

                DialogResult YRO = MessageBox.Show("您确定要删除选中的记录吗？", "提醒您：", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (YRO == DialogResult.Yes)
                {
                   
                    for (int i = 0; i < listView1.CheckedItems.Count; i++)
                    {
                        takeBll.Remove(int.Parse(listView1.CheckedItems[i].SubItems[9].Text));

                    }
                    lstadd(takeBll.GetList().ToList());

                }
            }

            txtCodeBard.Focus();
            txtCodeBard.SelectAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog4.ShowDialog();
        }

        private void saveFileDialog4_FileOk(object sender, CancelEventArgs e)
        {
            if (saveFileDialog4.FileName != "")
            {

                string path = System.Windows.Forms.Application.StartupPath + "\\temp.xls";
                if (!File.Exists(path))
                {
                    MessageBox.Show("模板文件不存在！");
                    return;
                }
                string filePath = saveFileDialog4.FileName;
                File.Copy(path, filePath, true);
               
                if (listView1.Items.Count == 0)
                {
                    MessageBox.Show("数据不存在！");
                    return;
                }

                try
                {
                    object missing = Type.Missing;
                    Microsoft.Office.Interop.Excel.Application ExcelRS;
                    Microsoft.Office.Interop.Excel.Workbook RSbook;
                    Microsoft.Office.Interop.Excel.Worksheet RSsheet;



                    ExcelRS = null;
                    //实例化ExcelRS对象
                    ExcelRS = new Microsoft.Office.Interop.Excel.Application();
                    //打开目标文件filePath
                    RSbook = ExcelRS.Workbooks.Open(filePath, missing, missing, missing, missing, missing,
                        missing, missing, missing, missing, missing, missing, missing, missing, missing);
                    //设置第一个工作溥
                    RSsheet = (Microsoft.Office.Interop.Excel.Worksheet)RSbook.Sheets.get_Item(1);
                    //激活当前工作溥
                    RSsheet.Activate();
                    List<TakeCode> list = takeBll.GetList().ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        TakeCode takeinfo = (TakeCode)list[i];


                        RSsheet.Cells[i + 4, 1] = (i+1).ToString();
                        RSsheet.Cells[i + 4, 3] = takeinfo.ItemName;
                        RSsheet.Cells[i+4,2]= takeinfo.BrandName;
                        RSsheet.Cells[i+4,4]= takeinfo.CodeBard;
                        RSsheet.Cells[i + 4,5] = takeinfo.TV1;
                        RSsheet.Cells[i + 4, 6]= takeinfo.TV2;
                        RSsheet.Cells[i + 4, 7]= takeinfo.TV3;
                       

                    }
                   

                    //保存目标文件
                    RSbook.Save();
                    //设置DisplayAlerts
                    ExcelRS.DisplayAlerts = false;
                    ExcelRS.Visible = true;
                    //ExcelRS.DisplayAlerts = true;

                    //释放对象
                    RSsheet = null;
                    RSbook = null;
                    ExcelRS = null;
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lstadd(takeBll.GetList().Where(u=>u.CodeBard.Contains(txtCodeBard.Text)&&u.ItemName.Contains(txtItemName.Text)&&u.BrandName.Contains(txtBrandName.Text)).OrderBy(u => u.ID).ToList());
        }

        private void dataGridView6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
               // MessageBox.Show(dataGridView6.CurrentRow.Index.ToString());
                txtItemCode.Text = dataGridView6.CurrentRow.Cells["ItemCode"].Value==null?"": dataGridView6.CurrentRow.Cells["ItemCode"].Value.ToString();
                txtItemName.Text = dataGridView6.CurrentRow.Cells["ItemName"].Value.ToString();
                txtBrandName.Text = dataGridView6.CurrentRow.Cells["BrandName"].Value == null ?"": dataGridView6.CurrentRow.Cells["BrandName"].Value.ToString();
                txtBrandCode.Text = dataGridView6.CurrentRow.Cells["BrandCode"].Value==null?"": dataGridView6.CurrentRow.Cells["BrandCode"].Value.ToString();
                groupBox6.Visible = false;
            }
        }

        private void 更新货品资料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f1 = new stocktake.Form1();
            f1.ShowDialog();
        }

        private void scanbar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Directory.Exists(@"C:\BAK"))
            {
                Directory.CreateDirectory(@"C:\BAK");
            }
            string path = System.Windows.Forms.Application.StartupPath + "\\StoreTake.mdb";
            string filePath = @"C:\BAK\" + System.DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒") + ".mdb";
            File.Copy(path, filePath, true);
        }

        private void 显示运行目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", System.Windows.Forms.Application.StartupPath);
        }

        private void 清除记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult YRO = MessageBox.Show("您确信要清除所有盘点记录吗？", "提醒您：", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (YRO == DialogResult.Yes)
            {
                if (!Directory.Exists(@"C:\BAK"))
                {
                    Directory.CreateDirectory(@"C:\BAK");
                }
                string path = System.Windows.Forms.Application.StartupPath + "\\StoreTake.mdb";
                string filePath = @"C:\BAK\" + System.DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒") + ".mdb";
                File.Copy(path, filePath, true);
                takeBll.RemoveAll();
                lstadd(takeBll.GetList().Where(u => u.CodeBard.Contains(txtCodeBard.Text) && u.ItemName.Contains(txtItemName.Text) && u.BrandName.Contains(txtBrandName.Text)).OrderBy(u => u.ID).ToList());
            }
        }

        private void txtCodeBard_Enter(object sender, EventArgs e)
        {
            txtCodeBard.SelectAll();
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public System.Data.DataTable oleGetDataSet(string strSql)
        {
            // "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Windows.Forms.Application.StartupPath + "\\StoreTake.mdb"
            DataSet ds = new DataSet();
            OleDbDataAdapter adapt = new OleDbDataAdapter();
            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Windows.Forms.Application.StartupPath + "\\StoreTake.mdb"))
            {
                try
                {
                    conn.Open();//与数据库连接    
                    adapt = new OleDbDataAdapter(strSql, conn); //实例化SqlDataAdapter类对象    
                    adapt.Fill(ds);//填充数据集    
                    return ds.Tables[0];//返回数据集DataSet的表的集合    

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);

                }
                finally
                {//断开连接，释放资源    
                    conn.Dispose();

                    adapt.Dispose();
                    conn.Close();
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
        private void button5_Click(object sender, EventArgs e)
        {
            string remsg = "";
            DataTable dt = new DataTable();
            WebReference.WebService ws = new WebReference.WebService();
            ws.Url = getelement("WebServiceurl");
            if (getelement("ShopCode").Trim().Length == 0)
            {
                MessageBox.Show("没有设置盘点店铺!");

            }
            dt = oleGetDataSet("select CodeBard as CODEBARS, TV3 as QUANTITY, '-1' as ORDERID from TakeCodes");
            if (dt.Rows.Count== 0)
            {
                MessageBox.Show("没有数据可以上传!");
            }
            if (ws.UpInventory(getelement("Instructions"), getelement("ShopCode"), "23", dt, out remsg))
            {
                MessageBox.Show(remsg);

            }
            else
            {
                MessageBox.Show(remsg);
            }

        }
    }
}
