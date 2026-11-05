using System;
using UnityEngine;
using UnityEngine.UI;

public class TrainingDummy : MonoBehaviour
{
    [SerializeField] bool EnableLogging = false;
    [SerializeField] Text HealthText;

    private MeshRenderer mesh;
    private Health health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        mesh = GetComponent<MeshRenderer>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamageTaken(float hp)
    {   
        if (EnableLogging)
        {
            Debug.Log($"Ouch!!! Dummy at {hp} health");
        }

        HealthText.text = Convert.ToString(hp);
    }

    public void OnDeath()
    {
        mesh.enabled = false;
        Invoke(nameof(Respawn), 3f);
    }

    private void Respawn()
    {
        mesh.enabled = true;
        health.CurrentHealth = health.MaxHealth;
        HealthText.text = Convert.ToString(health.MaxHealth);
    }
}
