using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LittleEnergyBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        switch (name)
        {
            case "EnergyBarHealth":
                SetMaxEnergy(35);
                break;
            case "EnergyBarAmmos":
                SetMaxEnergy(100);
                break;
            case "EnergyBarFire":
                SetMaxEnergy(60);
                break;
            default:
                break;
        }
        SetEnergy((int)slider.maxValue);
    }

    public void SetMaxEnergy(int health)
    {
        slider.maxValue = health;
    }

    public void SetEnergy(int health)
    {
        slider.value = health;
    }

}

