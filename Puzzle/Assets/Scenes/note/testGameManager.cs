using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGameManager : MonoBehaviour
{

    void Start()
    {
        Utils.MyStringLog(Globals.LayerName.Default);
        //GameInstance.Instance.GetUIObject();
        Debug.Log("=====================================");
        Delegate tt = new Delegate();
        tt.test("Delegate Test");
        Debug.Log("=====================================");
        OtherClass other = new OtherClass();
        other.targetDelegate();
    }




}


/*
모든 객체들을 Child로 갖는 하나의 관리자 클래스를 정의한다. 
만약 관리자 클래스가 여러 개가 정의되어야 한다면 꼭대기의 가장 핵심적인 관리자 클래스를 두고 그 Child로 파생 관리자 객체들을 두면 된다. 
아래 예제는 가장 꼭대기에 GameManager 클래스가 하위 관리자로 
CharacterManager, UIManager, CameraController, MapManager, EffectManager를 갖는 형태로 설계되어 있다. 
이렇게 설계를 해 두고 GameManager에 대한 역참조만 정의해둔다면, 모든 클래스가 다른 모든 클래스에 접근할 수 있게 된다. 
이 클래스는 Scene의 가장 루트 GameObject로 단 하나만 미리 인스턴싱 해두도록 한다.

public class GameManager : MonoBehaviour, IGameManager
{
	[SerializeField]
	private CharacterManager _characterManager;

	[SerializeField]
	private UIManager _uiManager;

	[SerializeField]
	private CameraController _cameraController;

	[SerializeField]
	private MapManager _mapManager;

	[SerializeField]
	private EffectManager _effectManager;
    
    // ..
}

GameManager의 하위 관리자는 GameManager로의 역참조를 갖도록 한다.

public class CharacterManager : MonoBehaviour
{
	private IGameManager _gameManager;
	private List<BaseCharacter> _guardians = new List<BaseCharacter>();
    
	public IGameManager GameManager
	{
		get { return _gameManager; }
	}
	
	public void Init(IGameManager gameManager)
	{
		_gameManager = gameManager;
	}
	// ..
}

CharacterManager의 하위 객체는 CharacterManager로의 역참조를 갖는다. 이렇게 되면 모든 객체는 다른 모든 객체를 참조해올 수 있다.

public class BaseCharacter : MonoBehaviour
{
	protected CharacterManager _manager;
	public void Init(CharacterManager manager)
	{
		_manager = manager;
	}
}

*/

