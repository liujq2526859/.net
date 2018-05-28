using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.Odbc;

namespace Dbf_to_Sqlite
{
    class CSVHelper
    {
        #region Fields
        string _fileName;//文件名
        DataTable _dataSource;//数据源
        string[] _titles = null;//列标题(A、B、C之类)
        string[] _fields = null;//字段名

        List<string> listTitles = new List<string>();
        List<string> listFields = new List<string>();
        #endregion

        #region .ctor
        /// <summary> 
        /// 构造函数 
        /// </summary> 
        public CSVHelper()
        { 
        
        }

        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="titles">要输出到 Excel 的列标题的数组</param> 
        /// <param name="fields">要输出到 Excel 的字段名称数组</param> 
        /// <param name="dataSource">数据源</param> 
        public CSVHelper(string[] titles, string[] fields, DataTable dataSource)
            : this(titles, dataSource)
        {
            if (fields == null || fields.Length == 0)
            {
                throw new ArgumentException("fields");
            }
            if (titles.Length != fields.Length)
            {
                throw new ArgumentException("titles.Length != fields.Length", "fields");
            }
            _fields = fields;
        }
        public CSVHelper(List<string> titles, List<string> fields, DataTable dataSource)
            : this(titles, dataSource)
        {
            if (titles == null || titles.Count == 0)
            {
                throw new ArgumentException("fields");
            }
            if (titles.Count != fields.Count)
            {
                throw new ArgumentException("titles.Count != fields.Count", "fields");
            }
            listFields = fields;
        }

        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="titles">要输出到 Excel 的列标题的数组</param> 
        /// <param name="dataSource">数据源</param> 
        public CSVHelper(string[] titles, DataTable dataSource)
            : this(dataSource)
        { 
            if (titles == null || titles.Length == 0)
            {
                throw new ArgumentException("titles");
            }
            _titles = titles;
        }
        public CSVHelper(List<string> titles, DataTable dataSource)
            : this(dataSource)
        {
            if (titles == null || titles.Count == 0)
            {
                throw new ArgumentNullException("titles");
            }
            listTitles = titles;
        }

        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="dataSource">数据源</param> 
        public CSVHelper(DataTable dataSource)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException("dataSource");
            }
            // maybe more checks needed here (IEnumerable, IList, IListSource, ) ??? 
            // 很难判断，先简单的使用 DataTable 
            _dataSource = dataSource;
        }
        #endregion

        #region public Methods
        #region 导出到CSV文件并且提示下载
        /// <summary>
        /// 导出到CSV文件并且提示下载
        /// </summary>
        /// <param name="fileName"></param>
        //public byte[] DataToCSV(string fileName)
        //{

        //    return System.Text.Encoding.Default.GetBytes(data);
        //}
        #endregion

        /// <summary>
        /// 获取CSV导入的数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">文件名称(.csv不用加)</param>
        /// <returns></returns>
        public DataTable GetCsvData(string filePath, string fileName)
        {
            string path = Path.Combine(filePath, fileName + ".csv");
            string connString = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + filePath + ";Extensions=asc,csv,tab,txt;";
            try
            {
                using (OdbcConnection odbcConn = new OdbcConnection(connString))
                {
                    odbcConn.Open();
                    OdbcCommand oleComm = new OdbcCommand();
                    oleComm.Connection = odbcConn;
                    oleComm.CommandText = "select * from [" + fileName + "#csv]";

                    OdbcDataAdapter adapter = new OdbcDataAdapter(oleComm);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, fileName);

                    odbcConn.Close();
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                throw ex;
            }
        }
        #endregion

        #region 返回写入CSV的字符串
        /// <summary>
        /// 返回写入CSV的字符串
        /// </summary>
        /// <returns></returns>
        private string ExportCSV()
        {
            if (_dataSource == null)
            { 
                throw new ArgumentNullException("dataSource");
            }
            StringBuilder strData = new StringBuilder();
            if (_titles == null)
            {
                //添加列名
                foreach (DataColumn column in _dataSource.Columns)
                {
                    strData.Append(column.ColumnName + ",");
                }
                strData.Append("\n");
                foreach (DataRow dr in _dataSource.Rows)
                {
                    //添加列数据
                    for (int i = 0; i < _dataSource.Columns.Count; i++)
                    {
                        strData.Append(dr[i].ToString() + ",");
                    }
                    strData.Append("\n");
                }
                return strData.ToString();
            }
            else
            {
                foreach (string columnName in _titles)
                {
                    strData.Append(columnName + ",");                    
                }
                strData.Append("\n");
                if (_fields == null)
                {
                    foreach (DataRow dr in _dataSource.Rows)
                    {
                        for (int i = 0; i < _dataSource.Columns.Count; i++)
                        {
                            strData.Append(dr[i].ToString() + ",");
                        }
                        strData.Append("\n");
                    }
                    return strData.ToString();
                }
                else
                {
                    foreach (DataRow dr in _dataSource.Rows)
                    {
                        for (int i = 0; i < _fields.Length; i++)
                        {
                            strData.Append(dr[_fields[i]].ToString() + ",");
                        }
                        strData.Append("\n");
                    }
                    return strData.ToString();
                }
            }
        }
        #endregion

        //private string ExportCSVForActiveTitle()
        //{ 
            
        //}
    }
}
