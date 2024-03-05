using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ExcelConvertor.Excel;

namespace ExcelConvertor.CodeMaker
{
    public delegate void TDelegateMakeCode(string dstPath, string name, List<Sheet> sheets);

    public struct CodeMakerInfo
    {
        public TDelegateMakeCode Func;

        public CodeMakerInfo(TDelegateMakeCode f)
        {
            Func = f;
        }
    }

    public static class CodeMakerMethods
    {
        static Dictionary<string, CodeMakerInfo> _mapCodeMakeMethods = new Dictionary<string, CodeMakerInfo>();
        public static void Initialize()
        {
            _mapCodeMakeMethods.Add("csharp", new CodeMakerInfo(MakeCSharpParentCode));
        }

        public static bool Get(string lang, out CodeMakerInfo cmi)
        {
            if (_mapCodeMakeMethods.TryGetValue(lang, out cmi))
                return true;

            cmi = default(CodeMakerInfo);
            return false;
        }
        private static string GetRead(ExcelTypes.IType type)
        {
            switch (type.Name)
            {
                case "bool": return "ReadBoolean";
                case "byte_t": return "ReadByte";
                case "short": return "ReadInt16";
                case "int": return "ReadInt32";
                case "long": return "ReadInt64";
                case "float": return "ReadSingle";
                case "string": return "ReadString";
                case "uint_t": return "ReadUInt32";
                case "ushort_t": return "ReadUInt16";
                case "sbyte_t": return "ReadSByte";
                case "ulong_t": return "ReadUInt64";
                case "double": return "ReadDouble";
            }
            return "";
        }

        private static string GetStringRead(ExcelTypes.IType type)
        {
            switch (type.Name)
            {
                case "bool": return "bool.Parse";
                case "byte_t": return "byte.Parse";
                case "short": return "short.Parse";
                case "int": return "int.Parse";
                case "long": return "long.Parse";
                case "float": return "float.Parse";
                case "string": return "";
                case "uint_t": return "uint.Parse";
                case "ushort_t": return "ushort.Parse";
                case "sbyte_t": return "sbyte.Parse";
                case "ulong_t": return "ulong.Parse";
                case "double": return "double.Parse";

            }
            return "";
        }

