using System;
using System.Collections.Generic;
using Components;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private Collider _collider;
    private GameObject _ownerRoot;
    
    [SerializeField] private float damage;
    [SerializeField] private DamageType damageType;

    private readonly HashSet<Health> _alreadyHit = new();
    
    private void Awake()
    {
        Health ownerHealth = GetComponentInParent<Health>();
        _ownerRoot = ownerHealth != null ? ownerHealth.gameObject 
            : transform.root.gameObject;
        
        _collider = GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.LogError($"{name}: No Collider Attached to Hitbox!", this);
            enabled = false;
            return;
        }

        if (!_collider.isTrigger)
        {
            Debug.LogWarning($"{name}: Hitbox Collider is not set to trigger! Manually forcing it.", this);
            _collider.isTrigger = true;
        }

        Disable();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Hurtbox>(out Hurtbox hb))
        {
            Debug.LogWarning($"{other.name} has no hurtbox");
            return;
        }
        
        if (hb.Owner == null)
        {
            Debug.LogError("No Health component that owns Hurtbox");
            return;
        }
            
        if (!_alreadyHit.Add(hb.Owner))
        {
            Debug.Log("Object was already hit");
            return;
        }
            
        hb.ReceiveHit(new DamageInfo(damage, _ownerRoot, damageType));
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
}