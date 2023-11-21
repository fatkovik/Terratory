using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeColliderScript : MonoBehaviour
{
    public event System.Action<Collider2D> ColliderEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ColliderEnter?.Invoke(other);
        Debug.Log("Collision");
    }
}
    