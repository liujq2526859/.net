namespace Dbf_to_Sqlite
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBox_DbfAddress = new System.Windows.Forms.TextBox();
            this.set_btn = new System.Windows.Forms.Button();
            this.preview_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog_setdbf = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView_dbf = new System.Windows.Forms.DataGridView();
            this.browse_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_SqliteAddress = new System.Windows.Forms.TextBox();
            this.start_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.saveFileDialog_browsesqlite = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_dbf)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_DbfAddress
            // 
            this.textBox_DbfAddress.Location = new System.Drawing.Point(95, 12);
            this.textBox_DbfAddress.Name = "textBox_DbfAddress";
            this.textBox_DbfAddress.Size = new System.Drawing.Size(411, 21);
            this.textBox_DbfAddress.TabIndex = 0;
            // 
            // set_btn
            // 
            this.set_btn.Location = new System.Drawing.Point(512, 10);
            this.set_btn.Name = "set_btn";
            this.set_btn.Size = new System.Drawing.Size(75, 23);
            this.set_btn.TabIndex = 1;
            this.set_btn.Text = "Set";
            this.set_btn.UseVisualStyleBackColor = true;
            this.set_btn.Click += new System.EventHandler(this.set_btn_Click);
            // 
            // preview_btn
            // 
            this.preview_btn.Location = new System.Drawing.Point(593, 10);
            this.preview_btn.Name = "preview_btn";
            this.preview_btn.Size = new System.Drawing.Size(75, 23);
            this.preview_btn.TabIndex = 2;
            this.preview_btn.Text = "Preview";
            this.preview_btn.UseVisualStyleBackColor = true;
            this.preview_btn.Click += new System.EventHandler(this.preview_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Dbf Address:";
            // 
            // openFileDialog_setdbf
            // 
            this.openFileDialog_setdbf.FileName = "openFileDialog_setdbf";
            // 
            // dataGridView_dbf
            // 
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.dataGridView_dbf.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_dbf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_dbf.Location = new System.Drawing.Point(12, 39);
            this.dataGridView_dbf.Name = "dataGridView_dbf";
            this.dataGridView_dbf.RowTemplate.Height = 23;
            this.dataGridView_dbf.Size = new System.Drawing.Size(656, 178);
            this.dataGridView_dbf.TabIndex = 4;
            // 
            // browse_btn
            // 
            this.browse_btn.Location = new System.Drawing.Point(512, 219);
            this.browse_btn.Name = "browse_btn";
            this.browse_btn.Size = new System.Drawing.Size(155, 23);
            this.browse_btn.TabIndex = 5;
            this.browse_btn.Text = "Browse...";
            this.browse_btn.UseVisualStyleBackColor = true;
            this.browse_btn.Click += new System.EventHandler(this.browse_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "SQLite File Path:";
            // 
            // textBox_SqliteAddress
            // 
            this.textBox_SqliteAddress.Location = new System.Drawing.Point(125, 221);
            this.textBox_SqliteAddress.Name = "textBox_SqliteAddress";
            this.textBox_SqliteAddress.Size = new System.Drawing.Size(381, 21);
            this.textBox_SqliteAddress.TabIndex = 7;
            // 
            // start_btn
            // 
            this.start_btn.Location = new System.Drawing.Point(414, 458);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(149, 23);
            this.start_btn.TabIndex = 9;
            this.start_btn.Text = "Start The Conversion Process";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(592, 458);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.cancel_btn.TabIndex = 10;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 493);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.start_btn);
            this.Controls.Add(this.textBox_SqliteAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.browse_btn);
            this.Controls.Add(this.dataGridView_dbf);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.preview_btn);
            this.Controls.Add(this.set_btn);
            this.Controls.Add(this.textBox_DbfAddress);
            this.Name = "MainForm";
            this.Text = "主界面";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_dbf)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_DbfAddress;
        private System.Windows.Forms.Button set_btn;
        private System.Windows.Forms.Button preview_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog_setdbf;
        private System.Windows.Forms.DataGridView dataGridView_dbf;
        private System.Windows.Forms.Button browse_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_SqliteAddress;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_browsesqlite;
    }
}

