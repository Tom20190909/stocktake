using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace stocktake
{
    public partial class Inventory : Form
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        string connstr = "User Id=dswmsuser;Password=sys2016;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.28.18.98)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=newwms)))";


        static oraexec ora;
        public Inventory()
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
        public bool chkall;
        private void Inventory_Load(object sender, EventArgs e)
        {
            try
            {
                OracleConnection ora1 = new OracleConnection(connstr);
                ora1.Open();
            }
            catch (Exception er)
            {
                richTextBox1.Text = er.Message;
            }

            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            ora = new oraexec(connstr);
            loadStoreInfo();
            ShowInvInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void ShowInvInfo()
        {
            string sql = @"select orderid 盘点单号,storageid 仓库代码 ,comparedate 盘点完成日期,checktime 盘点日期,checkstate 盘点状态,checkname 盘点名称 from t_mat_checkouttable";

            DataTable dt = ora.GetDt(sql);
            dataGridView6.DataSource = dt;


        }
        private void loadStoreInfo()
        {
            string sql = @"select  warehouseid,warehousename  from t_sys_warehouse";

            DataTable dt = ora.GetDt(sql);
            comboBox1.DisplayMember = "warehousename";
            comboBox1.ValueMember = "warehouseid";
            comboBox1.DataSource = dt;




        }


        private void button2_Click(object sender, EventArgs e)
        {
            string remark = dateTimePicker1.Value.ToString("yyyy/MM/dd") + " " + comboBox1.Text + "盘点";
            string PDDH = "PD" + comboBox1.SelectedValue.ToString() + "-" + dateTimePicker1.Value.Year.ToString() + (dateTimePicker1.Value.Month < 10 ? "0" + dateTimePicker1.Value.Month.ToString() : dateTimePicker1.Value.Month.ToString()) + (dateTimePicker1.Value.Day < 10 ? "0" + dateTimePicker1.Value.Day.ToString() : dateTimePicker1.Value.Day.ToString()) + "0001";
            string insql = @"insert into t_mat_checkouttable(orderid,storageid,checktime,remark,makedatetime,productor,
                         checkstate,updatetime,dtype,exceptionorder,checkname,checktype,docentry,systype) values(";
            insql += $"'{PDDH}','{comboBox1.SelectedValue.ToString()}',to_date('{dateTimePicker1.Value.ToString("yyyy-MM-dd")}','yyyy-mm-dd'),'{remark}',to_date('{dateTimePicker1.Value.ToString("yyyy-MM-dd")}','yyyy-mm-dd'),'Admin',";
            insql += $"0,to_date('{dateTimePicker1.Value.ToString("yyyy-MM-dd")}','yyyy-mm-dd'),'1','{remark}','{remark}',3,'1','K')";

            // richTextBox1.Text = insql;
            ora.ExecuteNonQuery(insql);
            ShowInvInfo();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string pddh = dataGridView6.SelectedRows[0].Cells[0].Value.ToString();
            string upsql = $"update t_mat_checkouttable set checkstate=3,comparedate=to_date('{DateTime.Now.ToString("yyyy-MM-dd")}','yyyy-mm-dd') where orderid='{pddh}'";
            //  richTextBox1.Text = upsql;
            int r = ora.ExecuteNonQuery(upsql);
            //  richTextBox1.AppendText("\r\n"+r.ToString());
            if (r > 0)
            {
                ShowInvInfo();
            }
            else
            {
                MessageBox.Show("更新失败");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult YRO = MessageBox.Show("您确定要删除选中的记录吗？", "提醒您：", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (YRO == DialogResult.Yes)
            {
                string pddh = dataGridView6.SelectedRows[0].Cells[0].Value.ToString();
                string upsql = $"delete  from  t_mat_checkouttable  where orderid='{pddh}'";
                //  richTextBox1.Text = upsql;
                int r = ora.ExecuteNonQuery(upsql);
                //  richTextBox1.AppendText("\r\n" + r.ToString());
                if (r > 0)
                {
                    ShowInvInfo();
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }
            else
            {
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            int r = ora.ExecuteNonQuery("delete from  temp_zhy_excption_2017th");
            richTextBox1.Text = "清除记录数:" + r.ToString() + "\r\n";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int startrow, endrow;
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

            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (openFileDialog1.FileName != "")
            {
                this.Invoke(new Action(() =>
                {

                    this.button3.Enabled = false;

                }));

                Task.Run(() => readexcel());
            }
        }
        private string GetGoodsName(string goodId)
        {
            string GoodsName = "";
            DataTable dt = ora.GetDt($"SELECT  GOODSNAME from t_pdm_goodsinfo WHERE GOODSID='{goodId}'");
            if (dt.Rows.Count > 0)
            {
                GoodsName = dt.Rows[0][0].ToString();
            }
            return GoodsName;
        }
        public void readexcel()
        {

            if (openFileDialog1.FileName != "")
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox1.AppendText("开始导入数据..." + "\r\n");


                }));
                int col = 0;
                int totalnum = 0;
                DataTable dt = ora.GetDt("select '' as NOROW, GOODSID,GOODSNAME,QUANTITY,PLACECODE from temp_zhy_excption_2017th");
                DataRow dr;
                // Sqlexec sqlex = new Sqlexec(getelement("connstr"));
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
                    
                    string GOODSID, GOODSNAME, QUANTITY, PLACECODE;
                    int Qty;
                    int i = 0;
                    int result = 0;
                    string insql;
                    for (int row = int.Parse(textBox1.Text); row < int.Parse(textBox2.Text) + 1; row++)
                    {
                        this.Invoke(new Action(() =>
                        {
                            richTextBox1.AppendText("读取第" + row.ToString() + "行记录中..." + "\r\n");
                            this.button5.Enabled = false;

                        }));
                        GOODSID = ((Microsoft.Office.Interop.Excel.Range)RSsheet.Cells[row, 3]).Text;
                        PLACECODE = ((Microsoft.Office.Interop.Excel.Range)RSsheet.Cells[row, 2]).Text;
                        QUANTITY = ((Microsoft.Office.Interop.Excel.Range)RSsheet.Cells[row, 5]).Text;
                        if (int.TryParse(QUANTITY, out Qty))
                        {

                        }
                        else
                        {
                            this.Invoke(new Action(() =>
                            {
                                richTextBox1.AppendText("第" + row.ToString() + "行记录数量不是正确格式" + "\r\n");

                            }));
                            Qty = 0;
                        }
                        totalnum += Qty;
                        dr = dt.NewRow();
                        dr[0] = row;
                        dr[1] = GOODSID;
                        dr[2] = GetGoodsName(GOODSID);
                        dr[3] = Qty;
                        dr[4] = PLACECODE;
                        dt.Rows.Add(dr);
                        insql = $"insert into temp_zhy_excption_2017th(GOODSID,GOODSNAME,QUANTITY,PLACECODE) values('{GOODSID}','{dr[1].ToString()}',{Qty},'{PLACECODE}')";
                        result = ora.ExecuteNonQuery(insql);
                        if (result > 0)
                        {
                            this.Invoke(new Action(() =>
                            {
                                richTextBox1.AppendText("第" + row.ToString() + "行记录保存至数据库成功" + "\r\n");

                            }));
                        }
                        else
                        {
                            this.Invoke(new Action(() =>
                            {
                                richTextBox1.AppendText("第" + row.ToString() + "行记录保存至数据库失败" + "\r\n");

                            }));
                        }
                        i++;
                    }

                    dr = dt.NewRow();
                    dr[0] = "";
                    dr[1] = "";
                    dr[2] = "合计";
                    dr[3] = totalnum;
                    dr[4] = "";
                    dt.Rows.Add(dr);

                    ExcelRS.Visible = false;
                    ExcelRS.DisplayAlerts = false;

                    RSbook.Save();
                    this.Invoke(new Action(() =>
                    {
                        dataGridView2.DataSource = dt;

                    }));



                    MessageBox.Show("成功导入记录:" + i.ToString() + "条!");


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

                        this.button5.Enabled = true;
                    }));

                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sql = $"insert into t_mat_batchlibrary select goodsid,to_date('{dateTimePicker2.Value.ToString("yyyy-MM-dd")}','yyyy-mm-dd'),36,36,'00000000',add_months(to_date('{dateTimePicker2.Value.ToString("yyyy-MM-dd")}','yyyy-mm-dd'), 36),sysdate   from temp_zhy_excption_2017th where (goodsid) not in ( select goodsid from t_mat_batchlibrary where batchcode = '00000000') group by goodsid";

            int result = ora.ExecuteNonQuery(sql);
            tabControl1.SelectedIndex = 2;
            if (result > 0)
            {
                this.Invoke(new Action(() =>
                {

                    richTextBox1.AppendText("补录批次库数据成功" + "\r\n");

                }));
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox1.AppendText("补录批次库数据失败" + "\r\n");

                }));
            }
