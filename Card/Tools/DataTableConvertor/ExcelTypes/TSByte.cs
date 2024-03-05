using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExcelConvertor.ExcelTypes
{
    class TSByte : IType
    {
        public eTypeCode TypeCode { get { return eTypeCode.Basic; } }
        public string Name { get { return "sbyte_t"; } }
        public IType TKey { get { throw new System.NotImplementedException(); } }
        public IType TValue { get { throw new System.NotImplementedException(); } }
        public bool Write(BinaryWriter writer, string val)
        {
            sbyte result;
            if (false == sbyte.TryParse(val, out result))
                return false;

            writer.Write(result);
            return true;
        }
    }
}
