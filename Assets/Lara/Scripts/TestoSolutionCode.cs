using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TestoSolutionCode : MonoBehaviour
{
    public TextMeshProUGUI testo;

    void Start()
    {

    }

    void Update()
    {

        testo.text = EnemyFinder.codiceSoluzione;

    }
}
