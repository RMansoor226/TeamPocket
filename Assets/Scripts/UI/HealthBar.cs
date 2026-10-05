using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Text TextValue;

    public void UpdateHealth(float CurrentHealth)
    {
        HealthSlider.value = CurrentHealth;
        TextValue.text = Convert.ToString(CurrentHealth);
    }
}
