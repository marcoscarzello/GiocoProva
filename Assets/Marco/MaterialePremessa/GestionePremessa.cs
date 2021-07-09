using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionePremessa : MonoBehaviour
{
    public GameObject blocco1;
    public GameObject blocco2;
    public GameObject blocco3;
    public GameObject bloccoIstruzioni;


    private AudioSource[] audios;

    void Start()
    {
        audios = GetComponents<AudioSource>();

        audios[0].Play();


        blocco1.SetActive(true);
        blocco2.SetActive(false);
        blocco3.SetActive(false);
        bloccoIstruzioni.SetActive(false);


    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (blocco1.active)
            {
                //audios[0].Stop();

                //audios[1].Play();

                blocco1.SetActive(false);
                blocco2.SetActive(true);
            }

            else if (blocco2.active)
            {
                //audios[1].Stop();

                //audios[2].Play();

                blocco2.SetActive(false);
                blocco3.SetActive(true);
            }

            else if (blocco3.active)
            {

                blocco3.SetActive(false);
                bloccoIstruzioni.SetActive(true);
            }

            else if (bloccoIstruzioni.active)
            {
                //cambioscena
                StartCoroutine(StartFade(audios[0], 1f, 0f, 0));
                gameObject.GetComponent<MainMenu>().ToMenu(); 
            }

        }
    }
    public static IEnumerator StartFade(AudioSource[] music, float duration, float targetVolume, int i)
    {
        float currentTime = 0;
        float start = music[i].volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            music[i].volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        //music[i + 1].Play();
        yield break;
    }
}
