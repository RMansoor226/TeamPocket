using System;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private CapsuleCollider _collider;
    
    [SerializeField] private float damage;

    private readonly HashSet<Health> _alreadyHit = new();
    
    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
        if (_collider == null)
        {
            Debug.LogError("No Capsule Collider Attached!");
        }

        Disable();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Hurtbox>(out Hurtbox hb))
        {
            if (hb.Owner == null)
            {
                Debug.LogError("No Health component that owns Hurtbox");
                return;
            }
            
            if (!_alreadyHit.Add(hb.Owner)) return;
            
            hb.ReceiveHit(damage);
            
            Debug.Log($"Hit landed at {Time.frameCount}");
        }
    }

    [ContextMenu("Enable Hitbox")]
    public void Enable()
    {
        _collider.enabled = true;
        _alreadyHit.Clear();
    }
    
    [ContextMenu("Disable Hitbox")]
    public void Disable()
    {
        _collider.enabled = false;
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.matrix = transform.localToWorldMatrix;
    //
    //     if (_collider.enabled)
    //     {
    //         Gizmos.color = Color.red;
    //     }
    //     else
    //     {
    //         Gizmos.color = Color.gray;
    //     }
    //     
    // }
}