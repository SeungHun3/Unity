using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using SlotServer.Models;
using Newtonsoft.Json;

namespace Mongs.API
{
    public class ApiDataBase
    {
    }

    public class ApiContainer<T1, T2> : ApiDataBase where T1 : RequestBase, new() where T2 : ResponseBase, new()
    {
        const int DEFAULT_TIME_OUT = 10;

        protected T1 _requestData = null;
        public T1 RequestData
        {
            get
            {
                if (_requestData == null)
                {
                    _requestData = new T1();
                }
                return _requestData;
            }
        }

        protected int _timeOut;

        public ApiContainer(int timeOut = DEFAULT_TIME_OUT) : base()
        {
            _timeOut = timeOut;
        }

        void Callback(T2 item)
        {
            //if (IsError(item.Result_Code) == false)
            {
                OnReceive(item);
            }
            //else
            {

            //    OnReceiveError(item.Result_Code);
            }
        }

        // 네트워크 에러 공통
        void NetworkError(string msg)
        {
            string title = "Error";
            string message = string.Format("The current network is unstable(002).\nPlease try again later\n({0})", msg);
            Debug.Log($"::Exception ApiData>NetworkError:{msg}");
        }

        protected virtual bool IsError(int error)
        {
            return true;
        }

        protected virtual void OnReceive(T2 item) { }

        protected virtual void OnReceiveError(int resultCode)
        {
            string title = "Network Error";
            string code = string.Format("Server_Error_{0}", resultCode);
            string message = "";       
        }

        public void Request()
        {
            if (_requestData == null)
			{
				_requestData = new T1();
			}

			string url = "http://127.0.0.1:5270/Account/Login";
            MongsWebNet.Request(JsonConvert.SerializeObject(_requestData), url, _timeOut, Complete, NetworkError);
		}

		void Complete(string msg)
        {
            Callback(JsonConvert.DeserializeObject<T2>(msg));
        }
    }

    public class LoginProc : ApiContainer<ReqLogin, ResLogin>
    {
        public delegate void CompleteCallBack(ResLogin response);
        public CompleteCallBack CompleteMsg;

        public delegate void ReceiveErrorCallBack(string error, int resultCode);
        public ReceiveErrorCallBack ReceiveError;


        public LoginProc() : base()
        {
            CompleteMsg += Temp;
        }
        void Temp(ResLogin item)
		{

		}

        protected override void OnReceiveError(int resultCode)
        {
            Debug.Log("Error" + resultCode);
        }

        protected override void OnReceive(ResLogin item)
        {
            
            Debug.Log(item.User.NickName);
            //SceneLoadManager.Instance.LoadScene("1_Slot_3X5_243Ways");
        }
    }

    public class SlotResultProc : ApiContainer<SlotResultModelRequest, SlotResultModelResponse>
    {
        public delegate void CompleteCallBack(SlotResultModelResponse response);
        public CompleteCallBack CompleteMsg;

        public SlotResultProc() : base()
        {

        }

        protected override void OnReceiveError(int resultCode)
        {
        }

        protected override void OnReceive(SlotResultModelResponse item)
        {
            if(CompleteMsg == null)
            {
                return;
            }

            CompleteMsg(item);
        }
    }



}