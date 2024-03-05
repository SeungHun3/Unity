using System.Collections;
using System.Collections.Generic;
using EnumTypes;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneController : SingleDontDestroy<SceneController>
{
    SceneType _curScene = SceneType.None;
    public SceneType CurScene => _curScene;


    public void SelectScene(SceneType scene)
    {
        switch (scene)
        {
            case SceneType.None:
                break;

            case SceneType.Splash:
                _curScene = SceneType.Splash;
                break;
            case SceneType.InGame:
                _curScene = SceneType.InGame;
                SceneManager.LoadSceneAsync("InGame", LoadSceneMode.Additive);
                //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

                break;
        }
    }
}
