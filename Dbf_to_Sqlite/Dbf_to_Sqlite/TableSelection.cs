using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Dbf_to_Sqlite
{
    public partial class TableSelection : Form
    {
        public TableSelection()
        {
            InitializeComponent();
        }

        private void read_btn_Click(object sender, EventArgs e)
        {
            MainForm m1 = new MainForm();

            try
            {
                OleDbConnection conn = new OleDbConnection();

                string connStr = @"Provider=VFPOLEDB.1;Data Source=" + m1.FileIndex + ";Collating Sequence=MACHINE";

                conn.ConnectionString = connStr;
                conn.Open();

                string dbf = @"select * from " + m1.FileName;
                OleDbDataAdapter da = new OleDbDataAdapter(dbf, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView_dbf.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
