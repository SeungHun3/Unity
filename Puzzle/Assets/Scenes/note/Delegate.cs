using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public delegate void CheckDelegate(string str);

public class Delegate //: MonoBehaviour
{
    public CheckDelegate test;

    public Delegate()
    {
        test = new CheckDelegate(this.MyLog);
        // == test = MyLog;
        test += MyLog1;
        test += MyLog2;
    }

    private void Awake()
    {
        //test = new CheckDelegate(this.MyLog);
        //// == test = MyLog;
        //test += MyLog1;
        //test += MyLog2;

    }
    private void Start()
    {
        //test("temp");
    }

    void MyLog(string str) { Debug.Log("aaa"); }
    void MyLog1(string str) { Debug.Log("bbb"); }
    void MyLog2(string str) { Debug.Log("ccc"); }
}

public class OtherClass
{
    public Delegate targetClass = new Delegate();

    public void targetDelegate()
    {
        targetClass.test += OtherLog;
        targetClass.test("other");
    }

    void OtherLog(string str)
    {
        Debug.Log(str);
    }
}


