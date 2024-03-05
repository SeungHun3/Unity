using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExcelConvertor.ExcelTypes
{
    class TMemo : IType
    {
        public eTypeCode TypeCode { get { return eTypeCode.Skip; } }
        public string Name { get { return "memo"; } }
        public IType TKey { get { throw new System.NotImplementedException(); } }
        public IType TValue { get { throw new System.NotImplementedException(); } }
        public bool Write(BinaryWriter writer, string val)
        {
            return true;
        }
    }
}
