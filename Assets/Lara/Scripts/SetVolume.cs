using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    //public GameObject camera=null;
    public Slider s;
    void Start()
    {
        s = GetComponent<Slider>();
        s.value = GameObject.Find("slaid").GetComponent<slaid>().volume;
    }

    public void SetLevel(float sliderValue)
    {
        //mixer.SetFloat("AudioVol", Mathf.Log10(sliderValue) * 20);
        AudioListener.volume = sliderValue;
        GameObject.Find("slaid").GetComponent<slaid>().volume = sliderValue;
        Debug.Log("Master Volume" + AudioListener.volume);
    }

}
