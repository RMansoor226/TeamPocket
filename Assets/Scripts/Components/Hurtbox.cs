using Components;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    private Health _health;
    public Health Owner => _health;

    [SerializeField] private float damageMultiplier = 1f;

    private void Awake()
    {
        _health = GetComponentInParent<Health>();
        if (_health == null)
        {
            Debug.LogError("No Health Component Attached in Parent");
            return;
        }
    }

    public void ReceiveHit(DamageInfo damageInfo)
    {
        float finalDamage = damageInfo.Amount * damageMultiplier;
        DamageInfo newDamageInfo = new DamageInfo(finalDamage, damageInfo.Source, damageInfo.Type);
        
        _health.TakeDamage(newDamageInfo);
    }
}
