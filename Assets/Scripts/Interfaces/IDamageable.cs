using Components;
using UnityEngine.Events;

public interface IDamageable
{
    void TakeDamage(DamageInfo damageInfo);
    void Heal(float amount);
    void Die();
}
