using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public UnityEvent<double> OnDamaged;
    public UnityEvent<double> OnHealed;
    public UnityEvent OnDeath;

    [SerializeField] private double MaxHealth = 100.0;
    public double CurrentHealth;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(double amount)
    {
        if (CurrentHealth == 0.0) return;

        CurrentHealth = Math.Clamp(CurrentHealth - amount, 0.0, MaxHealth);
        OnDamaged.Invoke(CurrentHealth);

        Debug.Log($"Health component on {gameObject.name} took {amount} damage.");

        if (CurrentHealth <= 0) Die();
    }

    public void Heal(double amount)
    {
        if (CurrentHealth == 0.0) return;

        CurrentHealth = Math.Clamp(CurrentHealth + amount, 0.0, MaxHealth);
        OnHealed.Invoke(CurrentHealth);

        Debug.Log($"Health component on {gameObject.name} healed by {amount}.");
    }

    public void Die()
    {
        OnDeath.Invoke();
        
        Debug.Log("DEAD!");
    }
}
