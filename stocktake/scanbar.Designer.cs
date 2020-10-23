namespace stocktake
{
    partial class scanbar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(scanbar));
            this.listView1 = new System.Windows.Forms.ListView();
            this.label14 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.txtTakeArea = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTV1 = new System.Windows.Forms.TextBox();
            this.txtBrandName = new System.Windows.Forms.TextBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.txtCodeBard = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTV2 = new System.Windows.Forms.TextBox();
            this.txtBrandCode = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrandCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            this.saveFileDialog4 = new System.Windows.Forms.SaveFileDialog();
            this.button4 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示运行目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更新货品资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.Color.White;
            this.listView1.CheckBoxes = true;
            this.listView1.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.ForeColor = System.Drawing.Color.Black;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.HoverSelection = true;
            this.listView1.Location = new System.Drawing.Point(74, 232);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(957, 292);
            this.listView1.TabIndex = 48;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(1013, 542);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 17);
            this.label14.TabIndex = 47;
            this.label14.Text = "合计数量：";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label34.Location = new System.Drawing.Point(30, 90);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(41, 12);
            this.label34.TabIndex = 46;
            this.label34.Text = "货位：";
            // 
            // txtTakeArea
            // 
            this.txtTakeArea.BackColor = System.Drawing.Color.White;
            this.txtTakeArea.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTakeArea.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtTakeArea.Location = new System.Drawing.Point(74, 83);
            this.txtTakeArea.Name = "txtTakeArea";
            this.txtTakeArea.Size = new System.Drawing.Size(135, 26);
            this.txtTakeArea.TabIndex = 45;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(604, 37);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 42;
            this.label11.Text = "品牌：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(382, 37);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 41;
            this.label10.Text = "名称：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(212, 37);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 40;
            this.label9.Text = "货品编号：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(19, 37);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 39;
            this.label8.Text = "条形码：";
            // 
            // txtTV1
            // 
            this.txtTV1.BackColor = System.Drawing.Color.White;
            this.txtTV1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTV1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtTV1.Location = new System.Drawing.Point(74, 131);
            this.txtTV1.Name = "txtTV1";
            this.txtTV1.Size = new System.Drawing.Size(62, 26);
            this.txtTV1.TabIndex = 4;
            // 
            // txtBrandName
            // 
            this.txtBrandName.BackColor = System.Drawing.Color.White;
            this.txtBrandName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBrandName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtBrandName.Location = new System.Drawing.Point(649, 34);
            this.txtBrandName.Name = "txtBrandName";
            this.txtBrandName.Size = new System.Drawing.Size(126, 26);
            this.txtBrandName.TabIndex = 3;
            // 
            // txtItemName
            // 
            this.txtItemName.BackColor = System.Drawing.Color.White;
            this.txtItemName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtItemName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtItemName.Location = new System.Drawing.Point(424, 34);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(179, 26);
            this.txtItemName.TabIndex = 2;
            // 
            // txtItemCode
            // 
            this.txtItemCode.BackColor = System.Drawing.Color.White;
            this.txtItemCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtItemCode.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtItemCode.Location = new System.Drawing.Point(277, 34);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(104, 26);
            this.txtItemCode.TabIndex = 34;
            // 
            // txtCodeBard
            // 
            this.txtCodeBard.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCodeBard.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtCodeBard.Location = new System.Drawing.Point(74, 34);
            this.txtCodeBard.Name = "txtCodeBard";
            this.txtCodeBard.Size = new System.Drawing.Size(135, 26);
            this.txtCodeBard.TabIndex = 1;
            this.txtCodeBard.Enter += new System.EventHandler(this.txtCodeBard_Enter);
            this.txtCodeBard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(1, 134);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "效期1~2年：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(140, 134);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 50;
            this.label2.Text = "有效期年份=2020";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(140, 182);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 53;
            this.label3.Text = "有效期年份>=2021";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(7, 182);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 52;
            this.label4.Text = "效期>2年：";
            // 
            // txtTV2
            // 
            this.txtTV2.BackColor = System.Drawing.Color.White;
            this.txtTV2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTV2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtTV2.Location = new System.Drawing.Point(74, 179);
            this.txtTV2.Name = "txtTV2";
            this.txtTV2.Size = new System.Drawing.Size(62, 26);
            this.txtTV2.TabIndex = 5;
            // 
            // txtBrandCode
            // 
            this.txtBrandCode.BackColor = System.Drawing.Color.White;
            this.txtBrandCode.Enabled = false;
            this.txtBrandCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBrandCode.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtBrandCode.Location = new System.Drawing.Point(649, 83);
            this.txtBrandCode.Name = "txtBrandCode";
            this.txtBrandCode.Size = new System.Drawing.Size(126, 26);
            this.txtBrandCode.TabIndex = 57;
            this.txtBrandCode.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(704, 193);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 24);
            this.button1.TabIndex = 6;
            this.button1.Text = "保存(&S)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(773, 193);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 24);
            this.button2.TabIndex = 59;
            this.button2.Text = "删除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dataGridView6);
            this.groupBox6.Location = new System.Drawing.Point(74, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(584, 221);
            this.groupBox6.TabIndex = 212;
            this.groupBox6.TabStop = false;
            this.groupBox6.Visible = false;
            // 
            // dataGridView6
            // 
            this.dataGridView6.AllowUserToAddRows = false;
            this.dataGridView6.AllowUserToDeleteRows = false;
            this.dataGridView6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView6.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView6.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView6.ColumnHeadersHeight = 30;
            this.dataGridView6.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemCode,
            this.ItemName,
            this.BrandName,
            this.BrandCode,
            this.Column3,
            this.Column4});
            this.dataGridView6.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView6.Location = new System.Drawing.Point(10, 18);
            this.dataGridView6.MultiSelect = false;
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.ReadOnly = true;
            this.dataGridView6.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView6.RowHeadersVisible = false;
            this.dataGridView6.RowHeadersWidth = 20;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SlateGray;
            this.dataGridView6.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView6.RowTemplate.Height = 23;
            this.dataGridView6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView6.Size = new System.Drawing.Size(568, 194);
            this.dataGridView6.TabIndex = 207;
            this.dataGridView6.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView6_CellContentClick);
            this.dataGridView6.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView6_CellContentDoubleClick);
            this.dataGridView6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView6_KeyDown);
            // 
            // ItemCode
            // 
            this.ItemCode.DataPropertyName = "ItemCode";
            this.ItemCode.FillWeight = 101.5228F;
            this.ItemCode.HeaderText = "货品编号";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.FillWeight = 98.47716F;
            this.ItemName.HeaderText = "货品名称(双击或回车可选择数据行)";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 270;
            // 
            // BrandName
            // 
            this.BrandName.DataPropertyName = "BrandName";
            this.BrandName.HeaderText = "品牌名称";
            this.BrandName.Name = "BrandName";
            this.BrandName.ReadOnly = true;
            // 
            // BrandCode
            // 
            this.BrandCode.DataPropertyName = "BrandCode";
            this.BrandCode.HeaderText = "品牌代码";
            this.BrandCode.Name = "BrandCode";
            this.BrandCode.ReadOnly = true;
            this.BrandCode.Width = 80;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "ID";
            this.Column3.HeaderText = "id";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "CodeBard";
            this.Column4.HeaderText = "CodeBard";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(841, 193);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(67, 24);
            this.button3.TabIndex = 213;
            this.button3.Text = "合并导出";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // saveFileDialog4
            // 
            this.saveFileDialog4.Filter = "EXCEL文件|*.xls";
            this.saveFileDialog4.InitialDirectory = "c:\\";
            this.saveFileDialog4.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog4_FileOk);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(912, 193);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(56, 24);
            this.button4.TabIndex = 214;
            this.button4.Text = "搜索";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(72, 68);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 215;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示运行目录ToolStripMenuItem,
            this.更新货品资料ToolStripMenuItem,
            this.清除记录ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 70);
            // 
            // 显示运行目录ToolStripMenuItem
            // 
            this.显示运行目录ToolStripMenuItem.Name = "显示运行目录ToolStripMenuItem";
            this.显示运行目录ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示运行目录ToolStripMenuItem.Text = "显示运行目录";
            this.显示运行目录ToolStripMenuItem.Click += new System.EventHandler(this.显示运行目录ToolStripMenuItem_Click);
            // 
            // 更新货品资料ToolStripMenuItem
            // 
            this.更新货品资料ToolStripMenuItem.Name = "更新货品资料ToolStripMenuItem";
            this.更新货品资料ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.更新货品资料ToolStripMenuItem.Text = "更新货品资料";
            this.更新货品资料ToolStripMenuItem.Click += new System.EventHandler(this.更新货品资料ToolStripMenuItem_Click);
            // 
            // 清除记录ToolStripMenuItem
            // 
            this.清除记录ToolStripMenuItem.Name = "清除记录ToolStripMenuItem";
            this.清除记录ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.清除记录ToolStripMenuItem.Text = "清除记录";
            this.清除记录ToolStripMenuItem.Click += new System.EventHandler(this.清除记录ToolStripMenuItem_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(973, 193);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(58, 23);
            this.button5.TabIndex = 216;
            this.button5.Text = "上传";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // scanbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 572);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtBrandCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTV2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.txtTakeArea);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtTV1);
            this.Controls.Add(this.txtBrandName);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.txtCodeBard);
            this.Controls.Add(this.button3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizeBox = false;
            this.Name = "scanbar";
            this.Text = "盘点";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.scanbar_FormClosing);
            this.Load += new System.EventHandler(this.scanbar_Load);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txtTakeArea;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTV1;
        private System.Windows.Forms.TextBox txtBrandName;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.TextBox txtCodeBard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTV2;
        private System.Windows.Forms.TextBox txtBrandCode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dataGridView6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示运行目录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更新货品资料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除记录ToolStripMenuItem;
        private System.Windows.Forms.Button button5;
    }
}