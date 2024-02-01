using UnityEngine;

/*
시간을 좀 더 효율적으로 활용하기 위해 제작해본 예제들을 기능 단위로 유틸리티 함수에 쌓아두어야 할 것이다. 
유틸리티 함수는 ***일반화된 작업***들을 수행하는 함수들로, 모두 static함수로 구성되어 있고 클래스 또한 static class로 정의한다. 
이렇게 하면 인스턴싱으로 인한 부분은 아예 신경쓸 일이 없을 것이다.
*/

public static class Utils
{
    public static void SetTimeScale(float timescale)
    {
        Time.timeScale = timescale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public static int GenerateID<T>()
    {
        return GenerateID(typeof(T));
    }
    public static int GenerateID(System.Type type)
    {
        return Animator.StringToHash(type.Name);
    }

    public static float DirectionToAngle(float x, float y)
    {
        float cos = x;
        float sin = y;
        return Mathf.Atan2(sin, cos) * Mathf.Rad2Deg;
    }
    public static void MyStringLog(string _string)
    {
        Debug.Log(_string);
    }



}