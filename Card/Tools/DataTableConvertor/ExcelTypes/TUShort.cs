using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExcelConvertor.ExcelTypes
{
    class TUShort : IType
    {
        public eTypeCode TypeCode { get { return eTypeCode.Basic; } }
        public string Name { get { return "ushort_t"; } }
        public IType TKey { get { throw new System.NotImplementedException(); } }
        public IType TValue { get { throw new System.NotImplementedException(); } }
        public bool Write(BinaryWriter writer, string val)
        {
            ushort result;
            if (false == ushort.TryParse(val, out result))
                return false;

            writer.Write(result);
            return true;
        }
    }
}
