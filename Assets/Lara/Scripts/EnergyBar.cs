using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxEnergy(int health)
    {
        slider.maxValue = health;
        //slider.value = health;

    }
    public void SetEnergy(int health)
    {
        slider.value = health;
    }

    private void Start()
    {
        SetMaxEnergy(100);
        SetEnergy(50);
    }
}