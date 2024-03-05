using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExcelConvertor.ExcelTypes
{
    // int64
    public class TLong : IType
    {
        static string typeName = "long";

        public eTypeCode TypeCode { get { return eTypeCode.Basic; } }
        public string Name { get { return typeName; } }
        public IType TKey { get { throw new System.NotImplementedException(); } }
        public IType TValue { get { throw new System.NotImplementedException(); } }
        public bool Write(BinaryWriter writer, string val)
        {
            long result;
            if (false == long.TryParse(val, out result))
                return false;

            writer.Write(result);
            return true;
        }
    }
}
