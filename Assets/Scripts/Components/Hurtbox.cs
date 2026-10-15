using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    private Health _health;
    public Health Owner => _health;

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
        Debug.Log($"ReceiveHit invoked with {amount} damage");
        _health.TakeDamage(amount);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("Entered Trigger");
    //     ReceiveHit(10);
    // } 
}
