using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : Singleton<GameInstance>
{
	private GameObject _logGUI;
	private GameObject _debugStatGUI;

	private GameObject _gamePrefabs;

	private GameObject _httpManager;
    
    public GameObject GetUIObject(){ return null/*_logGUI*/;}
    // ..
}

/*
싱글턴 객체 소멸 타이밍
OnApplicationQuit 이라는 이벤트가 있는데, 딱 봤을 때 앱이 종료될 때 호출될 것 같지만 
공식 문서에서는 항상 호출됨을 보장하지 않는다고 한다. 

다행히도 대체할 이벤트가 있다. OnApplicationPause을 이용하면 된다. 
OnApplicationPause (bool) 이벤트의 경우 모바일에서 창이 전환될 때 
바로 호출되기 때문에 종료되기 전에 반드시 호출됨을 보장
*/

public abstract class Singleton<T> : MonoBehaviour where T : Component // T는 Component클래스에 국한시키겠다 
// MonoBehaviour를 상속받는 싱글톤 템플릿이니 상관없지만 명확하게 적어줌
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();
				if (instance == null)
				{
					GameObject obj = new GameObject();
					obj.name = typeof(T).Name;
					instance = obj.AddComponent<T>();
				}
			}
			return instance;
		}
	}

	protected virtual void Awake()
	{
		if (instance == null)
		{
			instance = this as T;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}