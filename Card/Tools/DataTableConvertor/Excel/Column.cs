using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ExcelConvertor.ExcelTypes;
namespace ExcelConvertor.Excel
{
    public class Column
    {
        public string Name { get; protected set; }
        public IType Type { get; protected set; }

        public Column(string columnName, string typeName)
        {
            this.Name = columnName;
            this.Type = Factory.Get(typeName);
            if( Type == null )
            {
                throw new InvalidDataException(string.Format("{0}-{1} don't support", columnName, typeName));
            }
        }
    }
}
