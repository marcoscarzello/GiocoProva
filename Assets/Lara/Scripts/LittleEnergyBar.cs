using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LittleEnergyBar : MonoBehaviour
{
    public Slider slider;

    public Slider sliderGrande;

    private void Start()
    {
        switch (name)
        {
            case "EnergyBarHealth":
                SetMaxEnergy(30);
                break;
            case "EnergyBarAmmos":
                SetMaxEnergy(100);
                break;
            case "EnergyBarFire":
                SetMaxEnergy(60);
                break;
            case "EnergyBarPosition":
                SetMaxEnergy(40);
                break;
            default:
                break;
        }
    }

    public void Update()
    {
        SetEnergy((int)sliderGrande.value);

    }

    public void SetMaxEnergy(int n)
    {
        slider.maxValue = n;
    }

    public void SetEnergy(int n)
    {
        slider.value = n;
    }

}

