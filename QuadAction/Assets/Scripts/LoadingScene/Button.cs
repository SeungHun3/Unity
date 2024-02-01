using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Button : MonoBehaviour
{
    bool isClicked = false;
   public void Click()
   {
    Debug.Log("Click");
    isClicked = true;
    SceneManager.UnloadSceneAsync("SampleScene");
    //StartCoroutine(UnloadScene());
    //SceneManager.LoadScene("Loading");
   }

   private void Update() {
    if(isClicked)
    Debug.Log("check update");
   }
    IEnumerator UnloadScene()
    {
        yield return SceneManager.UnloadSceneAsync("test");
        isClicked = false;
        Debug.Log("Scene unloaded successfully!");
    }

}
