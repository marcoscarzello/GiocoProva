using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProvaPosizioneCanvaeccecc : MonoBehaviour
{
    public Vector3 posizionePG;
    public TextMeshProUGUI testo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        testo.text = posizionePG.ToString();
    }
}
