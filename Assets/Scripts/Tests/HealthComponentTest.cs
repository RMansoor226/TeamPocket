using UnityEngine;

public class HealthComponentTest : MonoBehaviour
{
    [SerializeField] private GameObject ObjectWithHealth;

    private void DamageListenerTest(double current)
    {
        Debug.Log($"I am listening to the OnDamaged event! I just triggered from {gameObject.name}! The current health is {current}.");
    }
    private void HealListenerTest(double current)
    {
        Debug.Log($"I am listening to the OnHealed event! I just triggered from {gameObject.name}! The current health is {current}.");
    }
    private void DeathListenerTest()
    {
        Debug.Log($"I am listening to the OnDeath event! I just triggered from {gameObject.name}!");
    }

    private void Start()
    {
        Health HealthComponentOnObject = ObjectWithHealth.GetComponent<Health>();

        Debug.Log($"Health component start! Current health is {HealthComponentOnObject.CurrentHealth}.");
        Debug.Log("Dealing 50 damage, overhealing it to test clamp, then dealing 75 and then 40 to kill and test clamping.");

        HealthComponentOnObject.OnDamaged.AddListener(DamageListenerTest);
        HealthComponentOnObject.OnHealed.AddListener(HealListenerTest);
        HealthComponentOnObject.OnDeath.AddListener(DeathListenerTest);

        HealthComponentOnObject.TakeDamage(50.0);
        HealthComponentOnObject.Heal(75.0);
        HealthComponentOnObject.TakeDamage(75);
        HealthComponentOnObject.TakeDamage(40.0);
    }
}