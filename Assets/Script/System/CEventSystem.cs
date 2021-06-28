using System.Collections.Generic;
using UnityEngine;
using Public;
using System;

public enum EEventType
{
    HPChange,
    PowerChange,
    SpeedChange,
    Shoot,
    SceneLoad,
}

public class CEventSystem : CSingleton<CEventSystem>
{
    private Dictionary<EEventType, Type> m_EventDict = new Dictionary<EEventType, Type>()
    {
        {EEventType.HPChange,typeof(Action<int,int>)},      //血量，血量上限
        {EEventType.PowerChange,typeof(Action<int>)},       //火力
        {EEventType.SpeedChange,typeof(Action<int>)},       //最大速度
        {EEventType.Shoot,typeof(Action)},

        {EEventType.SceneLoad,typeof(Action<int>)},         //要加载的场景号
    };
    private Dictionary<EEventType, Delegate> m_Event = new Dictionary<EEventType, Delegate>();

    protected override void Awake()
    {
        base.Awake();
        foreach (EEventType key in m_EventDict.Keys)
        {
            m_Event.Add(key, null);
        }
    }

    private bool TypeCheck(EEventType eventType, Type methodType)
    {
        if (m_EventDict[eventType] != methodType)
        {
            Debug.Log("响应方法的类型不符合事件所要求的类型");
            return false;
        }
        return true;
    }

    public void AddLisenter(EEventType eventType, Action listenerMethod)
    {
        if (TypeCheck(eventType, listenerMethod.GetType()))
            m_Event[eventType] = (Action)m_Event[eventType] + listenerMethod;
    }
    public void AddLisenter<T1>(EEventType eventType, Action<T1> listenerMethod)
    {
        if (TypeCheck(eventType, listenerMethod.GetType()))
            m_Event[eventType] = (Action<T1>)m_Event[eventType] + listenerMethod;
    }
    public void AddLisenter<T1, T2>(EEventType eventType, Action<T1, T2> listenerMethod)
    {
        if (TypeCheck(eventType, listenerMethod.GetType()))
            m_Event[eventType] = (Action<T1, T2>)m_Event[eventType] + listenerMethod;
    }

    public void RemoveListener(EEventType eventType, Action listenerMethod)
    {
        if (TypeCheck(eventType, listenerMethod.GetType()))
            m_Event[eventType] = (Action)m_Event[eventType] - listenerMethod;
    }
    public void RemoveListener<T1>(EEventType eventType, Action<T1> listenerMethod)
    {
        if (TypeCheck(eventType, listenerMethod.GetType()))
            m_Event[eventType] = (Action<T1>)m_Event[eventType] - listenerMethod;
    }
    public void RemoveListener<T1, T2>(EEventType eventType, Action<T1, T2> listenerMethod)
    {
        if (TypeCheck(eventType, listenerMethod.GetType()))
            m_Event[eventType] = (Action<T1, T2>)m_Event[eventType] - listenerMethod;
    }

    public void ActivateEvent(EEventType eventType)
    {
        if (TypeCheck(eventType, typeof(Action)))
            (m_Event[eventType] as Action)?.Invoke();
    }
    public void ActivateEvent<T1>(EEventType eventType, T1 arg1)
    {
        if (TypeCheck(eventType, typeof(Action<T1>)))
            (m_Event[eventType] as Action<T1>)?.Invoke(arg1);
    }
    public void ActivateEvent<T1, T2>(EEventType eventType, T1 arg1, T2 arg2)
    {
        if (TypeCheck(eventType, typeof(Action<T1, T2>)))
            (m_Event[eventType] as Action<T1, T2>)?.Invoke(arg1, arg2);
    }
}
