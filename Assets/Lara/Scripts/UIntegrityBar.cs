using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIntegrityBar : MonoBehaviour
{
    private AudioSource audio;

    public Slider slider;
    public static int currentValue;

    public void SetMaxIntegrity(int integrity)
    {
        slider.maxValue = integrity;
        slider.value = integrity;

    }
    public void SetDamage(int damage)
    {
        audio.Play();
        slider.value -= damage;
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        slider.value = 100;
        SetMaxIntegrity(100);
        SetDamage(0);
    }

    private void Update()
    {
        if(slider.value == 0)
        {
            Debug.Log("GameOver"); 
        }
    }
}
