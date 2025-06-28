using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单例模式基类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseManager<T> where T:class//,new()
{
    private static T instance;
    /*protected bool InstanceIsNull
    {
        get { return instance == null; }
    }*/
    //用于加锁的对象
    protected static readonly object Lockobj = new object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (Lockobj)
                {
                    if (instance == null)
                    {
                        //instance = new T();
                        //利用反射获得构造函数来实例化
                        Type type = typeof(T);
                        ConstructorInfo info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                                    null,
                                                                    Type.EmptyTypes,
                                                                    null);
                        if (info != null)
                            instance = info.Invoke(null) as T;
                        else
                            Debug.Log("没有无参构造函数");

                        //instance = Activator.CreateInstance(typeof(T), true) as T;

                    }
                }
            
                
            }
            return instance;
        }
    }
}
