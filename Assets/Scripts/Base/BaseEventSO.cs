using UnityEngine;

public abstract class BaseEventSO<T> : ScriptableObject
{
    public event System.Action<T> EventRaised;

    public virtual void Raise(T value)
    {
        EventRaised?.Invoke(value);
    }
}

public abstract class BaseEventSO : ScriptableObject
{
    public event System.Action EventRaised;

    public virtual void Raise()
    {
        EventRaised?.Invoke();
    }
}