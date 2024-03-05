using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : GameManager
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //StartCoroutine(Load());

        SceneController.Instance.SelectScene(EnumTypes.SceneType.InGame);
    }


    protected IEnumerator Load()
    {
        // 씬 로드, 아틀라스 로드
        yield return null;//AtlasManager.Instance.LoadAtlas();
        //yield return SceneManager.LoadSceneAsync("InGame", LoadSceneMode.Additive);
        //yield return new WaitForSeconds(1f);
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

    }

}
