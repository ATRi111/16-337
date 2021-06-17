using System.Collections.Generic;
using UnityEngine;
using Public;
using System;

public enum EEventType
{
    PlayerChange,
    PowerChange,
    SpellCardChange,
    SlowModeOn,
    ShootStart,
    SceneLoaded,
}

public class CEventSystem : CSigleton<CEventSystem>
{
    private readonly Dictionary<EEventType, Type> m_EventDict = new Dictionary<EEventType, Type>()
    {
        {EEventType.PlayerChange,typeof(Action<int>)},
        {EEventType.PowerChange,typeof(Action<int>) },
        {EEventType.SpellCardChange,typeof(Action<int>)},
        {EEventType.SlowModeOn,typeof(Action<bool>)},
        {EEventType.ShootStart,typeof(Action)},

        {EEventType.SceneLoaded,typeof(Action<int>)},
    };
    private readonly Dictionary<EEventType, Delegate> m_Event = new Dictionary<EEventType, Delegate>();

    protected override void Awake()
    {
        foreach (EEventType key in m_EventDict.Keys)
        {
            m_Event.Add(key, null);
        }
        base.Awake();
    }

    private bool TypeCheck(EEventType eventType, Type methodType)
    {
        if (m_EventDict[eventType] != methodType)
        {
            Debug.Log("��Ӧ�������¼����Ͳ�ƥ��");
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