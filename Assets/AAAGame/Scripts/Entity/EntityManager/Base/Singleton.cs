using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单例
/// </summary>
public  class Singleton<T>
{
    private static readonly T instance = Activator.CreateInstance<T>();

    public static T Instance => instance;

    //初始化
    //我该把初始化弄在Level或者Produce上面
    public virtual void OnInit()
    {

    }

    //每帧执行

    public virtual void OnUpdate(float dt)
    {

    }
    
    //释放
    public virtual void OnDestroy()
    {

    }
}