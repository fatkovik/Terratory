using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeCollider : MonoBehaviour
{
    public event System.Action<Collider2D> ColliderEnter;
    [SerializeField] private CircleCollider2D collider;
    public CircleCollider2D Collider => collider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ColliderEnter?.Invoke(other);
        Debug.Log("Collision");
    }
}
    