using System;
using System.Collections.Generic;
using System.IO;
using byte_t = System.Byte;
using sbyte_t = System.SByte;
using uint_t = System.UInt32;
using ulong_t = System.UInt64;
using ushort_t = System.UInt16;

namespace ChampionCard.Common
{

    public class DataTableBase
    {
    }

    public class DataTableListBase
    {
        public Dictionary<int, DataTableBase> DataList = new Dictionary<int, DataTableBase>();
        public virtual void Load(BinaryReader reader)
        {
        }

        public DataTableBase GetDataTable(int serialNo)
        {
            DataTableBase dataTableBase = null;
            if (DataList.TryGetValue(serialNo, out dataTableBase) == false)
            {
                return null;
            }
            return dataTableBase;
        }

        public ushort_t Version = 0;

    }

    public class BuyFreeSpinDataTable_List : DataTableListBase
    {
        public const string NAME = "BuyFreeSpin";
        public const string DATAFILENAME = "BuyFreeSpinData.bytes";
        public override void Load(BinaryReader reader)
        {
            Version = reader.ReadUInt16();
            ushort_t data_count = reader.ReadUInt16();
            for (ushort_t i = 0; i < data_count; ++i)
            {
                int serialNo = reader.ReadInt32();
                BuyFreeSpinDataTable data = new BuyFreeSpinDataTable();
                data.Load(reader);
                DataList.Add(serialNo, data);
            }
        }

    }

    public class BuyFreeSpinDataTable : DataTableBase
    {
        public int UseSlotMachine;
        public List<int> SymbolArray;
        public void Load(BinaryReader reader)
        {
            UseSlotMachine = reader.ReadInt32();
            SymbolArray = new List<int>();
            uint_t SymbolArrayCount = reader.ReadUInt32();
            for (uint_t i = 0; i < SymbolArrayCount; ++i)
            {
                int temp = reader.ReadInt32();
                SymbolArray.Add(temp);
            }
        }

    }

    public class CoinValueDataTable_List : DataTableListBase
    {
        public const string NAME = "CoinValue";
        public const string DATAFILENAME = "CoinValueData.bytes";
        public override void Load(BinaryReader reader)
        {
            Version = reader.ReadUInt16();
            ushort_t data_count = reader.ReadUInt16();
            for (ushort_t i = 0; i < data_count; ++i)
            {
                int serialNo = reader.ReadInt32();
                CoinValueDataTable data = new CoinValueDataTable();
                data.Load(reader);
                DataList.Add(serialNo, data);
            }
        }

    }

    public class CoinValueDataTable : DataTableBase
    {
        public List<float> CoinValue;
        public string MoneyMark;
        public void Load(BinaryReader reader)
        {
            CoinValue = new List<float>();
            uint_t CoinValueCount = reader.ReadUInt32();
            for (uint_t i = 0; i < CoinValueCount; ++i)
            {
                float temp = reader.ReadSingle();
                CoinValue.Add(temp);
            }
            uint_t MoneyMarkCount = reader.ReadUInt32();
            byte[] MoneyMarkValue = reader.ReadBytes((int)MoneyMarkCount);
            MoneyMark = System.Text.Encoding.UTF8.GetString(MoneyMarkValue);
        }

    }

    public class ConstanceDataTable_List : DataTableListBase
    {
        public const string NAME = "Constance";
        public const string DATAFILENAME = "ConstanceData.bytes";
        public override void Load(BinaryReader reader)
        {
            Version = reader.ReadUInt16();
            ushort_t data_count = reader.ReadUInt16();
            for (ushort_t i = 0; i < data_count; ++i)
            {
                int serialNo = reader.ReadInt32();
                ConstanceDataTable data = new ConstanceDataTable();
                data.Load(reader);
                DataList.Add(serialNo, data);
            }
        }

    }

    public class ConstanceDataTable : DataTableBase
    {
        public string Key;
        public int Constant;
        public void Load(BinaryReader reader)
        {
            uint_t KeyCount = reader.ReadUInt32();
            byte[] KeyValue = reader.ReadBytes((int)KeyCount);
            Key = System.Text.Encoding.UTF8.GetString(KeyValue);
            Constant = reader.ReadInt32();
        }

    }

    public class LoseCaseDataTable_List : DataTableListBase
    {
        public const string NAME = "LoseCase";
        public const string DATAFILENAME = "LoseCaseData.bytes";
        public override void Load(BinaryReader reader)
        {
            Version = reader.ReadUInt16();
            ushort_t data_count = reader.ReadUInt16();
            for (ushort_t i = 0; i < data_count; ++i)
            {
                int serialNo = reader.ReadInt32();
                LoseCaseDataTable data = new LoseCaseDataTable();
                data.Load(reader);
                DataList.Add(serialNo, data);
            }
        }

    }

