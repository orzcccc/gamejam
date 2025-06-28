using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 挂载式继承mono的 单例模式基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get { return instance; }
    }
    protected virtual void Awake()
    {
        if (instance == null)
            instance = this as T;
    }
}
