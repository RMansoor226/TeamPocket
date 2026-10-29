using System;
using Components;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public UnityEvent<float> OnDamaged;
    public UnityEvent<float> OnHealed;
    public UnityEvent OnDeath;

    [SerializeField] private float MaxHealth = 100.0f;
    public float CurrentHealth;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        TakeDamage(new DamageInfo(amount, null, DamageType.Physical));
    }
    
    public void TakeDamage(DamageInfo damageInfo)
    {
        float amount = damageInfo.Amount;
        if (amount <= 0)
        {
            Debug.LogError("Invalid damage value. Must be greater than 0");
            return;
        }
        
        if (CurrentHealth <= 0.0f) return;

        CurrentHealth = Math.Clamp(CurrentHealth - amount, 0.0f, MaxHealth);
        OnDamaged.Invoke(CurrentHealth);

        string damageSource = damageInfo.Source != null ? damageInfo.Source.name : "No Source";

        Debug.Log($"Health component on {gameObject.name} took {amount} damage with {damageInfo.Type} type from {damageSource}.");

        if (CurrentHealth <= 0) Die();
    }

    public void Heal(float amount)
    {
        if (CurrentHealth == 0.0) return;

        CurrentHealth = Math.Clamp(CurrentHealth + amount, 0.0f, MaxHealth);
        OnHealed.Invoke(CurrentHealth);

        Debug.Log($"Health component on {gameObject.name} healed by {amount}.");
    }

    public void Die()
    {
        OnDeath.Invoke();
        
        Debug.Log("DEAD!");
    }
}
