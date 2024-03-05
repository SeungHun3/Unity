using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.IO;
using System.Data.OleDb;

namespace ExcelConvertor
{
    class Program
    {
        static void Main(string[] args)
        {

            foreach( string arg in args)
            {
                Console.WriteLine(arg);
            }

            string name = "data";
            string srcPath = "./";
            string dstPath = "./";
            string lang = "csharp";
            string prefix = "";
            bool isMakeSheetPerFile = false;

            if (args.Length > 0) name       = args[0];
            if (args.Length > 1) lang       = args[1].ToLower();
            if (args.Length > 2) srcPath    = args[2].ToLower();
            if (args.Length > 3) dstPath    = args[3].ToLower();
            if (args.Length > 4) prefix     = args[4].ToLower();
            if (args.Length > 5) isMakeSheetPerFile = bool.Parse(args[5]);

            string [] langArray = lang.Split('|');
            CodeMaker.CodeMakerMethods.Initialize();
            List<CodeMaker.CodeMakerInfo> cmi_list = new List<CodeMaker.CodeMakerInfo>();
            for(int i = 0; i < langArray.Length; ++i)
            {
                CodeMaker.CodeMakerInfo cmi;
                if (false == CodeMaker.CodeMakerMethods.Get(langArray[i], out cmi))
                {
                    Console.WriteLine("{0} is unknown language", lang);
                    return;
                }

                cmi_list.Add(cmi);
            }

            ExcelConvertor.ExcelTypes.Factory.Initialize();
            Excel.ManagerExcelSheet.Initialize(name, srcPath);
            Excel.ManagerExcelSheet.Load();
            Excel.ManagerExcelSheet.MakeBinaryFile(dstPath);
            Excel.ManagerExcelSheet.MakeCodeFile(dstPath, cmi_list);
        }
    }
}
