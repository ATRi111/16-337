using System.Collections.Generic;
using UnityEngine;
using Public;
using System;

public enum EEvent
{
    HPChange,
    PowerChange,
    SpeedChange,
    Shoot,
    SceneLoad,
}

public class CEventSystem : CSingleton<CEventSystem>
{
    private Dictionary<EEvent, Type> m_EventDict = new Dictionary<EEvent, Type>()
    {
        {EEvent.HPChange,typeof(Action<int,int>)},      //Ѫ����Ѫ������
        {EEvent.PowerChange,typeof(Action<int>)},       //����
        {EEvent.SpeedChange,typeof(Action<int>)},       //����ٶ�
        {EEvent.Shoot,typeof(Action)},

        {EEvent.SceneLoad,typeof(Action<int>)},         //Ҫ���صĳ�����
    };
    private Dictionary<EEvent, Delegate> m_Event = new Dictionary<EEvent, Delegate>();

    protected override void Awake()
    {
        base.Awake();
        foreach (EEvent key in m_EventDict.Keys)
        {
            m_Event.Add(key, null);
        }
    }

    private bool TypeCheck(EEvent eventType, Type methodType)
    {
        if (m_EventDict[eventType] != methodType)
        {
            Debug.Log("��Ӧ���������Ͳ������¼���Ҫ�������");
            return false;
        }
        return true;
    }

    public void AddLisenter(EEvent eventType, Action listenerMethod)
    {
        if (TypeCheck(eventType, listenerMethod.GetType()))
            m_Event[eventType] = (Action)m_Event[eventType] + listenerMethod;
    }
    public void AddLisenter<T1>(EEvent eventType, Action<T1> listenerMethod)
    {
        if (TypeCheck(eventType, listenerMethod.GetType()))
            m_Event[eventType] = (Action<T1>)m_Event[eventType] + listenerMethod;
    }
    public void AddLisenter<T1, T2>(EEvent eventType, Action<T1, T2> listenerMethod)
    {
        if (TypeCheck(eventType, listenerMethod.GetType()))
            m_Event[eventType] = (Action<T1, T2>)m_Event[eventType] + listenerMethod;
    }

    public void RemoveListener(EEvent eventType, Action listenerMethod)
    {
        if (TypeCheck(eventType, listenerMethod.GetType()))
            m_Event[eventType] = (Action)m_Event[eventType] - listenerMethod;
    }
    public void RemoveListener<T1>(EEvent eventType, Action<T1> listenerMethod)
    {
        if (TypeCheck(eventType, listenerMethod.GetType()))
            m_Event[eventType] = (Action<T1>)m_Event[eventType] - listenerMethod;
    }
    public void RemoveListener<T1, T2>(EEvent eventType, Action<T1, T2> listenerMethod)
    {
        if (TypeCheck(eventType, listenerMethod.GetType()))
            m_Event[eventType] = (Action<T1, T2>)m_Event[eventType] - listenerMethod;
    }

    public void ActivateEvent(EEvent eventType)
    {
        if (TypeCheck(eventType, typeof(Action)))
            (m_Event[eventType] as Action)?.Invoke();
    }
    public void ActivateEvent<T1>(EEvent eventType, T1 arg1)
    {
        if (TypeCheck(eventType, typeof(Action<T1>)))
            (m_Event[eventType] as Action<T1>)?.Invoke(arg1);
    }
    public void ActivateEvent<T1, T2>(EEvent eventType, T1 arg1, T2 arg2)
    {
        if (TypeCheck(eventType, typeof(Action<T1, T2>)))
            (m_Event[eventType] as Action<T1, T2>)?.Invoke(arg1, arg2);
    }
}
