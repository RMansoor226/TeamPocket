using UnityEngine.Events;

public interface IDamageable
{
    void TakeDamage(double amount);
    void Heal(double amount);
    void Die();
}
