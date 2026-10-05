using UnityEngine.Events;

public interface IDamageable
{
    void TakeDamage(float amount);
    void Heal(float amount);
    void Die();
}
