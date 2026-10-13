using UnityEngine;

public class HealthPad : MonoBehaviour
{
    [SerializeField] bool IsDamagePad = true;
    [SerializeField] float AmountChanged = 20f;

    private void OnTriggerEnter(Collider other)
    {
        Health HealthComponent = other.GetComponent<Health>();
        if (HealthComponent)
        {
            if (IsDamagePad)
            {
                HealthComponent.TakeDamage(AmountChanged);
            } else
            {
                HealthComponent.Heal(AmountChanged);
            }
        }
    }
}
