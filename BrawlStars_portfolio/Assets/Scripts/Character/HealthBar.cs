using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    // public Gradient Gradient;
    // public Image fill;
    public void SetMaxHealth(int Health)
    {
        slider.maxValue = Health;   // Set Max Health
        slider.value = Health;      // Set Health value
        // fill.color = Gradient.Evaluate(1f);
    }
    public void SetHealth(int Health)
    {
        slider.value = Health;
    }
}