    public class LoseCaseDataTable : DataTableBase
    {
        public int UseSlotMachine;
        public List<int> SymbolArray;
        public void Load(BinaryReader reader)
        {
            UseSlotMachine = reader.ReadInt32();
            SymbolArray = new List<int>();
            uint_t SymbolArrayCount = reader.ReadUInt32();
            for (uint_t i = 0; i < SymbolArrayCount; ++i)
            {
                int temp = reader.ReadInt32();
                SymbolArray.Add(temp);
            }
        }

    }

    public class PayLineDataTable_List : DataTableListBase
    {
        public const string NAME = "PayLine";
        public const string DATAFILENAME = "PayLineData.bytes";
        public override void Load(BinaryReader reader)
        {
            Version = reader.ReadUInt16();
            ushort_t data_count = reader.ReadUInt16();
            for (ushort_t i = 0; i < data_count; ++i)
            {
                int serialNo = reader.ReadInt32();
                PayLineDataTable data = new PayLineDataTable();
                data.Load(reader);
                DataList.Add(serialNo, data);
            }
        }

    }

    public class PayLineDataTable : DataTableBase
    {
        public byte_t SlotMachineType;
        public List<int> PayLineShape;
        public void Load(BinaryReader reader)
        {
            SlotMachineType = reader.ReadByte();
            PayLineShape = new List<int>();
            uint_t PayLineShapeCount = reader.ReadUInt32();
            for (uint_t i = 0; i < PayLineShapeCount; ++i)
            {
                int temp = reader.ReadInt32();
                PayLineShape.Add(temp);
            }
        }

    }

    public class ReelDataTable_List : DataTableListBase
    {
        public const string NAME = "Reel";
        public const string DATAFILENAME = "ReelData.bytes";
        public override void Load(BinaryReader reader)
        {
            Version = reader.ReadUInt16();
            ushort_t data_count = reader.ReadUInt16();
            for (ushort_t i = 0; i < data_count; ++i)
            {
                int serialNo = reader.ReadInt32();
                ReelDataTable data = new ReelDataTable();
                data.Load(reader);
                DataList.Add(serialNo, data);
            }
        }

    }

    public class ReelDataTable : DataTableBase
    {
        public List<int> SymbolSerial;
        public void Load(BinaryReader reader)
        {
            SymbolSerial = new List<int>();
            uint_t SymbolSerialCount = reader.ReadUInt32();
            for (uint_t i = 0; i < SymbolSerialCount; ++i)
            {
                int temp = reader.ReadInt32();
                SymbolSerial.Add(temp);
            }
        }

    }

    public class SlotMachineDataTable_List : DataTableListBase
    {
        public const string NAME = "SlotMachine";
        public const string DATAFILENAME = "SlotMachineData.bytes";
        public override void Load(BinaryReader reader)
        {
            Version = reader.ReadUInt16();
            ushort_t data_count = reader.ReadUInt16();
            for (ushort_t i = 0; i < data_count; ++i)
            {
                int serialNo = reader.ReadInt32();
                SlotMachineDataTable data = new SlotMachineDataTable();
                data.Load(reader);
                DataList.Add(serialNo, data);
            }
        }

    }

    public class SlotMachineDataTable : DataTableBase
    {
        public byte_t SlotMachineType;
        public byte_t MoneyType;
        public List<int> UseReel1;
        public List<int> UseReel2;
        public List<int> UseReel3;
        public List<int> UseReel4;
        public List<int> UseReel5;
        public List<byte_t> MatchQuantity;
        public int StartBet;
        public int BetLevel;
        public int MaxReward;
        public int BuyFreeSpin;
        public float FreeSpinRate;
        public void Load(BinaryReader reader)
        {
            SlotMachineType = reader.ReadByte();
            MoneyType = reader.ReadByte();
            UseReel1 = new List<int>();
            uint_t UseReel1Count = reader.ReadUInt32();
            for (uint_t i = 0; i < UseReel1Count; ++i)
            {
                int temp = reader.ReadInt32();
                UseReel1.Add(temp);
            }
            UseReel2 = new List<int>();
            uint_t UseReel2Count = reader.ReadUInt32();
            for (uint_t i = 0; i < UseReel2Count; ++i)
            {
                int temp = reader.ReadInt32();
                UseReel2.Add(temp);
            }
            UseReel3 = new List<int>();
            uint_t UseReel3Count = reader.ReadUInt32();
            for (uint_t i = 0; i < UseReel3Count; ++i)
            {
                int temp = reader.ReadInt32();
                UseReel3.Add(temp);
            }
            UseReel4 = new List<int>();
            uint_t UseReel4Count = reader.ReadUInt32();
            for (uint_t i = 0; i < UseReel4Count; ++i)
            {
                int temp = reader.ReadInt32();
                UseReel4.Add(temp);
            }
            UseReel5 = new List<int>();
            uint_t UseReel5Count = reader.ReadUInt32();
            for (uint_t i = 0; i < UseReel5Count; ++i)
            {
                int temp = reader.ReadInt32();
                UseReel5.Add(temp);
            }
            MatchQuantity = new List<byte_t>();
            uint_t MatchQuantityCount = reader.ReadUInt32();
            for (uint_t i = 0; i < MatchQuantityCount; ++i)
            {
                byte_t temp = reader.ReadByte();
                MatchQuantity.Add(temp);
            }
            StartBet = reader.ReadInt32();
            BetLevel = reader.ReadInt32();
            MaxReward = reader.ReadInt32();
            BuyFreeSpin = reader.ReadInt32();
            FreeSpinRate = reader.ReadSingle();
        }

    }

