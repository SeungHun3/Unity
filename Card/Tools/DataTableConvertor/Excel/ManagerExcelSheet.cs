using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.IO;
using System.Data.OleDb;


namespace ExcelConvertor.Excel
{
    public static class ManagerExcelSheet
    {
        private static string _name = "";
        private static string _rootPath = "";
        private static ExcelConvertor.CodeMaker.CodeMakerInfo _codeMakerInfo;

        private static List<DataTable> _tables = new List<DataTable>();
        private static List<Sheet> Sheets = new List<Sheet>();

        public static void Initialize(string name, string rootPath/*, ExcelConvertor.CodeMaker.CodeMakerInfo cmi*/)
        {
            _name = name;
            _rootPath = rootPath;
            //_codeMakerInfo = cmi;
        }
        static void LoadDataTable(DataTable dt)
        {
            DataRow dr = dt.Rows[0]; //row of column type
            int nItemCount = dr.ItemArray.Length;

            // version
            object item = dr.ItemArray[0];
            int version = int.Parse(item.ToString().Replace(" ", ""));
            Console.WriteLine("Version:{0}", version);

            Sheet sheet = new Sheet(dt.TableName, version);
            for (int colIndex = 1; colIndex < dt.Columns.Count; ++colIndex)
            {
                DataColumn dc = dt.Columns[colIndex];
                object itemOfType = dr.ItemArray[colIndex];
                sheet.Columns.Add(new Column(dc.ColumnName, itemOfType.ToString().Replace(" ", "")));
            }

            int nRowCount = dt.Rows.Count;
            for (int i = 1; i < nRowCount; ++i)
            {
                List<string> values = new List<string>();

                DataRow drOfValue = dt.Rows[i];
                for (int n = 1; n < nItemCount; ++n)
                    values.Add(drOfValue.ItemArray[n].ToString());
                
                sheet.Rows.Add(values);
            }
            Sheets.Add(sheet);
        }
        static void LoadExcel(string excelFile)
        {
            if (System.IO.File.Exists(excelFile))
            {
                System.IO.FileInfo fi = new FileInfo(excelFile);

                string filePath = fi.DirectoryName.ToString();
                string fileName = fi.Name.ToString();

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\";", fileName);

                using (OleDbConnection conn = new OleDbConnection(sb.ToString()))
                {
                    conn.Open();
                    try
                    {
                        if (conn.State != ConnectionState.Open)
                            return;

                        var worksheets = conn.GetSchema("Tables");

                        foreach (DataRow dr in worksheets.Rows)
                        {
                            string Query = string.Format(" SELECT A.* FROM [{0}] AS A ", dr["TABLE_NAME"]);

                            OleDbCommand cmd = new OleDbCommand(Query, conn);
                            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                            DataTable dt = new DataTable();
                            da.FillSchema(dt, SchemaType.Source);
                            da.Fill(dt);

                            string tableName = dt.TableName.ToLower();
                            if (tableName.Contains("desc") || tableName.Contains("$") == false)
                                continue;
                            
                            if (dt == null || dt.Rows.Count == 0)
                                continue;

                            LoadDataTable(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        Console.WriteLine("OleDb Exception : {0} FIleName = {1}", ex.Message, fileName);
                    }

                    conn.Close();
                } // using
            }
        }
        static void LoadDirectory(DirectoryInfo rootNode)
        {
            FileInfo[] fis = rootNode.GetFiles("*.xlsx");
            foreach (FileInfo fi in fis)
            {
                if (fi.Name.Contains("~"))
                    continue;
                LoadExcel(fi.FullName);
            }
            
            DirectoryInfo[] dis = rootNode.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                LoadDirectory(di);
            }
        }
        public static void Load()
        {
            DirectoryInfo di = new DirectoryInfo(_rootPath);
            LoadDirectory(di);

            foreach (Sheet sh in Sheets)
            {
                // column info
                for( int i = 0; i < sh.Columns.Count; ++i )
                {
                    Console.Write("[");
                    Console.Write(sh.Columns[i].Name);
                    Console.Write("/");
                    Console.Write(sh.Columns[i].Type.Name);
                    Console.Write("] ");
                }
                Console.WriteLine();
                for (int r = 0; r < sh.Rows.Count; ++r)
                {
                    for (int c = 0; c < sh.Columns.Count; ++c)
                    {
                        Console.Write(sh.Rows[r][c]);
                        Console.Write(", ");
                    }
                    Console.WriteLine();
                }
            }
        }
        public static void MakeBinaryFile(string dstPath)
        {
            foreach (Sheet sh in Sheets)
            {
                string filename = string.Format("{0}/{1}Data.bytes", dstPath, sh.Name.Replace("$", ""));
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter sw = new BinaryWriter(fs);

                sh.Write(sw);

                fs.Flush();
                sw.Close();
                fs.Close();
            }
        }
        
        public static void MakeCodeFile(string dstPath, List<ExcelConvertor.CodeMaker.CodeMakerInfo> codeMakeList)
        {
            for(int i = 0; i < codeMakeList.Count; ++i)
            {
                codeMakeList[i].Func(dstPath, _name, Sheets);
            }
        }
    } 
}
