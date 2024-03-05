using ChampionCard.Common;
using System.IO;

namespace Server
{
    public class DataTableManager
    {
        protected readonly DataTableLoader _dataTableLoader;

        public DataTableManager()
        {
            var path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Bytes");
            _dataTableLoader = new DataTableLoader(path);
        }

        public DataTableBase GetDataTable(string name, int serialNo)
        {
            return _dataTableLoader.GetDataTable(name, serialNo);
        }

        public DataTableListBase GetDataTableList(string name)
        {
            return _dataTableLoader.GetDataTableList(name);
        }
    }
}