    public class SymbolDataTable_List : DataTableListBase
    {
        public const string NAME = "Symbol";
        public const string DATAFILENAME = "SymbolData.bytes";
        public override void Load(BinaryReader reader)
        {
            Version = reader.ReadUInt16();
            ushort_t data_count = reader.ReadUInt16();
            for (ushort_t i = 0; i < data_count; ++i)
            {
                int serialNo = reader.ReadInt32();
                SymbolDataTable data = new SymbolDataTable();
                data.Load(reader);
                DataList.Add(serialNo, data);
            }
        }

    }

    public class SymbolDataTable : DataTableBase
    {
        public byte_t SymbolType;
        public List<float> RewardQuantity;
        public string Icon;
        public void Load(BinaryReader reader)
        {
            SymbolType = reader.ReadByte();
            RewardQuantity = new List<float>();
            uint_t RewardQuantityCount = reader.ReadUInt32();
            for (uint_t i = 0; i < RewardQuantityCount; ++i)
            {
                float temp = reader.ReadSingle();
                RewardQuantity.Add(temp);
            }
            uint_t IconCount = reader.ReadUInt32();
            byte[] IconValue = reader.ReadBytes((int)IconCount);
            Icon = System.Text.Encoding.UTF8.GetString(IconValue);
        }

    }


    public class DataTableLoader
    {
        protected Dictionary<string, DataTableListBase> _datatableList = new Dictionary<string, DataTableListBase>();

        public DataTableLoader(string dstPath)
        {
            Load(dstPath, BuyFreeSpinDataTable_List.DATAFILENAME, BuyFreeSpinDataTable_List.NAME, new BuyFreeSpinDataTable_List());
            Load(dstPath, CoinValueDataTable_List.DATAFILENAME, CoinValueDataTable_List.NAME, new CoinValueDataTable_List());
            Load(dstPath, ConstanceDataTable_List.DATAFILENAME, ConstanceDataTable_List.NAME, new ConstanceDataTable_List());
            Load(dstPath, LoseCaseDataTable_List.DATAFILENAME, LoseCaseDataTable_List.NAME, new LoseCaseDataTable_List());
            Load(dstPath, PayLineDataTable_List.DATAFILENAME, PayLineDataTable_List.NAME, new PayLineDataTable_List());
            Load(dstPath, ReelDataTable_List.DATAFILENAME, ReelDataTable_List.NAME, new ReelDataTable_List());
            Load(dstPath, SlotMachineDataTable_List.DATAFILENAME, SlotMachineDataTable_List.NAME, new SlotMachineDataTable_List());
            Load(dstPath, SymbolDataTable_List.DATAFILENAME, SymbolDataTable_List.NAME, new SymbolDataTable_List());
        }

        public DataTableBase GetDataTable(string dataName, int serialNo)
        {
            DataTableListBase dataTableListBase = null;
            if (_datatableList.TryGetValue(dataName, out dataTableListBase) == false)
            {
                return null;
            }
            return dataTableListBase.GetDataTable(serialNo);
        }

        public DataTableListBase GetDataTableList(string dataName)
        {
            DataTableListBase dataTableListBase = null;
            if (_datatableList.TryGetValue(dataName, out dataTableListBase) == false)
            {
                return null;
            }
            return dataTableListBase;
        }

        public void Load(string dstPath, string fileName, string dataName, DataTableListBase datatableList)
        {
            try
            {
                string dataPath = string.Format("{0}/{1}", dstPath, fileName);
                if (!File.Exists(dataPath))
                {
                    return;
                }

                using (BinaryReader reader = new BinaryReader(File.Open(dataPath, FileMode.Open)))
                {
                    datatableList.Load(reader);
                }

                if (_datatableList.ContainsKey(dataName))
                {
                    _datatableList.Remove(dataName);
                }

                _datatableList.Add(dataName, datatableList);
            }
            catch
            {
            }
        }
    }

}

