using UnityEngine;
using System.Collections.Generic;

namespace Mongs
{
    public class MongsWebNetManager : SingleDontDestroy<MongsWebNetManager>
    {
        public enum API_TYPE
        {
            LoginProc = 1,
            SlotResultProc = 2,
        }

        Dictionary<API_TYPE, API.ApiDataBase> _apiDic = new Dictionary<API_TYPE, API.ApiDataBase>();

        protected override void Awake()
        {
            base.Awake();
            API_TYPE [] enums = (API_TYPE[])System.Enum.GetValues(typeof(API_TYPE));
            for(int i = 0; i < enums.Length; ++i)
            {
                Add(enums[i]);
            }
        }

        void Add(API_TYPE apiType)
        {
            string name = System.Enum.GetName(typeof(API_TYPE), apiType);
            name = string.Format("Mongs.API.{0}", name);
            Debug.Log(name);
            System.Type t = System.Type.GetType(name);
            API.ApiDataBase data = (API.ApiDataBase)System.Activator.CreateInstance(t);
            _apiDic.Add(apiType, data);
        }

        public API.ApiDataBase GetApi(API_TYPE apiType)
        {
            API.ApiDataBase data = null;
            if(_apiDic.TryGetValue(apiType, out data) == false)
            {
                return null;
            }

            return data;
        }
    }
}