using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExcelConvertor.ExcelTypes
{
    public enum eTypeCode : byte
    {
        Basic = 0,
        List,
        Map,
        String,
        BigInt,
        Skip,
    }
    public interface IType
    {
        eTypeCode TypeCode { get; }
        string Name { get; }
        IType TKey { get; }
        IType TValue { get; }
        bool Write(BinaryWriter stream, string valueString);
    }
}
