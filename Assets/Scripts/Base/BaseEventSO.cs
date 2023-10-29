using UnityEngine;

public abstract class BaseEventSO<T> : ScriptableObject
{
    public event System.Action<T> EventRaised;

    public virtual void Raise(T value)
    {
        EventRaised?.Invoke(value);
    }
}