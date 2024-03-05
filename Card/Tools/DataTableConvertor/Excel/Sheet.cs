using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ExcelConvertor.ExcelTypes;
namespace ExcelConvertor.Excel
{
    public class Sheet
    {
        public string Name { get; private set; }
        public int Ver { get; private set; }
        public List<Column> Columns = new List<Column>();
        public List<List<string>> Rows = new List<List<string>>();

        public Sheet(string name, int version)
        {
            Name = name;
            Ver = version;
        }
        public void Write(BinaryWriter sw)
        {
            // Version Write
            sw.Write((UInt16)Ver);

            // Data Count
            sw.Write((UInt16)Rows.Count);

            Dictionary<int, int> serialNoDic = new Dictionary<int, int>();

            bool serialNoFound = false;

            for(int r = 0; r < Rows.Count; ++r)
            {
                for (int c = 0; c < Columns.Count; ++c)
                {
                    if (Columns[c].Name == "SerialNo")
                    {
                        serialNoFound = true;
                        int serialNo;
                        if (false == int.TryParse(Rows[r][c], out serialNo))
                        {
                            Console.WriteLine("Error SerialNo Parse Table Name = {0}", Name);
                            break;
                        }

                        if (serialNoDic.ContainsKey(serialNo))
                        {
                            Console.WriteLine("Error Contains SerialNo Table Name = {0} SerialNo = {1}", Name, serialNo);
                            break;
                        }

                        serialNoDic.Add(serialNo, serialNo);

                        // serial No 
                        sw.Write(serialNo);
                    }
                    else
                    {
                        IType type = Columns[c].Type;
                        type.Write(sw, Rows[r][c]);
                    }
                }
            }

            // not found serial no
            if (serialNoFound == false)
            {
                Console.WriteLine("Not Found SerialNo Table Name = {0}", Name);
                return;
            }
        }
        //public void MakeCode(StringBuilder sb, ExcelConvertor.CodeMaker.TDelegateMakeCode _FuncCodeMake)
        //{
        //    _FuncCodeMake(sb, this);
        //}

        public void MakeCreateInsertQuery(StringBuilder sb)
        {
            StringBuilder header = new StringBuilder();
            header.AppendFormat("INSERT INTO [{0}DataTable](", Name.Replace("$", ""));
            for (int c = 0; c < Columns.Count; ++c)
            {
                if (c == Columns.Count - 1)
                {
                    header.AppendFormat("[{0}])", Columns[c].Name);
                }
                else
                {
                    header.AppendFormat("[{0}],", Columns[c].Name);
                }
            }

            header.AppendLine();

            for (int r = 0; r < Rows.Count; ++r)
            {
                sb.Append(header.ToString());
                sb.AppendLine("VALUES");
                for (int c = 0; c < Columns.Count; ++c)
                {
                    if(c == 0)
                    {
                        sb.AppendFormat("('{0}',", Rows[r][c]);
                    }
                    else if(c == Columns.Count - 1)
                    {
                        sb.AppendFormat("'{0}')", Rows[r][c]);
                    }
                    else
                    {
                        sb.AppendFormat("'{0}',", Rows[r][c]);
                    }
                }
                sb.AppendLine("");
                sb.AppendLine("GO");
                sb.AppendLine("");
            }
        }

        public void MakeCreateVersionQuery(StringBuilder sb)
        {

        }

    }
}
