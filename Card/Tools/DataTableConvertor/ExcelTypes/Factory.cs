using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExcelConvertor.ExcelTypes
{
    public static class Factory
    {
        public delegate IType TDelegateCreator();
        static Dictionary<string, IType> _mapCreator = new Dictionary<string, IType>();
        public static void Initialize()
        {
            Register<TBool>(); Register<TByte>(); Register<TShort>(); Register<TInt>(); Register<TLong>(); Register<TFloat>(); Register<TString>(); Register<TUShort>(); Register<TUInt>(); Register<TSByte>(); Register<TULong>(); Register<TDouble>();
            Register<TMemo>();
            Register<TList<TBool>>(); Register<TList<TByte>>(); Register<TList<TShort>>();
            Register<TList<TInt>>(); Register<TList<TLong>>(); Register<TList<TFloat>>(); Register<TList<TString>>();
            Register<TList<TUShort>>(); Register<TList<TUInt>>(); Register<TList<TULong>>(); Register<TList<TDouble>>();

            Register<TMap<TByte, TBool>>(); Register<TMap<TByte, TByte>>(); Register<TMap<TByte, TShort>>(); 
            Register<TMap<TByte, TInt>>(); Register<TMap<TByte, TLong>>(); Register<TMap<TByte, TFloat>>(); Register<TMap<TByte, TString>>();

            Register<TMap<TByte, TList<TBool>>>(); Register<TMap<TByte, TList<TByte>>>(); Register<TMap<TByte, TList<TShort>>>();
            Register<TMap<TByte, TList<TInt>>>(); Register<TMap<TByte, TList<TLong>>>(); Register<TMap<TByte, TList<TFloat>>>(); Register<TMap<TByte, TList<TString>>>();
            Register<TMap<TByte, TList<TUShort>>>(); Register<TMap<TByte, TList<TUInt>>>();

            Register<TMap<TShort, TBool>>(); Register<TMap<TShort, TByte>>(); Register<TMap<TShort, TShort>>();
            Register<TMap<TShort, TInt>>(); Register<TMap<TShort, TLong>>(); Register<TMap<TShort, TFloat>>(); Register<TMap<TShort, TString>>();
            Register<TMap<TShort, TList<TBool>>>(); Register<TMap<TShort, TList<TByte>>>(); Register<TMap<TShort, TList<TShort>>>();
            Register<TMap<TShort, TList<TInt>>>(); Register<TMap<TShort, TList<TLong>>>(); Register<TMap<TShort, TList<TFloat>>>(); Register<TMap<TShort, TList<TString>>>();
            Register<TMap<TShort, TList<TUShort>>>(); Register<TMap<TShort, TList<TUInt>>>();

            Register<TMap<TUShort, TUShort>>(); Register<TMap<TUShort, TUInt>>(); Register<TMap<TUShort, TInt>>();
            Register<TMap<TUShort, TULong>>(); Register<TMap<TUShort, TByte>>(); Register<TMap<TUShort, TFloat>>();


            Register<TMap<TInt, TBool>>(); Register<TMap<TInt, TByte>>(); Register<TMap<TInt, TShort>>();
            Register<TMap<TInt, TInt>>(); Register<TMap<TInt, TLong>>(); Register<TMap<TInt, TFloat>>(); Register<TMap<TInt, TString>>();

            Register<TMap<TInt, TList<TBool>>>(); Register<TMap<TInt, TList<TByte>>>(); Register < TMap<TInt, TList<TShort>>>();
            Register<TMap<TInt, TList<TInt>>>(); Register<TMap<TInt, TList<TLong>>>(); Register<TMap<TInt, TList<TFloat>>>(); Register < TMap<TInt, TList<TString>>>();
            Register<TMap<TInt, TList<TUShort>>>(); Register<TMap<TInt, TList<TUInt>>>();

            Register<TMap<TLong, TBool>>(); Register<TMap<TLong, TByte>>(); Register<TMap<TLong, TShort>>();
            Register<TMap<TLong, TInt>>(); Register<TMap<TLong, TLong>>(); Register<TMap<TLong, TFloat>>(); Register<TMap<TLong, TString>>();

            Register<TMap<TLong, TList<TBool>>>(); Register<TMap<TLong, TList<TByte>>>(); Register < TMap<TLong, TList<TShort>>>();
            Register<TMap<TLong, TList<TInt>>>(); Register<TMap<TLong, TList<TLong>>>(); Register<TMap<TLong, TList<TFloat>>>(); Register < TMap<TLong, TList<TString>>>();
            Register<TMap<TLong, TList<TUShort>>>(); Register<TMap<TLong, TList<TUInt>>>();

            Register<TMap<TFloat, TBool>>(); Register<TMap<TFloat, TByte>>(); Register<TMap<TFloat, TShort>>();
            Register<TMap<TFloat, TInt>>(); Register<TMap<TFloat, TLong>>(); Register<TMap<TFloat, TFloat>>(); Register<TMap<TFloat, TString>>();

            Register<TMap<TFloat, TList<TBool>>>(); Register<TMap<TFloat, TList<TByte>>>(); Register < TMap<TFloat, TList<TShort>>>();
            Register<TMap<TFloat, TList<TInt>>>(); Register<TMap<TFloat, TList<TLong>>>(); Register<TMap<TFloat, TList<TFloat>>>(); Register < TMap<TFloat, TList<TString>>>();
            Register<TMap<TFloat, TList<TUShort>>>(); Register<TMap<TFloat, TList<TUInt>>>();

            Register<TMap<TString, TBool>>(); Register<TMap<TString, TByte>>(); Register<TMap<TString, TShort>>();
            Register<TMap<TString, TInt>>(); Register<TMap<TString, TLong>>(); Register<TMap<TString, TFloat>>(); Register<TMap<TString, TString>>();

            Register<TMap<TString, TList<TBool>>>(); Register<TMap<TString, TList<TByte>>>(); Register < TMap<TString, TList<TShort>>>();
            Register<TMap<TString, TList<TInt>>>(); Register<TMap<TString, TList<TLong>>>(); Register<TMap<TString, TList<TFloat>>>(); Register < TMap<TString, TList<TString>>>();
            Register<TMap<TString, TList<TUShort>>>(); Register<TMap<TString, TList<TUInt>>>();

            Register<TMap<TULong, TBool>>(); Register<TMap<TULong, TByte>>(); Register<TMap<TULong, TShort>>();
            Register<TMap<TULong, TInt>>(); Register<TMap<TULong, TLong>>(); Register<TMap<TULong, TFloat>>(); Register<TMap<TULong, TString>>();
        }

        public static void Register<_T>() where _T : IType, new()
        {
            IType instance = new _T();
            _mapCreator.Add(instance.Name, instance);
        }
        public static IType Get(string typeName)
        {
            IType type;
            if (_mapCreator.TryGetValue(typeName, out type))
            {
                return type;
                
            }

            string tempName = typeName + "_t";
            if (_mapCreator.TryGetValue(tempName, out type))
            {
                return type;
            }

            char[] delimiterChars = { '<', '>' };

            string [] tempArray = typeName.Split(delimiterChars);
            if (tempArray.Length < 3)
            {
                return null;
            }

            tempName = tempArray[0] +  "<" + tempArray[1] + "_t>" + tempArray[2];
            if (_mapCreator.TryGetValue(tempName, out type))
            {
                return type;
            }

            return null;
        }
    }
}
