using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// C#은 다중상속을 이용할 수 없지만 여러개의 Interface를 붙일 수 있다

//인터페이스에서 정의한 멤버 변수들은 상수로 간주되므로 클래스에서 구현할 때 
//값을 변경할 수 없다
public interface IStatus
{
    public string Name { get; }
    string Age { get; }
    public void LogStatus();

}

public abstract class ABaseClass : MonoBehaviour
{
    public int Gold;
    public virtual void LogMyGold()
    {
        Debug.Log("Parent Log : " + Gold);
    }
    //MonoBehaviour = 생성자 없이 호출됨
    // 생성자의 값은 유니티 상에서 값이 사라짐 => 초기화 함수 따로 구분지어 Awake에 사용해야함
    public ABaseClass()
    {
        Gold = 3; 
        Debug.Log("Parent Create : " + Gold);
    }
}


public class Interface : ABaseClass, IStatus
{
    #region Interface
    public string Name { get; } = "Player";
    public string Age { get; } = "14";

    public void LogStatus()
    {
        Debug.Log("Interface Funtion");
    }
    #endregion


    #region Parent
    public Interface()
     : base()
    {
        Gold = 50;
        Debug.Log("Child Create : " + Gold);
    }
    public override void LogMyGold()
    {
        base.LogMyGold();
        Gold = 1;
        Debug.Log("Child Log : " + Gold);
    }
    #endregion

    void Start()
    {
        Debug.Log("Name : " + Name + ", Age : " + Age);
        LogStatus();

        LogMyGold();
    }


}
/* MonoBehaviour 실행순서

Awake: 이 함수는 항상 Start 함수 전에 호출되며 프리팹이 인스턴스화 된 직후에 호출됩니다. 
게임 오브젝트가 시작하는 동안 비활성 상태인 경우 Awake 함수는 활성화될 때까지 호출되지 않습니다.
OnEnable: (오브젝트가 활성화된 경우에만): 오브젝트 활성화 직후 이 함수를 호출합니다. 
레벨이 로드되거나 스크립트 컴포넌트를 포함한 게임 오브젝트가 인스턴스화될 때와 같이 MonoBehaviour를 생성할 때 이렇게 할 수 있습니다.
씬에 추가된 모든 오브젝트에 대해 Start, Update 등 이전에 호출된 모든 스크립트를 위한 Awake 및 OnEnable 함수가 호출됩니다. 
따라서 게임플레이 도중 오브젝트를 인스턴스화될 때는 실행되지 않습니다.

*/