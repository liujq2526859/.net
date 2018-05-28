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
using System.Data.Common;


namespace Dbf_to_Sqlite
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        string FilePath;
        string FilePath_s;
        public string FileName;
        public string FileName_s;
        public string FileIndex;
        public string FileIndex_s;
        bool FileOK;

        private void set_btn_Click(object sender, EventArgs e)
        {
            openFileDialog_setdbf.InitialDirectory = "e:\\";//初始加载路径为C盘；
            openFileDialog_setdbf.Filter = "DBF文件(*.a)|*.a";//过滤你想设置的文本文件类型
            openFileDialog_setdbf.RestoreDirectory = true;//保存对话框是否记忆上次打开的目录
            if (this.openFileDialog_setdbf.ShowDialog() == DialogResult.OK)
            {
                textBox_DbfAddress.Text = Path.GetFileName(openFileDialog_setdbf.FileName);
                FilePath = System.IO.Path.GetFullPath(openFileDialog_setdbf.FileName);
                FileName = textBox_DbfAddress.Text;
                FileName_s = System.IO.Path.GetFileNameWithoutExtension(FileName);
                FilePath_s = Path.GetDirectoryName(openFileDialog_setdbf.FileName);
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

                    string connStr = @"Provider=VFPOLEDB.1;Data Source=" + FilePath + ";Collating Sequence=MACHINE";

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
            //saveFileDialog_browsesqlite.Filter = "SQLite 数据库(*.db)|*.db";
            //saveFileDialog_browsesqlite.FileName = "newSQLite";
            //saveFileDialog_browsesqlite.RestoreDirectory = true;
            //saveFileDialog_browsesqlite.CheckPathExists = true;

            //if (saveFileDialog_browsesqlite.ShowDialog() == DialogResult.OK)
            //{
            //    FileName_s = saveFileDialog_browsesqlite.FileName;
            //    textBox_SqliteAddress.Text = FileName_s;
            //}

            openFileDialog_setdbf.InitialDirectory = "e:\\";//初始加载路径为C盘；
            openFileDialog_setsqlite.Filter = "DB文件(*.db)|*.db";//过滤你想设置的文本文件类型
            openFileDialog_setsqlite.RestoreDirectory = true;//保存对话框是否记忆上次打开的目录
            if (this.openFileDialog_setsqlite.ShowDialog() == DialogResult.OK)
            {
                textBox_SqliteAddress.Text = Path.GetFileName(openFileDialog_setsqlite.FileName);
                FilePath_s = System.IO.Path.GetFullPath(openFileDialog_setsqlite.FileName);
            }
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            DbProviderFactory factory = SQLiteFactory.Instance;
            using (DbConnection conn = factory.CreateConnection())
            {
                OleDbConnection Conn = new OleDbConnection();
                DataTable dt = new DataTable();

                string connStr = @"Provider=VFPOLEDB.1;Data Source=" + FileIndex + ";Collating Sequence=MACHINE";
                Conn.ConnectionString = connStr;
                Conn.Open();

                string dbf = @"select * from " + FileName;
                OleDbDataAdapter oldadapter = new OleDbDataAdapter(dbf, Conn);
                oldadapter.AcceptChangesDuringFill = false;
                oldadapter.Fill(dt);


                string sql = @"select * from " + FileName_s;
                conn.ConnectionString = "data source=" + FilePath_s;
                DataTable dt1 = new DataTable();
                conn.Open();

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn.ConnectionString); ;
                //da.AcceptChangesDuringFill = false;
                da.Fill(dt1);

                string strInsert;
                strInsert = "insert into " + FileName_s + " (";

                List<string> mycolumns_low = new List<string>();
                List<string> mycolumns_upper = new List<string>();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string columnName = dt.Columns[i].ColumnName;
                    string ColumnName = columnName.ToUpper();
                    mycolumns_low.Add(columnName);
                    mycolumns_upper.Add(ColumnName);
                }

                List<string> newmycolumns = new List<string>();
                for (int i = 0; i < dt1.Columns.Count; i++)
                {
                    string columnName = dt1.Columns[i].ColumnName;

                    newmycolumns.Add(columnName);

                    if (i > 0)
                    {
                        strInsert += "\"" + columnName + "\"" + ",";
                    }

                }

                strInsert = strInsert.Substring(0, strInsert.Length - 1);
                strInsert += ")";
                strInsert = strInsert + " values(";

                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string str1 = strInsert;
                        string str = null;
                        for (int j = 1; j < dt1.Columns.Count; j++)
                        {
                            int k;
                            for (k = 0; k < dt.Columns.Count; k++)
                            {
                                if (newmycolumns[j] == mycolumns_upper[k])
                                {
                                    if (dt.Rows[i].ItemArray[k].ToString() != "True" && dt.Rows[i].ItemArray[k].ToString() != "False")
                                    {
                                        str = dt.Rows[i].ItemArray[k].ToString();
                                        str = str.Trim();
                                        str1 += "\"" + str + "\"" + ",";
                                        break;
                                    }
                                    else
                                    {
                                        if (dt.Rows[i].ItemArray[k].ToString() == "False")
                                        {
                                            str1 += "0,";
                                        }
                                        else
                                        {
                                            str1 += "1,";
                                        }
                                        break;
                                    }
                                }
                                if ((k + 1) == dt.Columns.Count)
                                {
                                    str1 += "\"" + "" + "\"" + ",";
                                    break;
                                }
                            }
                        }
                        str1 = str1.Substring(0, str1.Length - 1);
                        str1 += ")";

                        DbCommand cmd = conn.CreateCommand();
                        cmd.CommandText = str1;
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    trans.Rollback();
                    throw;
                }
                conn.Close();
                Conn.Close();
            }
        }

        string UTF8ToGb2312(string str)
        {
            string gb2312info = string.Empty;

            Encoding utf8 = Encoding.UTF8;
            Encoding gb2312 = Encoding.GetEncoding("gb2312");

            byte[] unicodeBytes = utf8.GetBytes(str);

            byte[] asciiBytes = Encoding.Convert(utf8, gb2312, unicodeBytes);

            char[] asciiChars = new char[gb2312.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            gb2312.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            gb2312info = new string(asciiChars);

            return gb2312info;

        }

        public string StringToUnicode(string s)
        {
            char[] charbuffers = s.ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charbuffers.Length; i++)
            {
                buffer = System.Text.Encoding.Unicode.GetBytes(charbuffers[i].ToString());
                sb.Append(String.Format("//u{0:X2}{1:X2}", buffer[1], buffer[0]));
            }
            return sb.ToString();
        }

        private void show_btn_Click(object sender, EventArgs e)
        {
            string sql = @"select * from " + FileName_s;
            SQLiteConnection conn = new SQLiteConnection("data source=" + FilePath_s);
            DataTable dt1 = new DataTable();
            conn.Open();

            SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn); ;
            //da.AcceptChangesDuringFill = false;
            da.Fill(dt1);

            dataGridView_sqlite.DefaultCellStyle.Format = "0";
            dataGridView_sqlite.DataSource = dt1;

            conn.Close();
        }
  

    }
}
