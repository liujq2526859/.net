using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Data.SQLite;

namespace Dbf_to_Sqlite
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        string FilePath;
        public string FileName;
        public string FileIndex;
        bool FileOK;

        private void set_btn_Click(object sender, EventArgs e)
        {
            openFileDialog_setdbf.InitialDirectory = "c:\\";//初始加载路径为C盘；
            openFileDialog_setdbf.Filter = "DBF文件(*.a)|*.a";//过滤你想设置的文本文件类型
            openFileDialog_setdbf.RestoreDirectory = true;//保存对话框是否记忆上次打开的目录
            if (this.openFileDialog_setdbf.ShowDialog() == DialogResult.OK)
            {
                textBox_DbfAddress.Text = Path.GetFileName(openFileDialog_setdbf.FileName);
                FilePath = System.IO.Path.GetFullPath(openFileDialog_setdbf.FileName);
                FileName = openFileDialog_setdbf.SafeFileName;
                FileOK = true;
            }
        }

        private void preview_btn_Click(object sender, EventArgs e)
        {
            if (FileOK == true)
            {
                FileInfo fi = new FileInfo(FilePath);
                FileIndex = fi.DirectoryName;
                string table = FilePath;


                try
                {
                    OleDbConnection conn = new OleDbConnection();

                    string connStr = @"Provider=VFPOLEDB.1;Data Source=" + FileIndex + ";Collating Sequence=MACHINE";

                    conn.ConnectionString = connStr;
                    conn.Open();

                    string dbf = @"select * from " + FileName;
                    OleDbDataAdapter da = new OleDbDataAdapter(dbf, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView_dbf.DefaultCellStyle.Format = "0";
                    dataGridView_dbf.DataSource = dt;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                FileOK = false;
                MessageBox.Show("请选择文件！");
            }
        }

        private void browse_btn_Click(object sender, EventArgs e)
        {
            string fpath;
            saveFileDialog_browsesqlite.Filter = "SQLite 数据库(*.db)|*.db";
            saveFileDialog_browsesqlite.FileName = "newSQLite";
            saveFileDialog_browsesqlite.RestoreDirectory = true;
            saveFileDialog_browsesqlite.CheckPathExists = true;

            if (saveFileDialog_browsesqlite.ShowDialog() == DialogResult.OK)
            {
                fpath = saveFileDialog_browsesqlite.FileName;
                textBox_SqliteAddress.Text = fpath;
            }
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection();

                string connStr = @"Provider=VFPOLEDB.1;Data Source=" + FileIndex + ";Collating Sequence=MACHINE";

                conn.ConnectionString = connStr;
                conn.Open();

                string dbf = @"select * from " + FileName;
                OleDbDataAdapter oldadapter = new OleDbDataAdapter(dbf, conn);
                DataTable dt = new DataTable();
                oldadapter.Fill(dt);

                string path = textBox_SqliteAddress.Text.ToString();
  
                SQLiteConnection cn = new SQLiteConnection("data source=" + path);
                cn.Open();

                string cmd = "SELECT count(*) from sqlite_master where type='table' and name='tableName'";;
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd,cn);
                DataTable dt1;
                dt1 = dt.Copy();
                da.Fill(dt1);
                da.Update(dt1);

                cn.Close();
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
