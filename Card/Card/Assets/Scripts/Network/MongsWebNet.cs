using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using LitJson;
using Newtonsoft.Json;

namespace Mongs.API
{
    public class CustomCertificateHandler : CertificateHandler
    {
        // Encoded RSAPublicKey
        private static readonly string PUB_KEY = "";


        /// <summary>
        /// Validate the Certificate Against the Amazon public Cert
        /// </summary>
        /// <param name="certificateData">Certifcate to validate</param>
        /// <returns></returns>
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }

    public class MongsWebNet : SingleDontDestroy<MongsWebNet>
    {
        const int TIME_OUT = 10; 
 
        public class WebNetDataBase
        {
            public string JsonData;
            public string Path;
            public Action<UnityWebRequest> CompleteCallback;
            public Action<string> ErrorCallback;
            public int TimeOut;
        }

        string ReceiveDataPostProcess(string text_)
        {
            return text_.Trim();
        }

        public static void Request(string jsonData, string path, int timeOut, System.Action<string> callback, System.Action<string> errorCallback = null)
        {
            Instance.AddRequest(new WebNetDataBase()
            {
                JsonData = jsonData,
                Path = path,
                TimeOut = timeOut,

                CompleteCallback = delegate (UnityWebRequest www)
                {
                    try
                    {
                        if (null != callback)
                        {
                            //JsonData jsonData = JsonMapper.ToObject(instance.ReceiveDataPostProcess(www.downloadHandler.text));
                            // 단일 객체.
                            //callback(jsonData.ToJson());
                            callback(www.downloadHandler.text);
                        }
                    }
                    catch (Exception e) // 타임아웃, 데이터가 클래스에 맞게 담기지 않음,
					{
						string title = "Error";
						string msg = string.Format("The current network is unstable(003).\nPlease try again later\n({0})", e.Message);
                        Debug.Log(e.Message);
					}
				},
				ErrorCallback = delegate (string text_)
				{
                    if (null != errorCallback)
                    {
                        errorCallback(text_);
                    }
                }
            });
        }

        void AddRequest(WebNetDataBase dataBase)
        {
            StartCoroutine(WWWProcess(dataBase));
        }

        IEnumerator WWWProcess(WebNetDataBase dataBase)
        {
            UnityWebRequest www = null;

            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(dataBase.JsonData);

            UnityWebRequest.ClearCookieCache();
            www = new UnityWebRequest(dataBase.Path, UnityWebRequest.kHttpVerbPOST);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.uploadHandler.contentType = "application/x-www-form-urlencoded; charset=utf-8";
            www.downloadHandler = new DownloadHandlerBuffer();

            www.SetRequestHeader("Platform", Application.platform.ToString());
            www.SetRequestHeader("DeviceID", SystemInfo.deviceUniqueIdentifier);
            www.SetRequestHeader("ClientVerInfo", Application.version);
            www.SetRequestHeader("Content-Type", "application/json");

            www.timeout = dataBase.TimeOut;

            CustomCertificateHandler certHandler = new CustomCertificateHandler();
            www.certificateHandler = certHandler;

            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                yield return null;
            }

            if (null != www.error)
            {
                if (null != dataBase.ErrorCallback)
                {                  
                    dataBase.ErrorCallback(www.error);   
                }
            }
            else
            {
                if (null != dataBase.CompleteCallback)
                {
                    dataBase.CompleteCallback(www);
                }
            }

            www.Dispose();           
        }
    }
}
