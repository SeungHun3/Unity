using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Dictionary : MonoBehaviour
{
    void CustomDirtionary()
    {
        var myTable = new Dictionary<string, int>();
        myTable.Add("Hokkaido", 10);
        myTable.Add("Iwate", 2);
        myTable.Add("Miyagi", 5);
        foreach (KeyValuePair<string, int> item in myTable)
        {
            Debug.Log(string.Format("[{0}:{1}]", item.Key, item.Value));
        }
    }
    private void Start() {
        CustomDirtionary();
    }
}
