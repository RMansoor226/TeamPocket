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
        }
    }

    public void ReceiveHit(float amount)
    {
        float finalDamage = amount * damageMultiplier;
        Debug.Log($"ReceiveHit invoked with {finalDamage} damage");
        _health.TakeDamage(finalDamage);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("Entered Trigger");
    //     ReceiveHit(10);
    // } 
}