        private static void MakeCSharpParentCode(string dstPath, string name, List<Sheet> sheets)
        {
            string filename = string.Format("{0}/{1}.{2}", dstPath, name, "cs");
            FileStream fs = new FileStream(filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.IO;");
            sb.AppendLine();
            sb.AppendLine("using byte_t = System.Byte;");
            sb.AppendLine("using sbyte_t = System.SByte;");
            sb.AppendLine("using uint_t = System.UInt32;");
            sb.AppendLine("using ulong_t = System.UInt64;");
            sb.AppendLine("using ushort_t = System.UInt16;");
            sb.AppendLine();
            sb.AppendLine("namespace DataTable");
            sb.AppendLine("{");
            sb.AppendLine();
            sb.AppendLine("public class DataTableBase");
            sb.AppendLine("{");
            sb.AppendLine("}");
            sb.AppendLine();
            sb.AppendLine("public class DataTableListBase");
            sb.AppendLine("{");
            sb.AppendLine("\tpublic Dictionary<int,DataTableBase> DataList = new Dictionary<int,DataTableBase>();");
            sb.AppendLine("\tpublic virtual void Load(BinaryReader reader)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t}");
            sb.AppendLine();
            sb.AppendLine("\tpublic DataTableBase GetDataTable(int serialNo)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tDataTableBase dataTableBase = null;");
            sb.AppendLine("\t\tif(DataList.TryGetValue(serialNo, out dataTableBase) == false)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\treturn null;");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t\treturn dataTableBase;");
            sb.AppendLine("\t}");
            sb.AppendLine();
            sb.AppendLine("\tpublic UInt16 Version = 0;");
            sb.AppendLine();
            sb.AppendLine("}");
            sb.AppendLine();
            foreach (Sheet sh in sheets)
            {
                MakeCSharpCode(sb, sh);
            }

            sb.AppendLine();
            sb.AppendLine("public class DataTableLoader");
            sb.AppendLine("{");
            sb.AppendLine("\tprotected Dictionary<string, DataTableListBase> _datatableList = new Dictionary<string, DataTableListBase>();");
            sb.AppendLine();
            sb.AppendLine("\tpublic DataTableLoader(string dstPath)");
            sb.AppendLine("\t{");
            foreach (Sheet sh in sheets)
            {
                MakeCSharpLoadCode(sb, sh);
            }
            sb.AppendLine("\t}");
            sb.AppendLine();

            sb.AppendLine("\tpublic DataTableBase GetDataTable(string dataName, int serialNo)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tDataTableListBase dataTableListBase = null;");
            sb.AppendLine("\t\tif(_datatableList.TryGetValue(dataName, out dataTableListBase) == false)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\treturn null;");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t\treturn dataTableListBase.GetDataTable(serialNo);");
            sb.AppendLine("\t}");
            sb.AppendLine();

            sb.AppendLine("\tpublic DataTableListBase GetDataTableList(string dataName)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tDataTableListBase dataTableListBase = null;");
            sb.AppendLine("\t\tif(_datatableList.TryGetValue(dataName, out dataTableListBase) == false)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\treturn null;");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t\treturn dataTableListBase;");
            sb.AppendLine("\t}");
            sb.AppendLine();

            sb.AppendLine("\tpublic void Load(string dstPath, string fileName, string dataName, DataTableListBase datatableList)");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\ttry");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tstring dataPath = string.Format(\"{0}/{1}\", dstPath, fileName);");
            sb.AppendLine("\t\t\tif (!File.Exists(dataPath))");
            sb.AppendLine("\t\t\t{");
            sb.AppendLine("\t\t\t\treturn;");
            sb.AppendLine("\t\t\t}");
            sb.AppendLine();
            sb.AppendLine("\t\t\tusing (BinaryReader reader = new BinaryReader(File.Open(dataPath, FileMode.Open)))");
            sb.AppendLine("\t\t\t{");
            sb.AppendLine("\t\t\t\tdatatableList.Load(reader);");
            sb.AppendLine("\t\t\t}");
            sb.AppendLine();
            sb.AppendLine("\t\t\tif(_datatableList.ContainsKey(dataName))");
            sb.AppendLine("\t\t\t{");
            sb.AppendLine("\t\t\t\t_datatableList.Remove(dataName);");
            sb.AppendLine("\t\t\t}");
            sb.AppendLine();
            sb.AppendLine("\t\t\t_datatableList.Add(dataName, datatableList);");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t\tcatch");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");

            sb.AppendLine("}");
            sb.AppendLine();
            sb.AppendLine("}");

            sw.WriteLine(sb.ToString());
            fs.Flush();
            sw.Close();
            fs.Close();
        }

