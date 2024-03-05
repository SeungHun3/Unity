using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace ExcelConvertor.ExcelTypes
{
    public class TDouble : IType
    {
        public eTypeCode TypeCode { get { return eTypeCode.Basic; } }
        public string Name { get { return "double"; } }
        public IType TKey { get { throw new System.NotImplementedException(); } }
        public IType TValue { get { throw new System.NotImplementedException(); } }
        public bool Write(BinaryWriter writer, string val)
        {
            double result;
            if (false == double.TryParse(val, out result))
                return false;

            writer.Write(result);
            return true;
        }
    }
}
