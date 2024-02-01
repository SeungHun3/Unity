using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Loading : MonoBehaviour
{
   private void Start() {
        //LoadNextScene();
   }
   public void RemoveScene()
   {
    Debug.Log("Click");
   }

   public void LoadNextScene(){
         StartCoroutine(LoadMyAsyncScene());
   }

    IEnumerator LoadMyAsyncScene()
    {    

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");

        while (!asyncLoad.isDone)
        {
            float progress = asyncLoad.progress;
             Debug.Log("asyncLoad Progress: " + progress);
            yield return null;
        }
    }




    IEnumerator UnloadScene()
    {
        yield return SceneManager.UnloadSceneAsync("test");
        Debug.Log("Scene unloaded successfully!");
    }

}
