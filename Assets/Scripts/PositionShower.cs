using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PositionShower : MonoBehaviour
{
    public TextMeshProUGUI etichettaPosizione;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        etichettaPosizione.text = Player.posizione.ToString();
    }
}