        private static void MakeCSharpCode(StringBuilder sb, Excel.Sheet sh)
        {
            Master_MakeCSharpCode(sb, sh);
            sb.AppendFormat("public class {0}DataTable : DataTableBase", sh.Name.Replace("$", ""));
            sb.AppendLine();
            sb.AppendLine("{");
            
            StringBuilder loadSB = new StringBuilder();
            foreach (Excel.Column c in sh.Columns)
            {
                if (c.Name == "SerialNo")
                {
                    continue;
                }

                switch (c.Type.TypeCode)
                {
                    case ExcelTypes.eTypeCode.Basic:
                        sb.AppendFormat("\tpublic {0} {1};", c.Type.Name, c.Name);
                        sb.AppendLine();
                        loadSB.AppendFormat("\t\t{0} = reader.{1}();", c.Name, GetRead(c.Type));
                        loadSB.AppendLine();
                        break;
                    case ExcelTypes.eTypeCode.String:
                        sb.AppendFormat("\tpublic {0} {1};", c.Type.Name, c.Name);
                        sb.AppendLine();
                        loadSB.AppendFormat("\t\tuint_t {0}Count = reader.ReadUInt32();", c.Name);
                        loadSB.AppendLine();
                        loadSB.AppendFormat("\t\tbyte[] {0}Value = reader.ReadBytes((int){0}Count);", c.Name, c.Name);
                        loadSB.AppendLine();
                        loadSB.AppendFormat("\t\t{0} = System.Text.Encoding.UTF8.GetString({0}Value);", c.Name, c.Name);
                        loadSB.AppendLine();
                        break;
                    case ExcelTypes.eTypeCode.List:
                        sb.AppendFormat("\tpublic List<{0}> {1};", c.Type.TValue.Name, c.Name);
                        sb.AppendLine();
                        loadSB.AppendFormat("\t\t{0} = new List<{1}>();", c.Name, c.Type.TValue.Name);
                        loadSB.AppendLine();
                        loadSB.AppendFormat("\t\tuint_t {0}Count = reader.ReadUInt32();", c.Name);
                        loadSB.AppendLine();
                        loadSB.AppendFormat("\t\tfor( uint_t i = 0; i < {0}Count; ++i)", c.Name);
                        loadSB.AppendLine();
                        loadSB.AppendLine("\t\t{");
                        if (c.Type.TValue.TypeCode == ExcelTypes.eTypeCode.String)
                        {
                            loadSB.AppendFormat("\t\t\tuint_t tempCount = reader.ReadUInt32();");
                            loadSB.AppendLine();
                            loadSB.AppendFormat("\t\t\tbyte[] tempValue = reader.ReadBytes((int)tempCount);");
                            loadSB.AppendLine();
                            loadSB.AppendFormat("\t\t\tstring temp = System.Text.Encoding.UTF8.GetString(tempValue);");
                        }
                        else
                        {
                            loadSB.AppendFormat("\t\t\t{0} temp = reader.{1}();", c.Type.TValue.Name, GetRead(c.Type.TValue));
                        }
                        loadSB.AppendLine();
                        loadSB.AppendFormat("\t\t\t{0}.Add(temp);", c.Name);
                        loadSB.AppendLine();
                        loadSB.AppendLine("\t\t}");
                        break;
                }
            }
            sb.AppendLine("\tpublic void Load(BinaryReader reader)");
            sb.AppendLine("\t{");
            sb.Append(loadSB.ToString());
            sb.AppendLine("\t}");
            sb.AppendLine();

            sb.AppendLine("}");
            sb.AppendLine();
        }

        private static void Master_MakeCSharpCode(StringBuilder sb, Excel.Sheet sh)
        {
            sb.AppendFormat("public class {0}DataTable_List : DataTableListBase", sh.Name.Replace("$", ""));
            sb.AppendLine();
            sb.AppendLine("{");
            sb.AppendFormat("\tpublic const string NAME = {0}{1}{2};", @"""", sh.Name.Replace("$", ""), @"""");
            sb.AppendLine();
            sb.AppendFormat("\tpublic const string DATAFILENAME = {0}{1}Data.bytes{2};", @"""", sh.Name.Replace("$", ""), @"""");
            sb.AppendLine();
            
            sb.AppendLine("\tpublic override void Load(BinaryReader reader)");
            sb.AppendLine("\t{");
            sb.AppendFormat("\t\tVersion = reader.ReadUInt16();");
            sb.AppendLine();
            sb.AppendFormat("\t\tushort_t data_count = reader.ReadUInt16();");
            sb.AppendLine();
            sb.AppendLine("\t\tfor(ushort_t i = 0; i < data_count; ++i)");
            sb.AppendLine("\t\t{");
            sb.AppendFormat("\t\t\tint serialNo = reader.ReadInt32();");
            sb.AppendLine();
            sb.AppendFormat("\t\t\t{0}DataTable data = new {0}DataTable();", sh.Name.Replace("$", ""));
            sb.AppendLine();
            sb.AppendFormat("\t\t\tdata.Load(reader);");
            sb.AppendLine();
            sb.AppendFormat("\t\t\tDataList.Add(serialNo, data);", sh.Name.Replace("$", ""));
            sb.AppendLine();
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine();

            sb.AppendLine("}");
            sb.AppendLine();
        }

        private static void MakeCSharpLoadCode(StringBuilder sb, Excel.Sheet sh)
        {
            sb.AppendFormat("\t\tLoad(dstPath, {0}DataTable_List.DATAFILENAME, {0}DataTable_List.NAME, new {0}DataTable_List());", sh.Name.Replace("$", ""));
            sb.AppendLine();
        }
    }
}
