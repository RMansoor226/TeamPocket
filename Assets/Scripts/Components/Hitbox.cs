using System;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private CapsuleCollider _collider;
    
    [SerializeField] private float damage;

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
            hb.ReceiveHit(damage);
        }
    }

    [ContextMenu("Enable Hitbox")]
    public void Enable()
    {
        _collider.enabled = true;
    }
    
    [ContextMenu("Disable Hitbox")]
    public void Disable()
    {
        _collider.enabled = false;
    }
}