using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//모든 LINQ 쿼리식은 반드시 from 절로 시작한다.
//from의 데이터 원본은 아무 형식이나 사용할 수 없고, IEnumerable<T> 인터페이스를 상속하는 형식이어야 한다.
//배열, 컬렉션 객체들은 IEnumerable<T>를 상속하기 때문에 from 절의 데이터 원본으로 사용할 수 있다.
//from <범위 변수> in <데이터 원본>의 형식으로 사용

//LINQ의 범위 변수와 foreach 문의 반복 변수의 차이점

//foreach 문의 반복 변수는 데이터 원본으로부터 데이터를 담아내지만, 범위 변수는 실제로 데이터를 담지는 않는다.
//쿼리식 외부에서 선언된 변수에 범위 변수의 데이터를 넣는 일은 할 수 없다.

// => 최종적으로 둘다 복사데이터를 return 한다

public struct Student
{
    public string First;
    public string Last;
    public int ID;
    public List<int> Scores;
}

public class LINQ : MonoBehaviour
{
    public static List<Student> students = new List<Student>
    {
        new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 92, 81, 60}},
        new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
        new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {88, 94, 65, 91}},
        new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {97, 89, 85, 82}},
        new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {35, 72, 91, 70}},
        new Student {First="Fadi", Last="Fakhouri", ID=116, Scores= new List<int> {99, 86, 90, 94}},
        new Student {First="Hanying", Last="Feng", ID=117, Scores= new List<int> {93, 92, 80, 87}},
        new Student {First="Hugo", Last="Garcia", ID=118, Scores= new List<int> {92, 90, 83, 78}},
        new Student {First="Lance", Last="Tucker", ID=119, Scores= new List<int> {68, 79, 88, 92}},
        new Student {First="Terry", Last="Adams", ID=120, Scores= new List<int> {99, 82, 81, 79}},
        new Student {First="Eugene", Last="Zabokritski", ID=121, Scores= new List<int> {96, 85, 91, 60}},
        new Student {First="Michael", Last="Tucker", ID=122, Scores= new List<int> {94, 92, 91, 91}}
    };

    void CustomRange_NotLinq()
    {
        List<Student> studentList = new List<Student>();

        foreach (var list in students)
        {
            if (list.ID >= 111 && list.ID < 114)
            {
                studentList.Add(list);
            }
        }
        foreach (var list in studentList)
        {
            Debug.Log($"{list.First}'s ID : {list.ID}");
        }

    }
    void CustomRange_UseLinq()
    {
        var studentList = from student in students
                          where student.ID >= 111
                          where student.ID < 114
                          select student;

        foreach (var list in studentList)
        {
            Debug.Log($"{list.First}'s ID : {list.ID}");
        }
    }

    private void Start()
    {
        CustomRange_NotLinq();
        Debug.Log("==============");
        CustomRange_UseLinq();
    }


}