;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string bitchcode = dateTimePicker2.Value.Year.ToString() + (dateTimePicker2.Value.Month < 10 ? "0" + dateTimePicker2.Value.Month.ToString() : dateTimePicker2.Value.Month.ToString()) + (dateTimePicker2.Value.Day < 10 ? "0" + dateTimePicker2.Value.Day.ToString() : dateTimePicker2.Value.Day.ToString()) + "0001";

            string sql = @"insert into t_mat_warehousplacewbk
                    select 'OU' || substr(placecode, 0, 4) || '{0}',sysdate,-40,'盘亏出库',
                    a.goodsid,b.goodscode,b.goodsname,b.brandid,e.brandname,b.sortid,c.sortname,sum(a.sumquantity),
                    d.warehouseid,d.warehousename,d.warehouseid,d.warehousename,a.placecode,a.batchcode,''
                    from t_mat_storingplaceinventory a
                    inner
                    join t_pdm_goodsinfo b
                    on a.goodsid = b.goodsid
                    inner
                    join t_pdm_goodsort c
                    on b.sortid = c.sortid
                    inner
                    join t_sys_warehouse d
                    on substr(a.placecode, 0, 4) = d.warehouseid
                    inner
                    join t_pdm_brand e on b.brandid = e.brandno
                    where d.warehouseid in ('gdth', 'jhth')
                    group by a.goodsid,b.goodscode,b.goodsname,b.brandid,e.brandname,b.sortid,c.sortname,
                    d.warehouseid,d.warehousename,d.warehouseid,d.warehousename,a.placecode,a.batchcode
                    having sum(a.sumquantity) > 0";
            //  richTextBox1.AppendText(string.Format(sql, bitchcode) + "\r\n");
            tabControl1.SelectedIndex = 2;

            int result = ora.ExecuteNonQuery(string.Format(sql, bitchcode));
            if (result > 0)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox1.AppendText("记录库存流水成功" + "\r\n");

                }));
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox1.AppendText("记录库存流水失败" + "\r\n");

                }));
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string sql = @"delete t_mat_storingplaceinventory 
                            where placecode in
                            (
                                 select placecode from t_sys_storingplace where warehouseid in ('gdth', 'jhth')
                            )";

            int result = ora.ExecuteNonQuery(sql);
            tabControl1.SelectedIndex = 2;
            if (result > 0)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox1.AppendText("盘出原有库存成功" + "\r\n");

                }));
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox1.AppendText("盘出原有库存失败" + "\r\n");

                }));
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string sql = @" insert into t_mat_storingplaceinventory
                            select placecode,goodsid,'00000000',sum(quantity),0,0 from temp_zhy_excption_2017th
                            group by placecode,goodsid
                            having sum(quantity)>0";

            int result = ora.ExecuteNonQuery(sql);
            tabControl1.SelectedIndex = 2;
            if (result > 0)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox1.AppendText("盘盈入此次盘点库存成功" + "\r\n");

                }));
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox1.AppendText("盘盈入此次盘点库存失败" + "\r\n");

                }));
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string bitchcode = dateTimePicker2.Value.Year.ToString() + (dateTimePicker2.Value.Month < 10 ? "0" + dateTimePicker2.Value.Month.ToString() : dateTimePicker2.Value.Month.ToString()) + (dateTimePicker2.Value.Day < 10 ? "0" + dateTimePicker2.Value.Day.ToString() : dateTimePicker2.Value.Day.ToString()) + "0001";
            string sql = @"insert into t_mat_warehousplacewbk
                            select 'IN'||substr(placecode,0,4)||'{0}',sysdate,40,'盘盈入库',
                            a.goodsid,b.goodscode,b.goodsname,b.brandid,e.brandname,b.sortid,c.sortname,sum(a.quantity),
                            d.warehouseid,d.warehousename,d.warehouseid,d.warehousename,a.placecode,'00000000',''
                            from temp_zhy_excption_2017th a 
                            inner join t_pdm_goodsinfo b 
                            on a.goodsid = b.goodsid 
                            inner join t_pdm_goodsort c 
                            on b.sortid = c.sortid
                            inner join t_sys_warehouse d 
                            on substr(a.placecode,0,4) = d.warehouseid
                            inner join t_pdm_brand e on b.brandid = e.brandno
                            where d.warehouseid in ('gdth','jhth')
                            group by a.goodsid,b.goodscode,b.goodsname,b.brandid,e.brandname,b.sortid,c.sortname,
                            d.warehouseid,d.warehousename,d.warehouseid,d.warehousename,a.placecode
                            having sum(a.quantity)>0";

            int result = ora.ExecuteNonQuery(string.Format(sql, bitchcode));
            tabControl1.SelectedIndex = 2;
            if (result > 0)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox1.AppendText("记录库存入库流水成功" + "\r\n");

                }));
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox1.AppendText("记录库存入库流水失败" + "\r\n");

                }));
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string bitchcode = dateTimePicker2.Value.Year.ToString() + (dateTimePicker2.Value.Month < 10 ? "0" + dateTimePicker2.Value.Month.ToString() : dateTimePicker2.Value.Month.ToString()) + (dateTimePicker2.Value.Day < 10 ? "0" + dateTimePicker2.Value.Day.ToString() : dateTimePicker2.Value.Day.ToString()) + "0001";
            string sql = @" begin 
                            insert into t_mat_businesswbk

                            select 'OU'||a.businessid||'{0}',sysdate,-40,'盘亏出库',
                            a.businessid,a.goodsid,b.goodscode,b.goodsname,sum(a.sumquantity)
                            from t_mat_businessstock a 
                            inner join t_pdm_goodsinfo b 
                            on a.goodsid = b.goodsid 
                            inner join t_pdm_goodsort c 
                            on b.sortid = c.sortid
                            where 
                            1=1 --d.warehouseid in ('gdth','jhth')
                            group by a.businessid,a.goodsid,b.goodscode,b.goodsname
                            having sum(a.sumquantity)>0;

                            -- Step7 删除现有库存
                            delete t_mat_businessstock ;

                            -- Step8 记录事业部库存入库流水

                            insert into t_mat_businesswbk

                            select 'IN'||'syyb{1}',sysdate,40,'盘盈入库',
                            'SYYB',a.goodsid,b.goodscode,b.goodsname,sum(a.quantity)
                            from temp_zhy_excption_2017th a 
                            inner join t_pdm_goodsinfo b 
                            on a.goodsid = b.goodsid 
                            inner join t_pdm_goodsort c 
                            on b.sortid = c.sortid
                            group by a.goodsid,b.goodscode,b.goodsname
                            having sum(a.quantity)>0 
                            ;


                            -- Step9 将此次盘点库存，初始进事业部库存，默认 SYYB 事业一部 (盘盈入库)

                            insert into t_mat_businessstock

                            select 'SYYB',goodsid,sum(quantity),0 from temp_zhy_excption_2017th 
                            group by goodsid ; 
                            end;";

            //  richTextBox1.AppendText(string.Format(sql, bitchcode, bitchcode) + "\r\n");
            int result = ora.ExecuteNonQuery(string.Format(sql, bitchcode, bitchcode));
            tabControl1.SelectedIndex = 2;
            if (result > 0)
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox1.AppendText("物料仓盘点后续操作成功" + "\r\n");

                }));
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    richTextBox1.AppendText("物料仓盘点后续操作失败" + "\r\n");

                }));
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
