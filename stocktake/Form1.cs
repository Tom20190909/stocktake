using stocktake.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using stocktake.BLL;
using stocktake.DAL;
using System.Data.Common;
using System.Data.OleDb;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.Data.Entity;
using System.Runtime.InteropServices;
using System.Diagnostics;



namespace stocktake
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        BarCodeInfoBll barCodeInfoBll = new BarCodeInfoBll();
        TakeCodeBll takeCodeBll = new TakeCodeBll();
        NoexitBarBll nbBll = new NoexitBarBll();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)

        {

            Task.Run(() => GetBaseInfo());
        }

        public void GetBaseInfo()
        {

            this.Invoke(new Action(() =>
            {
                this.lblprocessmsg.Text = "开始从服务器获取数据...";
                this.button1.Enabled = false;

            }));
            try
            {
                var suc = new SoapUnitTOM.SoapUnitClient();
                DataTable dt = suc.GetData(1, "BAR_FOR_STOCK", null);


                BarCodeInfo barCodeinfo;
                //   OleDbConnection acn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Windows.Forms.Application.StartupPath + "\\StoreTake.mdb");
                //   OleDbCommand amd;
                //    acn.Open();
                this.Invoke(new Action(() =>
                {
                    this.lblprocessmsg.Text = "待更新条码数" + dt.Rows.Count.ToString();
                    this.button1.Enabled = false;

                }));
             //   barCodeInfoBll.RemoveAll();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barCodeinfo = new BarCodeInfo();
                    barCodeinfo.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    barCodeinfo.ItemName = dt.Rows[i]["ItemName"].ToString();
                    barCodeinfo.CodeBard = dt.Rows[i]["CodeBard"].ToString();
                    barCodeinfo.BrandCode = dt.Rows[i]["BrandCode"].ToString();
                    barCodeinfo.BrandName = dt.Rows[i]["BrandName"].ToString();


                    this.Invoke(new Action(() =>
                    {
                        this.lblprocessmsg.Text = "更新条码" + dt.Rows[i][0].ToString();
                       
                        this.button1.Enabled = false;

                    }));
                    string _itemcode = dt.Rows[i]["ItemCode"].ToString();
                    BarCodeInfo result = barCodeInfoBll.GetList().Where(b => b.ItemCode.Equals(_itemcode)).SingleOrDefault();
                    if (result != null)
                    {
                        barCodeInfoBll.Remove(result.ID);
                    }

                    if (barCodeInfoBll.Add(barCodeinfo))
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.lblprocessmsg.Text = "更新" + dt.Rows[i]["CodeBard"].ToString();
                            this.button1.Enabled = false;

                        }));
                    }
                    //this.Invoke(new Action(() =>
                    //{

                    //    this.richTextBox1.Text = "insert into BarCodeInfo(ItemCode,ItemName,CodeBard,BrandCode,BrandName) values('" + dt.Rows[i]["ItemCode"].ToString() + "','" + dt.Rows[i]["ItemName"].ToString() + "','" + dt.Rows[i]["CodeBard"].ToString() + "','" + dt.Rows[i]["BrandCode"].ToString() + "','" + dt.Rows[i]["BrandName"] + "')";
                    //    this.lblprocessmsg.Text = dt.Rows[i]["ItemCode"].ToString() + "下载插入中...";
                    //    this.button1.Enabled = false;

                    //}));

                }
                //  acn.Close();
                this.Invoke(new Action(() =>
                {
                    this.lblprocessmsg.Text = "同步数据完成！";

                    this.button1.Enabled = true;

                }));
 
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());


                this.Invoke(new Action(() =>
                {

                    this.richTextBox1.AppendText(ee.ToString());
                    this.button1.Enabled = true;

                }));
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            BarCodeInfoBll barBll = new BarCodeInfoBll();
            var bi = barBll.GetById(1);

            richTextBox1.Text = bi.ItemName;
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
                MessageBox.Show("请输入正确的条码列数值");
                textBox3.Focus();
                return;
            }
            if (!int.TryParse(textBox4.Text, out barcol))
            {
                MessageBox.Show("请输入正确的货品名称列数值");
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
        private void KillProcess(string processName)
        {
            //获得进程对象，以用来操作
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程
            try
            {
                Process[] pros = Process.GetProcesses();
                //获得需要杀死的进程名
                foreach (Process thisproc in pros)
                {
                    if (thisproc.ProcessName == "EXCEL.EXE")
                        //立即杀死进程
                        thisproc.Kill();
                }
            }
            catch (Exception Exc)
            {
                throw new Exception("", Exc);
            }
        }

        public void readexcel()
        {


            if (openFileDialog1.FileName != "")
            {
                this.Invoke(new Action(() =>
                {
                    this.lblprocessmsg.Text = "开始导入数据...";
                    this.button1.Enabled = false;

                }));
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



                    int col = 0;
                    string CodeBard, ItemName, ItemCode;
                    
                    #region 读取工作薄内容
                    for (int row = int.Parse(textBox1.Text); row < int.Parse(textBox2.Text) + 1; row++)
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.lblprocessmsg.Text = "读取第" + row.ToString() + "条记录中...";
                            this.button1.Enabled = false;
                            this.button3.Enabled = false;

                        }));
                        CodeBard = ((Microsoft.Office.Interop.Excel.Range)RSsheet.Cells[row, int.Parse(textBox3.Text)]).Text  ;
                        CodeBard = CodeBard.Replace('.', ' ').Trim();
                        ItemName = ((Microsoft.Office.Interop.Excel.Range)RSsheet.Cells[row, int.Parse(textBox4.Text)]).Text;
                        if (checkbar(CodeBard))
                        {
                          
                      //  if (checkbar_forproduct(CodeBard,ItemName)<3)
                      //  {    //我司存在的条码标为红色
                            ((Microsoft.Office.Interop.Excel.Range)RSsheet.Cells[row, int.Parse(textBox3.Text)]).Font.Color = ColorTranslator.ToOle(Color.Red);
                          
                        }
                        else
                        {
                            List<NoexitsBar> nblist = nbBll.GetList().Where(n => n.CodeBard.Equals(CodeBard)).ToList();
                            if (nblist.Count > 0)
                            {
                                //我司不存在但已经登记的条码标为蓝色
                                ((Microsoft.Office.Interop.Excel.Range)RSsheet.Cells[row, int.Parse(textBox3.Text)]).Font.Color = ColorTranslator.ToOle(Color.Blue);

                            }
                            else
                            {
                                NoexitsBar nb = new NoexitsBar();

                                nb.ItemName = ItemName;
                                nb.CodeBard = CodeBard;
                                if (nbBll.Add(nb))
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                        this.lblprocessmsg.Text = "插入" + CodeBard + "记录成功";
                                        this.button1.Enabled = false;
                                        this.button3.Enabled = false;
                                    }));
                                    col++;
                                }
                            }
                            // ((Microsoft.Office.Interop.Excel.Range)RSsheet.Cells[row, 2]).Font.Color = ColorTranslator.ToOle(Color.Red);
                        }
                    }


                    // DataTable dt= oleGetDataSet("select BARCODE,SPDM,GG1DM,GG2DM,N_SL,SL,MARK FROM WPHYCD");
                    ExcelRS.Visible = false;
                    ExcelRS.DisplayAlerts = false;

                    RSbook.Save();




                    MessageBox.Show("成功导入记录:" + col.ToString() + "条,未成功导入的记录将以红色标记，请打开原文件查看!");


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
                        this.button1.Enabled = true;
                        this.button3.Enabled = true;
                    }));

                }
            }
        }

        private bool checkbar(string codeBard)
        {
            
            List<BarCodeInfo> barcodeinfolist = barCodeInfoBll.GetList().Where(b => b.CodeBard.Equals(codeBard)&&!(b.BrandCode==null|| b.BrandCode == string.Empty)).ToList();
            if (barcodeinfolist.Count > 0)
            {
                //if (barcodeinfolist.First().BrandCode == null|| barcodeinfolist.First().BrandCode == string.Empty)
                //{
                //    return false;
                //}
                //else
                //{
                //    return true;
                //}
                return true;
            }
            else
            {
                return false;
            }
            
        }
        private int checkbar_forproduct(string codeBard,string itemName)
        {

            List<BarCodeInfo> barcodeinfolist = barCodeInfoBll.GetList().Where(b => b.CodeBard.Equals(codeBard)).ToList();
            if (barcodeinfolist.Count > 0)
            {
                if (barcodeinfolist.First().BrandCode == null|| barcodeinfolist.First().BrandCode == string.Empty)
                {
                    if (barcodeinfolist.Where(b => b.ItemName.Equals(itemName)).ToList().Count > 0)
                    {
                        return 2;//库里有资料,但不为我司资料，且货品名称与对比标的相同
                    }
                    else
                    {
                        return 3;//库里有资料,但不为我司资料，且货品名称与对比标的不相同
                    }
                   
                   
                }
                else
                {
                    return 1;//库里有资料,且为我司资料
                }
               
            }
            else
            {
                return 4;//库里无资料
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            var itemlist = barCodeInfoBll.GetList().Where(b => b.CodeBard.Contains(txtcodebard.Text)|| b.ItemCode.Contains(txtcodebard.Text)|| b.ItemName.Contains(txtcodebard.Text)|| b.BrandName.Contains(txtcodebard.Text)).AsQueryable();
           
               
            loaditem(itemlist.ToList());
                    
                   
             
        }
        public void loaditem(List<BarCodeInfo> list)
        {
            dataGridView6.DataSource = list;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

}
#endregion