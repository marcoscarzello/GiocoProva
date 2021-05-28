using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    private void Start()
    {
        SetMaxHealth(100);
        SetHealth(100);
    }
}