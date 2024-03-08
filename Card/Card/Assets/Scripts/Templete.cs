using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SingleDestroy<T> : MonoBehaviour where T : Component // T는 Component클래스에 국한시키겠다 
                                                                           // MonoBehaviour를 상속받는 싱글톤 템플릿이니 상관없지만 명확하게 적어줌
{
    protected static T instance;

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
        }
    }
}

public abstract class SingleDontDestroy<T> : SingleDestroy<T> where T : Component
{
    protected override void Awake()
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

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    public GameObject Prefab;
    protected List<T> _pool = new();
    protected int _cnt;

    protected virtual void Awake()
    {
        _cnt = 5;
    }

    protected virtual void Start()
    {
        for (int i = 0; i < _cnt; i++)
        {
            _pool.Add(Instantiate(Prefab, transform).GetComponent<T>());
            _pool[i].gameObject.SetActive(false);
        }
    }

    T GetObject()
    {
        foreach (T obj in _pool)
        {
            if (!obj.gameObject.activeSelf)
            {
                return obj;
            }
        }
        _pool.Add(Instantiate(Prefab, transform).GetComponent<T>());
        return _pool[^1];
    }

    protected virtual T Spawn()
    {
        T obj = GetObject();
        obj.gameObject.SetActive(true);

        return obj;
    }

}

public abstract class AnimContoller : MonoBehaviour
{
    public AnimTarget AnimTarget;
    private bool _bIsAnimEnd;
    protected string _animName;
    public virtual IEnumerator Use()
    {
        yield break;
    }
    public virtual IEnumerator Open()
    {
        gameObject.SetActive(true);
        yield return StartAnim();
    }

    IEnumerator StartAnim()
    {
        AnimTarget.StartAnim(this, _animName);
        while (!_bIsAnimEnd)
        {
            yield return null;
        }
        _bIsAnimEnd = false;
    }
    public virtual void EndAnim()
    {
        _bIsAnimEnd = true;
    }
}
public abstract class AnimTarget : MonoBehaviour
{
    AnimContoller _target;
    public virtual void StartAnim(AnimContoller target, string animName)
    {
        _target = target;
        GetComponent<Animation>().Play(animName);
    }
    public virtual void EndAnim()
    {
        _target.EndAnim();
    }
}