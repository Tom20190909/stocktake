namespace stocktake
{
    partial class bindInvMan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(bindInvMan));
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lblprocessmsg = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(523, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 235;
            this.label5.Text = "盘点人手机号所在列";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(642, 17);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(45, 21);
            this.textBox4.TabIndex = 234;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(405, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 233;
            this.label4.Text = "店铺所在列";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(476, 18);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(45, 21);
            this.textBox3.TabIndex = 232;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 231;
            this.label3.Text = "开始行数";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(237, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(45, 21);
            this.textBox1.TabIndex = 230;
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
            this.Column4,
            this.Column3});
            this.dataGridView6.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView6.Location = new System.Drawing.Point(31, 44);
            this.dataGridView6.MultiSelect = false;
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView6.RowHeadersVisible = false;
            this.dataGridView6.RowHeadersWidth = 20;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SlateGray;
            this.dataGridView6.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView6.RowTemplate.Height = 23;
            this.dataGridView6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView6.Size = new System.Drawing.Size(593, 452);
            this.dataGridView6.TabIndex = 226;
            // 
            // ItemCode
            // 
            this.ItemCode.DataPropertyName = "U_clientid";
            this.ItemCode.FillWeight = 101.5228F;
            this.ItemCode.HeaderText = "店铺编号";
            this.ItemCode.Name = "ItemCode";
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "U_clientName";
            this.ItemName.FillWeight = 98.47716F;
            this.ItemName.HeaderText = "店铺名称";
            this.ItemName.Name = "ItemName";
            this.ItemName.Width = 270;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "U_userid";
            this.Column4.HeaderText = "盘点人手机号码";
            this.Column4.Name = "Column4";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "U_Name";
            this.Column3.HeaderText = "盘点人";
            this.Column3.Name = "Column3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(295, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 225;
            this.label2.Text = "结束行数";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(354, 19);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(45, 21);
            this.textBox2.TabIndex = 224;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(31, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 23);
            this.button3.TabIndex = 223;
            this.button3.Text = "从excel导入基础数据";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(50, 304);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(541, 27);
            this.richTextBox1.TabIndex = 221;
            this.richTextBox1.Text = "";
            this.richTextBox1.Visible = false;
            // 
            // lblprocessmsg
            // 
            this.lblprocessmsg.AutoSize = true;
            this.lblprocessmsg.Location = new System.Drawing.Point(715, 21);
            this.lblprocessmsg.Name = "lblprocessmsg";
            this.lblprocessmsg.Size = new System.Drawing.Size(0, 12);
            this.lblprocessmsg.TabIndex = 220;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "excel文件(*.xls)|*.xls";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(900, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 23);
            this.button1.TabIndex = 236;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.Location = new System.Drawing.Point(631, 45);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(328, 451);
            this.richTextBox2.TabIndex = 237;
            this.richTextBox2.Text = "";
            // 
            // bindInvMan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 529);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.lblprocessmsg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "bindInvMan";
            this.Text = "绑定盘点人";
            this.Load += new System.EventHandler(this.bindInvMan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblprocessmsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}