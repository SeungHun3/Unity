using UnityEngine;


// field, property

// field: 클래스 멤버 변수로써 클래스 내부에 글로벌하게 선언된 변수라고 정의
public class MyClass
{
    public int count = 10;   // field
}
//만약 해당 필드에 다이렉트로 접근한다고 하면  아주 위험 부담이 많이 가는 코드
public class MyClass1
{
    private int count = 10;   // field

    public int GetCount() { return count; }         //Get method
    public void SetCount(int n) { this.count = n; } //Set method
}
//메소드들이 무수히 존재시 관리의 어려움


// property: 이 문제점을 해결하기 위해 마이크로소프트사가 제안한 개념

// get, set 접근자를 통한 직관화
class Person
{
    private string age; // field

    public string Age   // property
    {
        get { return age; } // field 값 읽기전용
        set { age = value; } // field 값 쓰기전용
    }
    public string CustomAge // property
    {
        get
        {
            if (age == "29") { return "29세 입니다"; }
            else
            {
                return age;
            }
        }
        set { age = value; }
    }
}

//value :  컴파일러 자체적으로 value라는 넘을 자동으로 생성해 주어 전달되어진 값

/* 컴파일 코드 = Code size 단계에서 생성

.method public hidebysig specialname instance void         
set_age (string 'value') cil managed{               // Code size       
8 (0x8)  .maxstack  8  IL_0000:  
ldarg.0  IL_0001:  
ldarg.1  IL_0002: 
stfld      string Address::age  IL_0007:  ret}      // end of method Address::set_age
*/


class Program
{

    Person personObj = new Person();

    void GetSet()
    {

        Debug.Log("personObj.Age 값 설정 전 : " + personObj.Age);

        personObj.Age = "29";
        Debug.Log("personObj.Age 값 설정 후 : " + personObj.Age);
    }
}