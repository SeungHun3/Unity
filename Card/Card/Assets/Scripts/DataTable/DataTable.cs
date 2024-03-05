using System;
using System.Collections.Generic;
using System.IO;

using byte_t = System.Byte;
using sbyte_t = System.SByte;
using uint_t = System.UInt32;
using ulong_t = System.UInt64;
using ushort_t = System.UInt16;

namespace DataTable
{

public class DataTableBase
{
}

public class DataTableListBase
{
	public Dictionary<ulong,DataTableBase> DataList = new Dictionary<ulong,DataTableBase>();
	public virtual void Load(BinaryReader reader)
	{
	}

	public DataTableBase GetDataTable(ulong serialNo)
	{
		DataTableBase dataTableBase = null;
		if(DataList.TryGetValue(serialNo, out dataTableBase) == false)
		{
			return null;
		}
		return dataTableBase;
	}

	public UInt16 Version = 0;

}

public class TempDataTable_List : DataTableListBase
{
	public const string NAME = "Temp";
	public const string DATAFILENAME = "TempData.bytes";
	public override void Load(BinaryReader reader)
	{
		Version = reader.ReadUInt16();
		ushort_t data_count = reader.ReadUInt16();
		for(ushort_t i = 0; i < data_count; ++i)
		{
			ulong_t serialNo = reader.ReadUInt64();
			TempDataTable data = new TempDataTable();
			data.Load(reader);
			DataList.Add(serialNo, data);
		}
	}

}

public class TempDataTable : DataTableBase
{
	public ulong_t percentage;
	public string SkillName;
	public string IconName;
	public string Content;
	public string Content1;
	public void Load(BinaryReader reader)
	{
		percentage = reader.ReadUInt64();
		uint_t SkillNameCount = reader.ReadUInt32();
		byte[] SkillNameValue = reader.ReadBytes((int)SkillNameCount);
		SkillName = System.Text.Encoding.UTF8.GetString(SkillNameValue);
		uint_t IconNameCount = reader.ReadUInt32();
		byte[] IconNameValue = reader.ReadBytes((int)IconNameCount);
		IconName = System.Text.Encoding.UTF8.GetString(IconNameValue);
		uint_t ContentCount = reader.ReadUInt32();
		byte[] ContentValue = reader.ReadBytes((int)ContentCount);
		Content = System.Text.Encoding.UTF8.GetString(ContentValue);
		uint_t Content1Count = reader.ReadUInt32();
		byte[] Content1Value = reader.ReadBytes((int)Content1Count);
		Content1 = System.Text.Encoding.UTF8.GetString(Content1Value);
	}

}


public class DataTableLoader
{
	protected Dictionary<string, DataTableListBase> _datatableList = new Dictionary<string, DataTableListBase>();

	public DataTableLoader(string dstPath)
	{
		Load(dstPath, TempDataTable_List.DATAFILENAME, TempDataTable_List.NAME, new TempDataTable_List());
	}

	public DataTableBase GetDataTable(string dataName, ulong serialNo)
	{
		DataTableListBase dataTableListBase = null;
		if(_datatableList.TryGetValue(dataName, out dataTableListBase) == false)
		{
			return null;
		}
		return dataTableListBase.GetDataTable(serialNo);
	}

	public DataTableListBase GetDataTableList(string dataName)
	{
		DataTableListBase dataTableListBase = null;
		if(_datatableList.TryGetValue(dataName, out dataTableListBase) == false)
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

			if(_datatableList.ContainsKey(dataName))
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

